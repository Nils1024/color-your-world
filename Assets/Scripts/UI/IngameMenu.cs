using Player;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace  UI
{
    public class IngameMenu : MonoBehaviour
    {
        public GameObject ingameMenu;
        public Movement playerMovement;
        public Timer timer;
    
        private readonly Keyboard _keyboard = Keyboard.current;
    
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

                if(ingameMenu.activeSelf)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    timer.Stop();
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    timer.Resume();
                }
            
                playerMovement.isLocked = ingameMenu.activeSelf;
            }
        }
    }
}
