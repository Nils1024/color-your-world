
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Objects;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

namespace Services
{
    public static class DataStoreService
    {
        public const string SAVEDATA_FILENAME = "/DATA.json";
        public static SaveData SaveData = new();

        public static void WriteSaveData()
        {
            string filePathSaveData = Application.persistentDataPath + SAVEDATA_FILENAME;
            
            string txt = JsonUtility.ToJson(SaveData);
            File.WriteAllText(filePathSaveData, txt);
        }

        public static void ReadSaveData()
        {
            string filePath = Application.persistentDataPath + DataStoreService.SAVEDATA_FILENAME;
            string fileContent = File.ReadAllText(filePath);
            SaveData = JsonUtility.FromJson<SaveData>(fileContent);
        }
    }

    [Serializable]
    public class SaveData
    {
        [SerializeField] public SerializableDictionary<string, LevelData> levelData = new();

        public void SaveLevelData(LevelService.Levels level)
        {
            if(level == LevelService.Levels.None)
                return;
                
            LevelData data = new LevelData(level);
            
            levelData.data[data.levelName] = data;
        }

        public void DeleteLevelData(LevelService.Levels level)
        {
            if(level == LevelService.Levels.None)
                return;
            
            levelData.data.Remove(level.ToString());
        }

        public bool LoadLevelData(LevelService.Levels level)
        {
            if(level == LevelService.Levels.None)
                return false;
            
            try
            {
                Scene currentlyLoadedScene = SceneManager.GetActiveScene();

                List<Colorable> alreadyColored = new List<Colorable>();
                
                foreach (GameObject root in currentlyLoadedScene.GetRootGameObjects())
                {
                    alreadyColored.AddRange(root.GetComponentsInChildren<Colorable>(true)
                        .Where(c => levelData.data[level.ToString()].colorablesID.Contains(c.UniqueId)));
                }

                foreach (Colorable colorable in alreadyColored)
                {
                    colorable.Color();
                }
                
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    
    [Serializable]
    public class LevelData
    {
        public string levelName;
        [SerializeField] public double elapsedTime;
        [SerializeField] public List<String> colorablesID;

        public LevelData(LevelService.Levels level)
        {
            colorablesID = new List<String>();
            levelName = level.ToString();

            if (LevelService.CurrentSelectedLevel != LevelService.Levels.None)
            {
                Scene currentlyLoadedScene = SceneManager.GetActiveScene();
                
                foreach (GameObject root in currentlyLoadedScene.GetRootGameObjects())
                {
                    colorablesID.AddRange(root.GetComponentsInChildren<Colorable>(true)
                        .Where(c => c.IsColored())
                        .Select(c => c.UniqueId));
                    
                    var timer = root.GetComponentInChildren<Timer>(true);
                    if (timer != null)
                    {
                        elapsedTime = timer.elapsedTime;
                    }
                }
            }
        }
    }
}

