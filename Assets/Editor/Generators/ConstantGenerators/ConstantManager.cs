using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using File = System.IO.File;

namespace Editor.Generators.ConstantGenerators
{
    public class ConstantManager
    {
        private const string OutputDic = "Assets/Scripts/Constants/";
        
        [MenuItem("Tools/Generate Constants")]
        public static void GenerateConstants()
        {
            string directoryName = Path.GetDirectoryName(OutputDic);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName!);
            }
            
            GenerateLayers();
            GenerateTags();
            GenerateSortingLayers();
            
            AssetDatabase.Refresh();
        }

        private static void GenerateLayers()
        {
            string fileName = OutputDic + "LayerConstants.cs";
            
            StringBuilder sb = new StringBuilder(); 
            sb.AppendLine("namespace CouchHero.Constants\n{\n\tpublic static partial class GameConstants\n\t{\n\t\tpublic static class LayerConstants\n\t\t{");
            for (int i = 0; i < 32; i++)
            {
                string layerName = LayerMask.LayerToName(i);
                layerName = layerName.Replace(" ", "")
                                     .Replace("$", "")
                                     .Replace("/", "")
                                     .Replace(".", "")
                                     .Replace(",", "")
                                     .Replace(";", "")
                                     .Replace("-", "")
                                     .Replace("_", "");
                if (layerName.Length == 0)
                {
                    continue;
                }

                sb.AppendLine($"\t\t\tpublic const int Layer{layerName} = {i};");
            }
            
            sb.AppendLine("\t\t}\n\t}\n}");
            File.WriteAllText(fileName, sb.ToString());
        }
        
        private static void GenerateTags()
        {
            string fileName = OutputDic + "TagConstants.cs";

            StringBuilder sb = new StringBuilder(); 
            sb.AppendLine("namespace CouchHero.Constants\n{\n\tpublic static partial class GameConstants\n\t{\n\t\tpublic static class TagConstants\n\t\t{");
            
            sb.AppendLine("\t\tpublic const int TagUntagged = 0;");
            sb.AppendLine("\t\tpublic const int TagRespawn = 1;");
            sb.AppendLine("\t\tpublic const int TagFinish = 2;");
            sb.AppendLine("\t\tpublic const int TagEditorOnly = 3;");
            sb.AppendLine("\t\tpublic const int TagMainCamera = 4;");
            sb.AppendLine("\t\tpublic const int TagPlayer = 5;");
            sb.AppendLine("\t\tpublic const int TagGameController = 6;");
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty tagsProp = tagManager.FindProperty("tags");
            
            for (int i = 0; i < tagsProp.arraySize; i++)
            {
                SerializedProperty tag = tagsProp.GetArrayElementAtIndex(i);
                if (!string.IsNullOrEmpty(tag.stringValue))
                {
                    sb.AppendLine($"\t\t\tpublic const int Tag{tag.stringValue} = {i+7};");
                }
            }
            
            sb.AppendLine("\t\t}\n\t}\n}");
            File.WriteAllText(fileName, sb.ToString());
        }

        private static void GenerateSortingLayers()
        {
            string fileName = OutputDic + "SortingLayerConstants.cs";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("namespace CouchHero.Constants\n{\n\tpublic static partial class GameConstants\n\t{\n\t\tpublic static class SortingLayerConstants\n\t\t{");

            foreach (SortingLayer sortingLayer in SortingLayer.layers)
            {
                sb.AppendLine($"\t\t\tpublic const int SortingLayer{sortingLayer.name} = {sortingLayer.value};");
            }
            
            sb.AppendLine("\t\t}\n\t}\n}");
            File.WriteAllText(fileName, sb.ToString());
        }
    }
}