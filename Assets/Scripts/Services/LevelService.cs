using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public static class LevelService
    {
        public enum Levels
        {
            None = 0,
            City = 1,
            Desert = 2,
            Village = 3,
            Forest = 4,
        }
        
        public static Levels CurrentSelectedLevel = Levels.None;
        
        public static void LoadLevel(Levels level)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene((int) level);
            CurrentSelectedLevel = level;
        }
        
        public static void LoadMainMenu()
        {
            DataStoreService.SaveData.SaveLevelData(CurrentSelectedLevel);
            DataStoreService.WriteSaveData();
            CurrentSelectedLevel = Levels.None;
            SceneManager.LoadSceneAsync(0);
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            
            DataStoreService.ReadSaveData();
            if (DataStoreService.SaveData.LoadLevelData(CurrentSelectedLevel))
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