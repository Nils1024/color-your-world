using Objects;
using UnityEngine.SceneManagement;

namespace Services
{
    public static class LevelService
    {
        public enum Levels
        {
            City = 1,
            Desert = 2,
            Village = 3,
            Forest = 4,
        }
        
        public static Level CurrentSelectedLevel = null;
        
        public static void LoadLevel(Levels level)
        {
            SceneManager.LoadSceneAsync((int) level);
        }

        public static void LoadMainMenu()
        {
            
        }
    }
}