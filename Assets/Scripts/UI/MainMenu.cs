using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public CinemachineCamera vcam;
    
    public void playGame()
    {
        //SceneManager.LoadSceneAsync(0);
        mainMenu.SetActive(false);
        vcam.gameObject.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
