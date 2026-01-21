
using System;
using System.IO;
using Objects;
using UnityEngine;

namespace Services
{
    public static class SaveService
    {
        public const string SAVEDATA_FILENAME = "/DATA.json";

        public static bool SaveGameState()
        {
            string filePathSaveData = Application.persistentDataPath + SAVEDATA_FILENAME;

            if (LevelService.CurrentSelectedLevel != null)
            {
                LevelData levelData = new LevelData(LevelService.CurrentSelectedLevel);
                SaveData saveData = new SaveData(levelData);
            
                string txt = JsonUtility.ToJson(saveData);
                File.WriteAllText(filePathSaveData, txt);
                
                return true;
            }
            
            return false;
        }
    }

    [Serializable]
    public class SaveData
    {
        [SerializeField] LevelData _levelData;
        
        public SaveData(LevelData levelData)
        {
            _levelData = levelData;
        }
    }
    
    [Serializable]
    public class LevelData
    {
        [SerializeField] string levelName;
        [SerializeField] double elapsedTime;
        [SerializeField] object coloredObjects;

        public LevelData(Level level)
        {
            
        }
    }
}

