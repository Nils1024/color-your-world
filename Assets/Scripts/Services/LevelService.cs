using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class LevelService : MonoBehaviour
    {
        private static LevelService _instance;
        
        public enum Levels
        {
            None = 0,
            City = 1,
            Desert = 2,
            Forest = 3,
            Village = 4,
        }
        
        public static Levels CurrentSelectedLevel = Levels.None;
        
        public static LevelService GetLevelService()
        {
            return _instance;
        }
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        
        public void LoadLevel(Levels level)
        {
            CurrentSelectedLevel = level;
            StartCoroutine(LoadScene((int) level));
        }
        
        public void LoadMainMenu()
        {
            DataStoreService.GetDataStoreService().GetSaveData().SaveLevelData(CurrentSelectedLevel);
            CurrentSelectedLevel = Levels.None;
            StartCoroutine(LoadScene(0));
        }

        IEnumerator LoadScene(int index)
        {
            TransitionService.GetTransitionService().transitionAnimator.SetTrigger("End");
            yield return new WaitForSeconds(1);

            if (index > 0)
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            
            AsyncOperation op = SceneManager.LoadSceneAsync(index);
            yield return op;
            
            TransitionService.GetTransitionService().transitionAnimator.SetTrigger("Start");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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