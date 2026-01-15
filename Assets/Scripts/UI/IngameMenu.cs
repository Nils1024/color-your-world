using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour
{
    public GameObject ingameMenu;
    public Movement playerMovement;
    
    private Keyboard _keyboard = Keyboard.current;
    
    public void ResumeGame()
    {
        ingameMenu.SetActive(false);
        playerMovement.isLocked = false;
    }

    public void ShowLeftoverObject()
    {
        
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync(1);
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
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            
            playerMovement.isLocked = ingameMenu.activeSelf;
        }
    }
}
