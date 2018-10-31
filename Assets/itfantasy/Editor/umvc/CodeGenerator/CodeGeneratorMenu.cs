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
            window.ShowPopup();
        }
    }
}
