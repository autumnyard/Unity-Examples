using System;
using System.IO;
using UnityEngine;
using SimpleJSON;
//using Newtonsoft.Json; // TODO: Dependency with Newtonsoft.JSON
#if UNITY_SWITCH
using AutumnYard.Plugins.NintendoSwitch;
#elif UNITY_PS4
using AutumnYard.Plugins.PlayStation;
#endif

namespace AutumnYard.Tools.IO
{
    //public interface ISaveData
    //{
    //}
    //public abstract class SaveData : ISaveData
    //{
    //}

    public static class File
    {
        private const string mountName = "Save";
        private const string Extension = ".json";

        public static event Action<bool> onSave;


        #region System specific: Windows

#if (!UNITY_SWITCH && !UNITY_PS4) || UNITY_EDITOR

        private static void WriteToFile(string filePath, JSONObject json)
        {
        }

        public static void DirectoryCopy(string originPath, string targetPath)
        {
            // Copied straight from Unity docs

            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(originPath);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + originPath);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.
            Directory.CreateDirectory(targetPath);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(targetPath, file.Name);

                if (System.IO.File.Exists(tempPath))
                {
                    System.IO.File.Delete(tempPath);
                }

                file.CopyTo(tempPath, false);
            }

        }

        public static void DirectoryReadFromStreamingAssets(string originPath, string targetPath)
        {
            // Copied straight from Unity docs
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(originPath);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + originPath);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.
            Directory.CreateDirectory(targetPath);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (file.Name.Contains(".meta")) continue;

                string tempPath = Path.Combine(targetPath, file.Name);

                if (System.IO.File.Exists(tempPath))
                {
                    System.IO.File.Delete(tempPath);
                }

                file.CopyTo(tempPath, false);
            }

        }

#endif

        #endregion


        #region File operations

        public static void ReadJSONFromFile(out JSONObject json, string filePath)
        {
#if UNITY_SWITCH && !UNITY_EDITOR
      ThirdParty.NintendoSwitch.File.ReadJSONFromFile(out json, filePath);
#elif UNITY_PS4 && !UNITY_EDITOR
      ThirdParty.PlayStation.File.ReadJSONFromFile(out json, filePath);
#else
            Plugins.Windows.File.ReadJSONFromFile(out json, filePath);
#endif
        }

        public static void WriteJSONToFile(JSONObject json, string filePath)
        {
            onSave?.Invoke(true);

#if UNITY_SWITCH && !UNITY_EDITOR
      ThirdParty.NintendoSwitch.File.WriteJSONToFile(json, filePath);
#elif UNITY_PS4 && !UNITY_EDITOR
      ThirdParty.PlayStation.File.WriteJSONToFile(json, filePath);
#else
            Plugins.Windows.File.WriteJSONToFile(json, filePath);
#endif

            onSave?.Invoke(false);
        }

        #endregion


        #region Tools

        public static void ReadFromFile(string filePath, out JSONObject json)
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

        public static string CalculateFolderPath_Persistent(string slot)
        {
            string result;

#if UNITY_SWITCH && !UNITY_EDITOR
      result = $"{mountName}:/{slot}/";
#elif UNITY_PS4 && !UNITY_EDITOR
      result = string.Format(mountPoint + "/{1}/", mountName, slot.ToString());
#else
            result = $"{Application.persistentDataPath}/{mountName}/{slot}/";
#endif

            return result;
        }

        public static string CalculateFolderPath_StreamingAssets(string slot)
        {
            return string.Format(Application.streamingAssetsPath + "/{0}/{1}/", mountName, slot.ToString());
        }

        private static string CalculateFilePath(string slot, string fileName, string extension = Extension)
        {
            string result = string.Empty;

#if UNITY_SWITCH && !UNITY_EDITOR
      result = $"{mountName}:/{slot}/{fileName}{extension}";
#elif UNITY_PS4 && !UNITY_EDITOR
      result = string.Format(mountPoint + "/{1}/{2}{3}", mountName, slot.ToString(), fileName, extension);
#else
            result = $"{Application.persistentDataPath}/{mountName}/{slot}/{fileName}{extension}";
#endif
            result = result.Replace($"{extension}{extension}", $"{extension}");

            return result;
        }

        public static string CalculateFilePath_Persistent(string slot, string fileName, string extension = Extension)
        {
            string result = string.Empty;

#if UNITY_SWITCH && !UNITY_EDITOR
      result = $"{mountName}:/{slot}/{fileName}{extension}";
#elif UNITY_PS4 && !UNITY_EDITOR
      result = string.Format(mountPoint + "/{1}/{2}{3}", mountName, slot.ToString(), fileName, extension);
#else
            result = $"{Application.persistentDataPath}/{mountName}/{slot}/{fileName}{extension}";
#endif
            result = result.Replace($"{extension}{extension}", $"{extension}");

            return result;
        }

        public static string CalculateFilePath_StreamingAssets(string slot, string fileName, string extension = Extension)
        {
            return string.Format(Application.streamingAssetsPath + "/{0}/{1}/{2}{3}", mountName, slot.ToString(), fileName, extension)
                   .Replace($"{extension}{extension}", $"{extension}");
        }

        #endregion


        #region Serialization

        public static void WriteToFile(in string data, in string fileName)
        {
            string path = CalculateFilePath_Persistent("Main", fileName);

            FileInfo fileInfo = new FileInfo(path);
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
            System.IO.File.WriteAllText(path, data);
        }

        public static void ReadFromFile(out string data, in string fileName)
        {
            string filePath = CalculateFilePath_Persistent("Main", fileName);
            if (System.IO.File.Exists(filePath))
            {
                data = System.IO.File.ReadAllText(filePath);
            }
            else
            {
                Debug.LogError($"Couldn't find file {filePath} to read.");
                data = null;
            }
        }

        // TODO: Dependency with Newtonsoft.JSON
        //public static void Serialize<T>(T data, string filePath)/* where T : SaveData*/
        //{
        //    filePath = CalculateFilePath_Persistent("Main", filePath);
        //    string serialized = JsonConvert.SerializeObject(data, Formatting.Indented);

        //    FileInfo fileInfo = new FileInfo(filePath);
        //    if (!fileInfo.Directory.Exists)
        //    {
        //        fileInfo.Directory.Create();
        //    }
        //    System.IO.File.WriteAllText(filePath, serialized);
        //}

        //public static void Deserialize<T>(out T data, string filePath) where T : class /*where T : SaveData*/
        //{
        //    filePath = CalculateFilePath_Persistent("Main", filePath);
        //    if (System.IO.File.Exists(filePath))
        //    {
        //        string read = System.IO.File.ReadAllText(filePath);
        //        data = (T)JsonConvert.DeserializeObject(read, typeof(T));
        //    }
        //    else
        //    {
        //        Debug.LogError($"Couldn't find file {filePath} to read.");
        //        data = null;
        //    }
        //}

        #endregion


    }
}