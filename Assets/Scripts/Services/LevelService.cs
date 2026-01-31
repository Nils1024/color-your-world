using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

namespace Services
{
    public static class LevelService
    {
        public enum Levels
        {
            None = 0,
            City = 1,
            Desert = 2,
            Forest = 3,
            Village = 4,
        }
        
        public static Levels CurrentSelectedLevel = Levels.None;
        
        public static void LoadLevel(Levels level)
        {
            CurrentSelectedLevel = level;
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene((int) level);
        }
        
        public static void LoadMainMenu()
        {
            DataStoreService.GetDataStoreService().GetSaveData().SaveLevelData(CurrentSelectedLevel);
            CurrentSelectedLevel = Levels.None;
            SceneManager.LoadSceneAsync(0);
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            
            if (DataStoreService.GetDataStoreService().GetSaveData().LoadLevelData(CurrentSelectedLevel))
            {
                Debug.Log("Level Loaded");
            }
            else
            {
                Debug.Log("Level Not Loaded");
            }
        }
    }
}