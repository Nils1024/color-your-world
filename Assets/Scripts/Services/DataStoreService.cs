using System.IO;
using UnityEngine;
using Util;

namespace Services
{
    public class DataStoreService : MonoBehaviour
    {
        #if UNITY_EDITOR
            private const string SaveDataFileName = "/DATA.json";
        #endif
        
        private const string PlayerPrefsDataKey = "Color-Your-World-SaveData";
        private SaveData _saveData = new();
        private static DataStoreService _instance;

        public static DataStoreService GetDataStoreService()
        {
            return _instance;
        }
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
            ReadSaveData();
        }
        
        private void OnApplicationQuit()
        {
            WriteSaveData();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                WriteSaveData();
            }
        }

        private void WriteSaveData()
        {
            #if UNITY_EDITOR
                string filePathSaveData = Application.persistentDataPath + SaveDataFileName;
                    
                string txt = JsonUtility.ToJson(_saveData);
                File.WriteAllText(filePathSaveData, txt);
            #else
                PlayerPrefs.SetString(PlayerPrefsDataKey, JsonUtility.ToJson(_saveData));
            #endif
        }

        private void ReadSaveData()
        {
            #if UNITY_EDITOR
                string filePath = Application.persistentDataPath + DataStoreService.SaveDataFileName;

                if (File.Exists(filePath))
                {
                    string fileContent = File.ReadAllText(filePath);
                    _saveData = JsonUtility.FromJson<SaveData>(fileContent);
                }
            #else
                if(PlayerPrefs.HasKey(PlayerPrefsDataKey))
                {
                    _saveData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(PlayerPrefsDataKey));
                }
            #endif
        }

        public SaveData GetSaveData()
        {
            return _saveData;
        }
    }
}

