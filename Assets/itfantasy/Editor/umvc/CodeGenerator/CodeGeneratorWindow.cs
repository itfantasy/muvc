using UnityEngine;
using UnityEditor;
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

        void OnInspectorUpdate()
        {
            Repaint();
        }

        string name = "";

        void OnGUI()
        {
            name = EditorGUILayout.TextField("name", name);
            if (GUILayout.Button("Generate"))
            {
                GenerateCode();
                this.Close();
            }
        }

        void GenerateCode()
        {
            CodeGeneratorConfig config = ConfigAssets.LoadCodeGeneratorConfig();

            string templateDir = Application.dataPath + "/itfantasy/Editor/umvc/CodeGenerator/CodeTemplate";

            List<FileInfo> fileInfos = FileIOUtil.GetFileInfos(templateDir);
            foreach(FileInfo fileInfo in fileInfos)
            {
                string fileName = fileInfo.Name;
                if (fileName.Contains(".meta"))
                {
                    continue;
                }
                fileName = fileName.Replace(".txt", "");
                string saveName = fileName.Replace("template", name);
                string content = FileIOUtil.ReadFile(fileInfo.FullName);
                string saveContent = content.Replace("##NAME##", name);
                string savePath = Application.dataPath + "/" + config.codeSavePath + "/" + saveName;
                FileIOUtil.CreateFile(savePath, saveContent);
                Debug.Log("[GenerateCode]:: " + config.codeSavePath + "/" + saveName);
            }

            AssetDatabase.Refresh();
        }

    }
}
