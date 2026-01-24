
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
    public static class SaveService
    {
        public const string SAVEDATA_FILENAME = "/DATA.json";

        public static bool SaveGameState()
        {
            string filePathSaveData = Application.persistentDataPath + SAVEDATA_FILENAME;

            if (LevelService.CurrentSelectedLevel != LevelService.Levels.NONE)
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
        [SerializeField] public SerializableDictionary<string, LevelData> levelData;
        
        public SaveData(LevelData levelData)
        {
            this.levelData = new SerializableDictionary<string, LevelData>();
            this.levelData.data.Add(levelData.levelName, levelData);
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

            if (LevelService.CurrentSelectedLevel != LevelService.Levels.NONE)
            {
                Scene currentlyLoadedScene = SceneManager.GetActiveScene();
                
                foreach (GameObject root in currentlyLoadedScene.GetRootGameObjects())
                {
                    colorablesID.AddRange(root.GetComponentsInChildren<Colorable>(true)
                        .Where(c => c.isColored())
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

