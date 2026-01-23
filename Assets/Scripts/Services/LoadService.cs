using System.Collections.Generic;
using System.IO;
using System.Linq;
using Objects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public static class LoadService
    {
        public static bool LoadGameState()
        {
            try
            {
                string filePath = Application.persistentDataPath + SaveService.SAVEDATA_FILENAME;
                string fileContent = File.ReadAllText(filePath);
                SaveData saveData = JsonUtility.FromJson<SaveData>(fileContent);
                
                Scene currentlyLoadedScene = SceneManager.GetActiveScene();

                List<Colorable> alreadyColored = new List<Colorable>();
                
                foreach (GameObject root in currentlyLoadedScene.GetRootGameObjects())
                {
                    alreadyColored.AddRange(root.GetComponentsInChildren<Colorable>(true)
                        .Where(c => saveData.levelData.colorablesID.Contains(c.UniqueId)));
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
}