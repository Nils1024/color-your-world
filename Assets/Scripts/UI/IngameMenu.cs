using System.Linq;
using Objects;
using Player;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Util;

namespace  UI
{
    public class IngameMenu : MonoBehaviour
    {
        public GameObject ingameOverlay;
        public GameObject ingameMenu;
        public Movement playerMovement;
        public TextMeshProUGUI levelNameText;
        public TextMeshProUGUI menuTimerText;
        public TextMeshProUGUI progressText;
        public Timer ingameTimer;
    
        private readonly Keyboard _keyboard = Keyboard.current;

        public void Start()
        {
            levelNameText.text = LevelService.CurrentSelectedLevel.ToString();
        }
        
        public void ResumeGame()
        {
            ingameMenu.SetActive(false);
            playerMovement.isLocked = false;
        }
    
        public void BackToMainMenu()
        {
            LevelService.LoadMainMenu();
        }

        public void ShowHint()
        {
            Debug.Log("Show Hint not implemented yet");
        }

        private void Update()
        {
            if(_keyboard.escapeKey.wasPressedThisFrame)
            {
                ingameMenu.SetActive(!ingameMenu.activeSelf);
                ingameOverlay.SetActive(!ingameOverlay.activeSelf);

                if(ingameMenu.activeSelf)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    ingameTimer.Stop();
                    menuTimerText.text = Tools.timeFloatToString(ingameTimer.elapsedTime);
                    progressText.text = $"{CalculateProgress()}%";
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    ingameTimer.Resume();
                }
            
                playerMovement.isLocked = ingameMenu.activeSelf;
            }
        }

        private int CalculateProgress()
        {
            Scene currentlyLoadedScene = SceneManager.GetActiveScene();

            int allParts = 0;
            int onlyColoredParts = 0;
            
            foreach (GameObject root in currentlyLoadedScene.GetRootGameObjects())
            {
                Colorable[] colorablesInRoot = root.GetComponentsInChildren<Colorable>(true);

                allParts += colorablesInRoot.Length;
                
                onlyColoredParts += colorablesInRoot.Count(c => c.isColored());
            }
            
            return allParts == 0 ? 0 : (int)((float) onlyColoredParts / allParts * 100f);
        }
    }
}
