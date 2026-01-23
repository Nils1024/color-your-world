using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public static class LevelService
    {
        public enum Levels
        {
            NONE = 0,
            City = 1,
            Desert = 2,
            Village = 3,
            Forest = 4,
        }
        
        public static Levels CurrentSelectedLevel = Levels.NONE;
        
        public static void LoadLevel(Levels level)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene((int) level);
            CurrentSelectedLevel = level;
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            
            if (LoadService.LoadGameState())
            {
                Debug.Log("Level Loaded");
            }
            else
            {
                Debug.Log("Level Not Loaded");
            }
        }

        public static void LoadMainMenu()
        {
            
        }
    }
}