using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

namespace itfantasy.umvc.Editor
{
    public class ConfigAssets
    {

        const string configAssetsSavePath = "Assets/itfantasy/Editor/umvc/";

        [MenuItem("itfantasy/umvc/Reset Config Assets")]
        static void ResetConfigAssets()
        {
            ResetCodeGeneratorConfig();
        }

        static void ResetCodeGeneratorConfig()
        {
            ScriptableObject config = ScriptableObject.CreateInstance<CodeGeneratorConfig>();
            AssetDatabase.CreateAsset(config, configAssetsSavePath + "CodeGeneratorConfig.Asset");
        }

        public static CodeGeneratorConfig LoadCodeGeneratorConfig()
        {
            return AssetDatabase.LoadAssetAtPath<CodeGeneratorConfig>(configAssetsSavePath + "CodeGeneratorConfig.Asset");
        }
    }

    [Serializable]
    public class CodeGeneratorConfig : ScriptableObject
    {
        public string codeSavePath = "Scripts/";

        public string uiGenerateFunc = "this.transform.Find(\"##PATH##\").GetComponent<##TYPE##>()";

        public List<PrefixConfig> prefixConfigList = new List<PrefixConfig>();
    }

    [Serializable]
    public class PrefixConfig
    {
        public string prefix;
        public string classType;
    }
}
