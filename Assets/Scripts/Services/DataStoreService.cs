using System.IO;
using UnityEngine;
using Util;

namespace Services
{
    public class DataStoreService : MonoBehaviour
    {
        private const string PlayerPrefsDataKey = "Color-Your-World-SaveData";
        private SaveData _saveData = new();
        private static DataStoreService _instance;

        public static DataStoreService GetDataStoreService()
        {
            return _instance;
        }
        
        private void Start()
        {
            ReadSaveData();
            
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
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
            PlayerPrefs.SetString(PlayerPrefsDataKey, JsonUtility.ToJson(_saveData));
        }

        private void ReadSaveData()
        {
            _saveData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(PlayerPrefsDataKey));
        }

        public SaveData GetSaveData()
        {
            return _saveData;
        }
    }
}

