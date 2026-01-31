using System.IO;
using UnityEngine;
using Util;

namespace Services
{
    public class DataStoreService : MonoBehaviour
    {
        private const string SaveDataFileName = "/DATA.json";
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
        }

        private void Start()
        {
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
            string filePathSaveData = Application.persistentDataPath + SaveDataFileName;
            
            string txt = JsonUtility.ToJson(_saveData);
            File.WriteAllText(filePathSaveData, txt);
        }

        private void ReadSaveData()
        {
            string filePath = Application.persistentDataPath + DataStoreService.SaveDataFileName;
            string fileContent = File.ReadAllText(filePath);
            _saveData = JsonUtility.FromJson<SaveData>(fileContent);
        }

        public SaveData GetSaveData()
        {
            return _saveData;
        }
    }
}

