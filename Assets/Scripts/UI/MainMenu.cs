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
    private LevelData _currentHovered;
    
    public void PlayGame()
    {
        mainMenu.SetActive(false);
        background.SetActive(false);
        vcam.gameObject.SetActive(true);
        _playClicked = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(!_playClicked)
            return;
        
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
        
        if(Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if(hit.collider.TryGetComponent<LevelData>(out var levelData))
            {
                if (_currentHovered != levelData)
                {
                    _currentHovered?.Unhover();
                    _currentHovered = levelData;
                    _currentHovered.Hover();
                }
                
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    levelData.Click();
                }
                
                return;
            }
        }
        
        if (_currentHovered != null)
        {
            _currentHovered.Unhover();
            _currentHovered = null;
        }
    }
}
