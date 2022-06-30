using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sony.PS4.SaveData;
using SimpleJSON;
using System.IO;

namespace AutumnYard.Plugins.SonyPlayStation4
{
    public static class File
    {
        private const string mountName = "Save";

        private static InitResult initResult;
        static public System.UInt64 TestBlockSize = Sony.PS4.SaveData.Mounting.MountRequest.BLOCKS_MIN + ((1024 * 1024 * 4) / Sony.PS4.SaveData.Mounting.MountRequest.BLOCK_SIZE);
        private static Mounting.MountPoint mountPoint;
        private static Action onMounted;
        private static bool isNotificationInitialised = false;


        #region Tools

        private static void Log(string text)
        {
            Debug.Log($"<color=cyan>[File]: (PS4) > {text}</color>");
        }

        private static void LogError(string text)
        {
            Debug.LogError($"<color=cyan>[File]:<color=red> (PS4) >  {text}</color></color>");
        }

        #endregion

        #region Internal

        public static void PS4_Initialize()
        {
            try
            {
                PS4_NotificationInitialize();

                Sony.PS4.SaveData.InitSettings settings = new Sony.PS4.SaveData.InitSettings();

                settings.Affinity = Sony.PS4.SaveData.ThreadAffinity.Core5;

                initResult = Sony.PS4.SaveData.Main.Initialize(settings);

                if (initResult.Initialized == true)
                {
                    Log("SaveData Initialized ");
                    Log("Plugin SDK Version : " + initResult.SceSDKVersion.ToString());
                    Log("Plugin DLL Version : " + initResult.DllVersion.ToString());
                }
                else
                {
                    Log("SaveData not initialized ");
                }
            }
            catch (Sony.PS4.SaveData.SaveDataException e)
            {
                LogError("Exception During Initialization : " + e.ExtendedMessage);
            }
#if UNITY_EDITOR
            catch (System.DllNotFoundException e)
            {
                LogError("Missing DLL Expection : " + e.Message);
                LogError("The sample APP will not run in the editor.");
            }
#endif
        }


        private static void PS4_NotificationInitialize()
        {
            Sony.PS4.Dialog.Main.OnLogError += PS4_OnNotificationError;
            Sony.PS4.Dialog.Main.Initialise();
            isNotificationInitialised = true;
        }

        private static void PS4_NotificationShowError(System.UInt32 errorCode, int userId = 0)
        {
            bool success = Sony.PS4.Dialog.Common.ShowErrorMessage(errorCode, userId);
            if (!success)
            {
                LogError("FAILED to show ERROR: " + errorCode);
            }
        }

        private static void PS4_OnNotificationError(Sony.PS4.Dialog.Messages.PluginMessage msg)
        {
            LogError("PS4 Notification ERROR: " + msg.Text);
        }

        private static void PS4_ShowSaveErrorDialog_NoSpace(ulong requiredBlocks)
        {
            try
            {
                Sony.PS4.SaveData.Dialogs.OpenDialogRequest request = new Sony.PS4.SaveData.Dialogs.OpenDialogRequest();
                request.UserId = UnityEngine.PS4.Utility.initialUserId; // Ps4User.GetActiveUserId; //1; // Ps4User.GetActiveUserId;
                request.Mode = Sony.PS4.SaveData.Dialogs.DialogMode.SystemMsg;
                request.DispType = Sony.PS4.SaveData.Dialogs.DialogType.Save;

                var param = new Dialogs.SystemMessageParam(); // Mensaje de tipo NoSpace, que el jugadr no podra continuar si no libera espacio
                param.SysMsgType = Dialogs.SystemMessageType.NoSpace;
                param.Value = requiredBlocks;

                request.SystemMessage = param;
                request.Animations = new Sony.PS4.SaveData.Dialogs.AnimationParam(Sony.PS4.SaveData.Dialogs.Animation.On, Sony.PS4.SaveData.Dialogs.Animation.On);

                Sony.PS4.SaveData.Dialogs.OpenDialogResponse response = new Sony.PS4.SaveData.Dialogs.OpenDialogResponse();
                int requestId = Sony.PS4.SaveData.Dialogs.OpenDialog(request, response);
                Debug.Log("OpenDialog Async : Request Id = " + requestId);
            }
            catch (Sony.PS4.SaveData.SaveDataException e)
            {
                Debug.Log("Exception : " + e.ExtendedMessage);
            }
        }


        public static void PS4_SaveIcon(Sony.PS4.SaveData.Mounting.MountPoint mp)
        {
            try
            {
                Sony.PS4.SaveData.Mounting.SaveIconRequest request = new Sony.PS4.SaveData.Mounting.SaveIconRequest();

                if (mp == null) return;

                request.UserId = UnityEngine.PS4.Utility.initialUserId; // Ps4User.GetActiveUserId; //1; // Ps4User.GetActiveUserId;
                request.MountPointName = mp.PathName;

                request.IconPath = "/app0/Media/StreamingAssets/SaveIcon.png";

                Sony.PS4.SaveData.EmptyResponse response = new Sony.PS4.SaveData.EmptyResponse();

                int requestId = Sony.PS4.SaveData.Mounting.SaveIcon(request, response);

                Log("SaveIcon Async : Request Id = " + requestId);
            }
            catch (Sony.PS4.SaveData.SaveDataException e)
            {
                Log("Exception : " + e.ExtendedMessage);
            }
        }

        public static void PS4_Mount(bool async, bool readWrite)
        {
            try
            {
                Mounting.MountRequest request = new Sony.PS4.SaveData.Mounting.MountRequest();
                DirName dirName = new Sony.PS4.SaveData.DirName();

                dirName.Data = "Fantasia";

                Log("Mounting Directory : " + dirName.Data);

                request.UserId = UnityEngine.PS4.Utility.initialUserId; // Ps4User.GetActiveUserId; //1; // Ps4User.GetActiveUserId;
                request.Async = async;
                request.DirName = dirName;

                if (readWrite == true)
                {
                    request.MountMode = Sony.PS4.SaveData.Mounting.MountModeFlags.Create2 | Sony.PS4.SaveData.Mounting.MountModeFlags.ReadWrite;
                }
                else
                {
                    request.MountMode = Sony.PS4.SaveData.Mounting.MountModeFlags.ReadOnly;
                }

                request.Blocks = TestBlockSize;

                Sony.PS4.SaveData.Mounting.MountResponse response = new Sony.PS4.SaveData.Mounting.MountResponse();

                int requestId = Sony.PS4.SaveData.Mounting.Mount(request, response);

                if (async == true)
                {
                    Log("Mount Async : Request Id = " + requestId);
                }
                else
                {
                    if (response.ReturnCodeValue < 0)
                    {
                        LogError("Mount Sync Error: " + response.ConvertReturnCodeToString(request.FunctionType));

                        if (response.ReturnCode == Sony.PS4.SaveData.ReturnCodes.DATA_ERROR_NO_SPACE_FS)
                        {
                            LogError("Can't save: No free space."); // 0x809F000A
                            PS4_ShowSaveErrorDialog_NoSpace(response.RequiredBlocks);
                            Application.Quit();
                        }
                        else if (response.ReturnCode == Sony.PS4.SaveData.ReturnCodes.SAVE_DATA_ERROR_BROKEN)
                        {
                            LogError("Save data corrupted."); // 0x809F000F
                            PS4_NotificationShowError((uint)response.ReturnCodeValue);
                        }

                        else if (response.ReturnCode == Sony.PS4.SaveData.ReturnCodes.SAVE_DATA_ERROR_NOT_FOUND)
                        {
                            Log("Save data doesnt exist."); // 0x809F0008
                                                            //PS4_NotificationShowError((uint)response.ReturnCodeValue); // Es generico
                        }

                    }
                    else
                    {
                        Log("Mount Sync : " + response.ConvertReturnCodeToString(request.FunctionType));
                    }
                    Ps4_MountReponseOutput(response);
                }
            }
            catch (Sony.PS4.SaveData.SaveDataException e)
            {
                LogError("Exception : " + e.ExtendedMessage);
            }
        }


        private static void Ps4_MountReponseOutput(Sony.PS4.SaveData.Mounting.MountResponse response)
        {


            if (response != null)
            {
                Log("MountPoint : " + response.MountPoint.PathName.Data);
                mountPoint = response.MountPoint;
                Log("RequiredBlocks : " + response.RequiredBlocks);
                Log("WasCreated : " + response.WasCreated);

                PS4_SaveIcon(mountPoint);

                if (onMounted != null) onMounted.Invoke();
            }
        }

        public static void PS4_Unmount(bool async, bool backup)
        {
            try
            {

                Sony.PS4.SaveData.Mounting.UnmountRequest request = new Sony.PS4.SaveData.Mounting.UnmountRequest();

                if (mountPoint == null) return;

                request.UserId = UnityEngine.PS4.Utility.initialUserId;// Ps4User.GetActiveUserId; // 1; // Ps4User.GetActiveUserId;

                request.MountPointName = mountPoint.PathName;
                request.Backup = backup;
                request.Async = async;

                Sony.PS4.SaveData.EmptyResponse response = new Sony.PS4.SaveData.EmptyResponse();

                Log("Unmounting = " + request.MountPointName.Data);

                int requestId = Sony.PS4.SaveData.Mounting.Unmount(request, response);

                Log("Unmount Async : Request Id = " + requestId);
            }
            catch (Sony.PS4.SaveData.SaveDataException e)
            {
                Log("Exception : " + e.ExtendedMessage);
            }
        }

        #endregion


        #region Final

        public static void ReadJSONFromFile(out JSONObject json, string filePath)
        {
            if (initResult.Initialized != true)
            {
                PS4_Initialize();
            }

            PS4_Mount(false, false);

            filePath = string.Format(mountPoint + "/{1}/{2}{3}", mountName, slot.ToString(), fileName, extension);
            Log($"Load {fileName} ({slot}). Path: {filePath}");
            if (System.IO.File.Exists(filePath))
            {
                string jsonString = System.IO.File.ReadAllText(filePath);
                json = JSON.Parse(jsonString) as JSONObject;
            }

            PS4_Unmount(false, false);
        }

        public static void WriteJSONToFile(JSONObject json, string filePath)
        {
            if (initResult.Initialized != true)
            {
                PS4_Initialize();
            }

            PS4_Mount(false, true);

            filePath = string.Format(mountPoint + "/{1}/{2}{3}", mountName, slot.ToString(), fileName, extension);
            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
            System.IO.File.WriteAllText(filePath, json.ToString(2));

            PS4_Unmount(false, false);
        }

        #endregion

    }
}
