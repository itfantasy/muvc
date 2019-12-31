using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace itfantasy.umvc.Editor
{
    public class CodeGeneratorMenu
    {
        [MenuItem("itfantasy/umvc/Code Generator")]
        static void CodeGenerator()
        {
            CodeGeneratorWindow window = CodeGeneratorWindow.ins;
            window.titleContent = new GUIContent("Generate Code");
            GameObject root = Selection.activeGameObject;
            if (root != null)
            {
                window.root = root;
                window.hasView = true;
                window.name = root.name.EndsWith("Window") ? root.name.Replace("Window", "") : root.name;
            }
            else
            {
                window.root = null;
                window.hasView = false;
                window.name = "";
            }
            window.ShowPopup();
        }

        [MenuItem("GameObject/UI/Generate umvc Code - itfantasy ")]
        static void CodeGeneratorFromGameObject()
        {
            CodeGenerator();
        }

        [MenuItem("GameObject/UI/Rebuild umvc View Code - itfantasy ")]
        static void CodeGeneratorOnlyViewFromGameObject()
        {
            CodeGeneratorWindow window = CodeGeneratorWindow.ins;
            window.titleContent = new GUIContent("Generate Code (only view)");
            GameObject root = Selection.activeGameObject;
            if (root != null)
            {
                window.root = root;
                window.onlyView = true;
                window.hasView = true;
                window.name = root.name.EndsWith("Window") ? root.name.Replace("Window", "") : root.name;
                window.ShowPopup();
            }
        }
    }
}
