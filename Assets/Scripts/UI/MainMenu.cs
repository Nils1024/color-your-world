using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public CinemachineCamera vcam;
    public GameObject mainMenu;
    public GameObject background;

    private bool _playClicked = false;
    private Mouse _mouse = Mouse.current;
    
    public void playGame()
    {
        mainMenu.SetActive(false);
        background.SetActive(false);
        vcam.gameObject.SetActive(true);
        _playClicked = true;
    }

    public void quitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(_playClicked)
        {
            Debug.Log("Mouse raycast");
            RaycastHit hitInfo = new RaycastHit();
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

            var ray = Camera.main.ScreenPointToRay(mouseScreenPos);
            
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
            if(Physics.Raycast(ray, out hitInfo, 100f))
            {
                if(hitInfo.collider.TryGetComponent<LevelData>(out var levelData))
                {
                    levelData.Hover();

                    if (_mouse.leftButton.wasPressedThisFrame)
                    {
                        levelData.Click();
                    }
                }
            }
        }
    }
}
