using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace  UI
{
    public class IngameMenu : MonoBehaviour
    {
        public GameObject ingameMenu;
        public GameObject map;
        public Movement playerMovement;
        public Timer timer;
    
        private Keyboard _keyboard = Keyboard.current;
    
        public void ResumeGame()
        {
            ingameMenu.SetActive(false);
            playerMovement.isLocked = false;
        }
    
        public void BackToMainMenu()
        {
            //TODO: Add saving
            SceneManager.LoadSceneAsync(0);
        }

        public void ShowHint()
        {
        
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
