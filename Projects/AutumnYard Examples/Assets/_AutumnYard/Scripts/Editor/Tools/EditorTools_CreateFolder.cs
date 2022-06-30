using UnityEditor;
using UnityEngine;

namespace AutumnYard.Editor
{
    public static partial class EditorTools
    {
        [MenuItem("Autumn Yard/Create standard folders", priority = 61)]
        private static void CreateStandardFolders()
        {
            // Metodo 1
            //string path = "Assets";
            //foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            //{
            //  path = AssetDatabase.GetAssetPath(obj);
            //  if (UnityEngine.Windows.File.Exists(path))
            //  {
            //    path = System.IO.Path.GetDirectoryName(path);
            //  }
            //  break;
            //}

            // Metodo 2
            //string clickedAssetGuid = Selection.assetGUIDs[0];
            //string clickedPath = AssetDatabase.GUIDToAssetPath(clickedAssetGuid);
            //string clickedPathFull = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), clickedPath);

            //System.IO.FileAttributes attr = System.IO.File.GetAttributes(clickedPathFull);
            //string path = attr.HasFlag(System.IO.FileAttributes.Directory) ? clickedPathFull : System.IO.Path.GetDirectoryName(clickedPathFull);

            // Metodo 3
            string path = "-";
            foreach (var obj in Selection.GetFiltered<UnityEngine.Object>(SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (string.IsNullOrEmpty(path))
                    continue;

                if (System.IO.Directory.Exists(path))
                {
                    break;
                }
                else if (System.IO.File.Exists(path))
                {
                    path = System.IO.Path.GetDirectoryName(path);
                    break;
                }
            }

            // Materials
            AssetDatabase.CreateFolder(path, "Materials");
            AssetDatabase.CreateFolder(path, "Meshes");
            AssetDatabase.CreateFolder(path, "Textures");

            Debug.Log($"Creadas carpetas estandar en {path}");
        }
    }
}
