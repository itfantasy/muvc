using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using itfantasy.run.utils;

namespace itfantasy.umvc.Editor
{
    public class CodeGeneratorWindow : EditorWindow
    {
        private static CodeGeneratorWindow _ins;
        public static CodeGeneratorWindow ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = (CodeGeneratorWindow)EditorWindow.GetWindow(typeof(CodeGeneratorWindow));
                }
                _ins.minSize = new Vector2(300, 200);
                _ins.maxSize = new Vector2(300, 250);
                return _ins;
            }
        }

        public string name { get; set; }
        public bool hasView { get; set; }
        public GameObject root { get; set; }
        public bool onlyView { get; set; }

        void OnInspectorUpdate()
        {
            Repaint();
        }

        CodeGeneratorConfig config = null;

        void OnGUI()
        {
            name = EditorGUILayout.TextField("name", name);
            if (!onlyView)
            {
                hasView = EditorGUILayout.Toggle("hasView", hasView);
            }
            root = EditorGUILayout.ObjectField("root", root, typeof(GameObject), true) as GameObject;
            if (GUILayout.Button("Generate"))
            {
                GenerateCode();
                this.Close();
            }
        }

        void GenerateCode()
        {
            config = ConfigAssets.LoadCodeGeneratorConfig();

            string templateDir = Application.dataPath + "/itfantasy/Editor/umvc/CodeGenerator/CodeTemplate";

            List<FileInfo> fileInfos = FileIOUtil.GetFileInfos(templateDir);
            foreach(FileInfo fileInfo in fileInfos)
            {
                string fileName = fileInfo.Name;
                if (fileName.Contains(".meta"))
                {
                    continue;
                }
                if (onlyView)
                {
                    if (fileName != "TemplateView.cs.txt")
                    {
                        continue;
                    }
                }
                fileName = fileName.Replace(".txt", "");
                string saveName = fileName.Replace("Template", name);
                string content = FileIOUtil.ReadFile(fileInfo.FullName);
                string saveContent = content.Replace("##NAME##", name);
                saveContent = saveContent.Replace("##HASVIEW##", hasView ? "" : "//");
                if (hasView && fileName == "TemplateView.cs")
                {
                    GenerateViewCode(ref saveContent);
                }
                string savePath = "/" + config.codeSavePath + "/" + name + "Window/" + saveName;
                if(!onlyView)
                {
                    if(FileIOUtil.FileExists(Application.dataPath + savePath))
                    {
                        Debug.LogError("The target files have existed! However, if you want to recreate them, delete please!");
                        return;
                    }
                }
                FileIOUtil.CreateFile(Application.dataPath + savePath, saveContent);
                Debug.Log("[GenerateCode]:: " + savePath);
            }

            AssetDatabase.Refresh();
        }

        List<ViewUIInfo> _viewUIInfoList;
        string _uiList = "";
        string _genUIList = "";

        void GenerateViewCode(ref string saveContent)
        {
            _viewUIInfoList = new List<ViewUIInfo>();
            _uiList = _genUIList = "";
            TraverseUIFromGameObject(root);
            foreach (ViewUIInfo info in _viewUIInfoList)
            {
                _uiList += String.Format("    public {0} {1};\r\n", info.classType, info.name);
                string genUIListFormate = config.uiGenerateFunc.Replace("##PATH##", "{0}");
                genUIListFormate = genUIListFormate.Replace("##TYPE##", "{1}");
                _genUIList += String.Format("        this." + info.name + " = " + genUIListFormate + ";\r\n", info.path, info.classType);
            }
            saveContent = saveContent.Replace("##UILIST##", _uiList);
            saveContent = saveContent.Replace("##GENUILIST##", _genUIList);
        }

        void TraverseUIFromGameObject(GameObject go, string tempPath="")
        {
            if (go != root)
            {
                foreach(PrefixConfig item in config.prefixConfigList)
                {
                    if (go.name.StartsWith(item.prefix))
                    {
                        ViewUIInfo info = new ViewUIInfo();
                        info.name = go.name;
                        info.path = tempPath + info.name;
                        info.classType = item.classType;
                        _viewUIInfoList.Add(info);
                    }
                }
            }
            if (go.transform.childCount <= 0)
            {
                return;
            }
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform child = go.transform.GetChild(i);
                if (go == root)
                {
                    TraverseUIFromGameObject(child.gameObject, tempPath);
                }
                else
                {
                    TraverseUIFromGameObject(child.gameObject, tempPath + go.name + "/");
                }
            }
        }
    }

    public class ViewUIInfo
    {
        public string name = "";
        public string path = "";
        public string classType = "";
    }
}
