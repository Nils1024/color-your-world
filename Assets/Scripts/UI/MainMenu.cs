using Levels;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public CinemachineCamera vcam;
        public GameObject mainMenu;
        public GameObject levelSelectMenu;
        public GameObject background;

        private bool _playClicked;
        private bool _levelClicked;
        private Level _currentHovered;

        private void Awake()
        {
            mainMenu.SetActive(true);
            background.SetActive(true);
        }

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

        public void LoadLevel()
        {
            _currentHovered.Load();
        }

        public void ShowLevelLeaderboard()
        {
            
        }

        public void ResetLevel()
        {
            
        }

        public void CloseLevelMenu()
        {
            _levelClicked = false;
            levelSelectMenu.SetActive(false);
        }

        private void Update()
        {
            if(!_playClicked)
                return;

            if(_levelClicked)
                return;
            
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
            
            if(Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                if(hit.collider.TryGetComponent<Level>(out var levelData))
                {
                    if (_currentHovered != levelData)
                    {
                        _currentHovered?.Unhover();
                        _currentHovered = levelData;
                        _currentHovered.Hover();
                    }
                    
                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        _levelClicked = true;
                        levelSelectMenu.SetActive(true);
                    }
                    
                    return;
                }
            }
            
            if(_currentHovered != null)
            {
                _currentHovered.Unhover();
                _currentHovered = null;
            }
        }
    }
}
