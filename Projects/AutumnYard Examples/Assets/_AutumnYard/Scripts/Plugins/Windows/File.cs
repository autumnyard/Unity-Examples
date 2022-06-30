using System.IO;
using UnityEngine;
using SimpleJSON;

namespace AutumnYard.Plugins.Windows
{
    public static class File
    {
        public static void ReadJSONFromFile(out JSONObject json, string filePath)
        {
            json = null;
            if (System.IO.File.Exists(filePath))
            {
                string jsonString = System.IO.File.ReadAllText(filePath);
                json = JSON.Parse(jsonString) as JSONObject;
            }
            else
            {
                Debug.LogError($"Couldn't find file {filePath} to read.");
            }
        }

        public static void WriteJSONToFile(JSONObject json, string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
            System.IO.File.WriteAllText(filePath, json.ToString(2));
        }

    }
}
