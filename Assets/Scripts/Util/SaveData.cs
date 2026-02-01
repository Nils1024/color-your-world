using System;
using System.Collections.Generic;
using System.Linq;
using Objects;
using Services;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util
{
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
                GameObject[] rootObjects = currentlyLoadedScene.GetRootGameObjects();
                
                foreach (GameObject root in rootObjects)
                {
                    alreadyColored.AddRange(root.GetComponentsInChildren<Colorable>(true)
                        .Where(c => levelData.data[level.ToString()].colorablesID.Contains(c.UniqueId)));
                }

                foreach (Colorable colorable in alreadyColored)
                {
                    colorable.Color();
                }

                foreach (GameObject root in rootObjects)
                {
                    Timer timer = root.GetComponentInChildren<Timer>(true);

                    if (timer != null)
                    {
                        timer.elapsedTime = levelData.data[level.ToString()].elapsedTime;
                        break;
                    }
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
        [SerializeField] public float elapsedTime;
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