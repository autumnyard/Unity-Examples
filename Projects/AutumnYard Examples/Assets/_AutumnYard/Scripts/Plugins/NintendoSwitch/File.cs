#if UNITY_SWITCH
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;

namespace AutumnYard.Plugins.NintendoSwitch
{
  public class File
  {
    private const string mountName = "Save";

    private static nn.account.Uid userId;
    private static bool mounted = false;

    // the value entered in PlayerSettings > Publishing Settings > User account save data
    private const int journalSaveDataSize = 65536; // 65 KB. This value should be the actual journal size in bytes. This should by 32KB less than
    private const int loadBufferSize = 65536; //32768;

    private const int journalDoubleSaveDataSize = 131072;
    private const int loadDoubleBufferSize = 131072;


#region Tools

    private static void ReadFromFile(string filePath, out JSONObject json)
    {
      json = null;
      if(System.IO.File.Exists(filePath))
      {
        string jsonString = System.IO.File.ReadAllText(filePath);
        json = JSON.Parse(jsonString) as JSONObject;
      }
      else
      {
        LogError($"Couldn't find file {filePath} to read.");
      }
    }

    private static void ListFilesInDirectory(string originPath, out List<string> fileNames)
    {
      DirectoryInfo dir = new DirectoryInfo(originPath);

      var files = dir.GetFiles();
      int numberOfFiles = files.Length;

      fileNames = new List<string>();
      for(int i = 0; i < numberOfFiles; i++)
      {
        if(files[i].Name.Contains(".meta")) continue;

        fileNames.Add(files[i].Name);
        Log($"   - Found file: {files[i].Name}");
      }

    }

    private static void Log(string text)
    {
      Debug.Log($"<color=cyan>[File]: (Switch) > {text}</color>");
    }

    private static void LogError(string text)
    {
      Debug.LogError($"<color=cyan>[File]:<color=red> (Switch) >  {text}</color></color>");
    }

#endregion


#region Internal

    public static void Mount()
    {
      nn.account.Account.Initialize();
      nn.account.UserHandle userHandle = new nn.account.UserHandle();
      nn.account.Account.TryOpenPreselectedUser(ref userHandle);
      nn.account.Account.GetUserId(ref userId, userHandle);

      nn.Result result = nn.fs.SaveData.Mount(mountName, userId);
      if(result.IsSuccess() == false)
      {
        if(nn.fs.FileSystem.ResultMountNameAlreadyExists.Includes(result))
        {
          LogError($"Mounting Critical Error: File System could not be mounted. Mount name already exists.");
          //result.abortUnlessSuccess();
          mounted = true;
        }
        else
        {
          LogError($"Mounting Critical Error: File System could not be mounted. Another error.");
          result.abortUnlessSuccess();
          mounted = false;
        }
      }
      else
      {
        Log("Mounted succesfully");
        mounted = true;
      }
    }

    public static void Unmount()
    {
      nn.fs.FileSystem.Unmount(mountName);
      mounted = false;
      Log("Unmounted succesfully");
    }


    private static bool CheckIfPathExists(string path)
    {
      nn.fs.EntryType entryType = 0;
            Debug.Log($" - CheckIfPathExists: {path}");
      nn.Result result = nn.fs.FileSystem.GetEntryType(ref entryType, path);
      bool exists = !nn.fs.FileSystem.ResultPathNotFound.Includes(result);

      //switch (entryType)
      //{
      //  case nn.fs.EntryType.Directory: Log($"Checkin if directory {path} exists: {exists}"); break;
      //  case nn.fs.EntryType.File: Log($"Checkin if file {path} exists: {exists}"); break;
      //}

      return exists;
    }


    private static bool FileCreate(string path, long size)
    {
      nn.Result result = nn.fs.File.Create(path, size);
      if(nn.fs.FileSystem.ResultPathAlreadyExists.Includes(result)) LogError($"Can't create file {path}: Path already exists");
      if(nn.fs.FileSystem.ResultPathNotFound.Includes(result)) LogError($"Can't create file {path}: Path not found");
      if(nn.fs.FileSystem.ResultUsableSpaceNotEnough.Includes(result)) LogError($"Can't create file {path}: Usable space not enough");

      //result.abortUnlessSuccess();

      return result.IsSuccess();
    }

    private static void ns_FileDelete(string path)
    {
      nn.Result result = nn.fs.File.Delete(path);
      if(nn.fs.FileSystem.ResultPathNotFound.Includes(result)) Log($"Can't delete file {path}: Path not found");
      if(nn.fs.FileSystem.ResultTargetLocked.Includes(result)) LogError($"Can't delete file {path}: Target locked");
    }

    private static void ns_FileRename(string currentPath, string newPath)
    {
      nn.Result result = nn.fs.File.Rename(currentPath, newPath);
      if(nn.fs.FileSystem.ResultPathNotFound.Includes(result)) Log($"Can't move file {currentPath}>{newPath}: The original file to be renamed cannot be found. There are no directories within the path after it was renamed.");
      if(nn.fs.FileSystem.ResultPathAlreadyExists.Includes(result)) LogError($"Can't move file {currentPath}>{newPath}: The renamed file already exists.");
      if(nn.fs.FileSystem.ResultTargetLocked.Includes(result)) LogError($"Can't move file {currentPath}>{newPath}: The original file to be renamed cannot be moved if it is open.");
      result.abortUnlessSuccess();
    }


    private static void DirectoryCreate(string path)
    {
      nn.Result result = nn.fs.Directory.Create(path);
      if(nn.fs.FileSystem.ResultPathAlreadyExists.Includes(result)) Log($"Can't create directory {path}: Already exists");
      if(nn.fs.FileSystem.ResultPathNotFound.Includes(result)) LogError($"Can't create directory {path}: Path not found");
      if(nn.fs.FileSystem.ResultUsableSpaceNotEnough.Includes(result)) LogError($"Can't create directory {path}: Usable space not enough");

      result = nn.fs.FileSystem.Commit(mountName);
      if(nn.fs.FileSystem.ResultTargetLocked.Includes(result)) LogError($"Can't commit mountName {mountName}: Target locked");
      result.abortUnlessSuccess();
    }

    private static void ns_DirectoryDelete(string path)
    {
      nn.Result result = nn.fs.Directory.DeleteRecursively(path);
      if(nn.fs.FileSystem.ResultPathNotFound.Includes(result)) Log($"Can't delete directory {path}: Path not found");
      if(nn.fs.FileSystem.ResultTargetLocked.Includes(result)) LogError($"Can't delete directory {path}: Target locked");
    }

    private static void ns_DirectoryRename(string currentPath, string newPath)
    {
      nn.Result result = nn.fs.Directory.Rename(currentPath, newPath);
      if(nn.fs.FileSystem.ResultPathNotFound.Includes(result)) Log($"Can't move directory {currentPath}>{newPath}: The original file to be renamed cannot be found. There are no directories within the path after it was renamed.");
      if(nn.fs.FileSystem.ResultPathAlreadyExists.Includes(result)) LogError($"Can't move directory {currentPath}>{newPath}: The renamed file already exists.");
      if(nn.fs.FileSystem.ResultTargetLocked.Includes(result)) LogError($"Can't move directory {currentPath}>{newPath}: The original file to be renamed cannot be moved if it is open.");
      result.abortUnlessSuccess();
    }


    private static void ns_WriteToFile(string path, JSONObject json, int bufferSize)
    {
      ConvertJSONtoBytes(json, out byte[] data, bufferSize);
      WriteToFile(path, data);
    }

    private static void WriteToFile(string path, byte[] data)
    {
      nn.Result result;
      nn.fs.FileHandle fileHandle = new nn.fs.FileHandle();

      //Log($"Write to file ({path}): Gonna open ");

      result = nn.fs.File.Open(ref fileHandle, path, nn.fs.OpenFileMode.Write);
      if(nn.fs.FileSystem.ResultPathNotFound.Includes(result)) Log($"Can't open file {path}: Path not found");
      if(nn.fs.FileSystem.ResultTargetLocked.Includes(result)) LogError($"Can't open file {path}: Target locked");
      result.abortUnlessSuccess();

      //Log($"Write to file ({path}): Gonna write ");

      result = nn.fs.File.Write(fileHandle, 0, data, data.LongLength, nn.fs.WriteOption.Flush);
      if(nn.fs.FileSystem.ResultUsableSpaceNotEnough.Includes(result)) Log($"Can't write ({data.Length}) on file {path}: Usable space not enough");
      result.abortUnlessSuccess();

      nn.fs.File.Close(fileHandle);

      //Log($"Write to file ({path}): Gonna commit ");

      result = nn.fs.FileSystem.Commit(mountName);
      if(nn.fs.FileSystem.ResultTargetLocked.Includes(result)) LogError($"Can't commit mountName {mountName}: Target locked");
      result.abortUnlessSuccess();
    }

    private static void ReadFromFile(string path, out byte[] data)
    {
      nn.Result result;
      nn.fs.FileHandle fileHandle = new nn.fs.FileHandle();
      long fileSize = 0;

      //Log($"Read from file ({path}): Gonna open");

      nn.fs.File.Open(ref fileHandle, path, nn.fs.OpenFileMode.Read).abortUnlessSuccess();
      nn.fs.File.GetSize(ref fileSize, fileHandle).abortUnlessSuccess();

      //Log($"Read from file ({path}): Gonna read");

      data = new byte[fileSize];
      nn.fs.File.Read(fileHandle, 0, data, fileSize).abortUnlessSuccess();

      nn.fs.File.Close(fileHandle);
    }

    private static void ns_ListFilesInDirectory(string path, out string[] fileNames)
    {
      long numberOfFiles = -1;
      long entryBufferCount = 20;
      nn.fs.DirectoryEntry[] entryBuffer = new nn.fs.DirectoryEntry[entryBufferCount];
      nn.fs.DirectoryHandle handle = new nn.fs.DirectoryHandle();
      nn.Result result;

      nn.fs.Directory.Open(ref handle, path, nn.fs.OpenDirectoryMode.File);
      nn.fs.Directory.Read(ref numberOfFiles, entryBuffer, handle, entryBufferCount);

      // Save the names
      fileNames = new string[numberOfFiles];
      for(int i = 0; i < numberOfFiles; i++)
      {
        fileNames[i] = entryBuffer[i].name;
      }

      nn.fs.Directory.Close(handle);
    }


    public static void ns_DirectoryCopy(string originPath, string targetPath)
    {
      Log($"ns_DirectoryCopy 1");
      // Check if origin exists
      if(!CheckIfPathExists(originPath))
      {
        Log($"Copy: Couldn't find origin slot ({originPath})");
        return;
      }

      Log($"ns_DirectoryCopy 2");

      // Remove goal if exists
      if(CheckIfPathExists(targetPath))
        ns_DirectoryDelete(targetPath);

      Log($"ns_DirectoryCopy 3");

      DirectoryCreate(targetPath);

      Log($"ns_DirectoryCopy 4");

      // Second, find files in main
      ns_ListFilesInDirectory(originPath, out string[] fileNames);

      Log($"ns_DirectoryCopy 5");

      if(fileNames.Length == 0)
      {
        Log($"Copy: Origin folder is empty ({originPath})");
        return;
      }

      for(int i = 0; i < fileNames.Length; i++)
      {
        byte[] data;
        string originFilePath = $"{originPath}{fileNames[i]}";
        string goalFilePath = $"{targetPath}{fileNames[i]}";

        Log($"ns_DirectoryCopy loop 1 {originFilePath}");

        //  open file, read contents, close
        ReadFromFile(originFilePath, out data);

        Log($"ns_DirectoryCopy loop 2 {originFilePath}");

        //  write contents in backup folder file
        FileCreate(goalFilePath, loadBufferSize);

        Log($"ns_DirectoryCopy loop 3 {originFilePath}");

        WriteToFile(goalFilePath, data);


      }
    }

    public static void ns_DirectoryReadFromStreamingAssets(string origin, string target)
    {
      // Remove goal if exists
      if(CheckIfPathExists(target))
        ns_DirectoryDelete(target);

      DirectoryCreate(target);

      // Second, find files in main
      ListFilesInDirectory(origin, out List<string> fileNames);

      if(fileNames.Count == 0)
      {
        Log($"Copy: Origin folder is empty ({origin})");
        return;
      }

      for(int i = 0; i < fileNames.Count; i++)
      {
        JSONObject json;
        byte[] data;

        string originFilePath = $"{origin}{fileNames[i]}";
        string goalFilePath = $"{target}{fileNames[i]}";

        //Log($"Copy: Gonna copy file {originFilePath} to ({goalFilePath})");

        ReadFromFile(originFilePath, out json);

        if(!CheckIfPathExists(goalFilePath))
          FileCreate(goalFilePath, loadBufferSize);

        ns_WriteToFile(goalFilePath, json, loadBufferSize);
      }
    }


    private static void ns_DirectoryMove(string originPath, string targetPath)
    {
      if(!CheckIfPathExists(originPath))
      {
        Log($"Copy: Couldn't find origin slot ({originPath})");
        return;
      }

      // Remove goal if exists
      if(CheckIfPathExists(targetPath))
        ns_DirectoryDelete(targetPath);

      // Move
      ns_DirectoryRename(originPath, targetPath);
    }

        private static void ConvertStringToBytes(string text, out byte[] data, int bufferSize)
        {
            using (MemoryStream stream = new MemoryStream(bufferSize))
            {
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(text);
                stream.Close();
                data = stream.GetBuffer();
                //Log($"Converting json data to byte array, for {fileName} with buffer size {data.Length} (expected {bufferSize})");
                Debug.Assert(data.Length == bufferSize, "Byte array and buffer are of different sizes.");
            }
        }

    private static void ConvertJSONtoBytes(JSONObject json, out byte[] data, int bufferSize)
    {
      using(MemoryStream stream = new MemoryStream(bufferSize))
      {
        BinaryWriter writer = new BinaryWriter(stream);
        writer.Write(json.ToString());
        stream.Close();
        data = stream.GetBuffer();
        //Log($"Converting json data to byte array, for {fileName} with buffer size {data.Length} (expected {bufferSize})");
        Debug.Assert(data.Length == bufferSize, "Byte array and buffer are of different sizes.");
      }
    }

    private static void ConvertBytesToJSON(byte[] data, out JSONObject json)
    {
      string jsonString;

      using(MemoryStream stream = new MemoryStream(data))
      {
        BinaryReader reader = new BinaryReader(stream);
        jsonString = reader.ReadString();
      }


      try
      {
        json = JSON.Parse(jsonString) as JSONObject;
      }
      catch
      {
        // Si hemos importado la partida, no podemos leerla como stream de bytes
        // y tenemos que leerla como ASCII normal y corriente
        jsonString = System.Text.Encoding.ASCII.GetString(data);
        json = JSON.Parse(jsonString) as JSONObject;
      }
    }

#endregion


#region Final

    public static void ReadJSONFromFile(out JSONObject json, string filePath)
    {
      if(!mounted)
      {
        Mount();
      }

      UnityEngine.Switch.Notification.EnterExitRequestHandlingSection(); // Nintendo Switch Guideline 0080

      if(!CheckIfPathExists(filePath))
      {
        Log($"File {filePath} doesn't exist, returning empty.");
        UnityEngine.Switch.Notification.LeaveExitRequestHandlingSection(); // Nintendo Switch Guideline 0080
        json = null;
        return;
      }

      ReadFromFile(filePath, out byte[] data);
      ConvertBytesToJSON(data, out json);

      UnityEngine.Switch.Notification.LeaveExitRequestHandlingSection(); // Nintendo Switch Guideline 0080

    }

    public static void WriteJSONToFile(JSONObject json, string filePath)
    {
      if(!mounted)
      {
        Mount();
      }

      ConvertJSONtoBytes(json, out byte[] data, loadBufferSize);

      UnityEngine.Switch.Notification.EnterExitRequestHandlingSection(); // Nintendo Switch Guideline 0080

      string folderPath = Directory.GetParent(filePath).FullName;
      folderPath = folderPath.Replace("/rom:/", "");

      if(!CheckIfPathExists(folderPath))
        DirectoryCreate(folderPath);

      if(!CheckIfPathExists(filePath))
      {
        if(!FileCreate(filePath, loadBufferSize)) // este puede fallar, pero he comentado lo de abortar
        {
          Log($"Couldn't create file {filePath} when trying to save. Aborting.");
          UnityEngine.Switch.Notification.LeaveExitRequestHandlingSection(); // Nintendo Switch Guideline 0080
          return;
        }
      }
      // 4) Save data
      WriteToFile(filePath, data);

      UnityEngine.Switch.Notification.LeaveExitRequestHandlingSection(); // Nintendo Switch Guideline 0080

    }

        public static void WriteStringToFile(string text, string filePath)
        {
            if (!mounted)
            {
                Mount();
            }

            ConvertStringToBytes(text, out byte[] data, loadBufferSize);

            UnityEngine.Switch.Notification.EnterExitRequestHandlingSection(); // Nintendo Switch Guideline 0080

            string folderPath = Directory.GetParent(filePath).FullName;
            folderPath = folderPath.Replace("/rom:/", "");

            Debug.Log($"aaaa 1 filePath {filePath}");
            Debug.Log($"aaaa 1 folderPath {folderPath}");

            if (!CheckIfPathExists(folderPath))
                DirectoryCreate(folderPath);

            Debug.Log($"aaaa 2");

            if (!CheckIfPathExists(filePath))
            {
                if (!FileCreate(filePath, loadBufferSize)) // este puede fallar, pero he comentado lo de abortar
                {
                    Log($"Couldn't create file {filePath} when trying to save. Aborting.");
                    UnityEngine.Switch.Notification.LeaveExitRequestHandlingSection(); // Nintendo Switch Guideline 0080
                    return;
                }
            }
            // 4) Save data
            WriteToFile(filePath, data);

            UnityEngine.Switch.Notification.LeaveExitRequestHandlingSection(); // Nintendo Switch Guideline 0080

        }

#endregion
    }
}
#endif