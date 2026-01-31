using System.Collections.Generic;
using Objects;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class LevelSelector : MonoBehaviour
    {
        public Material hoverMaterial;
        public GameObject levelSelectMenu;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI timerText;
        
        private bool _levelClicked;
        private Level _currentHovered;
        private Material _currentHoveredBaseMaterial;
        
        private void Awake()
        {
            levelSelectMenu.SetActive(false);
            levelText.gameObject.SetActive(false);
            timerText.gameObject.SetActive(false);
        }
        
        private void Update()
        {
            if(_levelClicked)
                return;
            
            if(Util.RaycastCreator.RayCastFromCamera(Camera.main, 100f, out var hit, true))
            {
                if(hit.collider.TryGetComponent<Level>(out var levelData))
                {
                    if (_currentHovered != levelData)
                    {
                        if (_currentHovered != null)
                        {
                            Unhover(_currentHovered.gameObject);
                        }
                        
                        _currentHovered = levelData;
                        Hover(_currentHovered.gameObject);
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
                Unhover(_currentHovered.gameObject);
                levelText.gameObject.SetActive(false);
                timerText.gameObject.SetActive(false);
                _currentHovered = null;
                _currentHoveredBaseMaterial = null;
            }
        }
        
        public void LoadLevel()
        {
            Debug.Log(_currentHovered.level);
            LevelService.LoadLevel(_currentHovered.level);
        }

        public void ShowLevelLeaderboard()
        {
            Debug.Log("Leaderboard not implemented yet");
        }

        public void ResetLevel()
        {
            DataStoreService.GetDataStoreService().GetSaveData().DeleteLevelData(_currentHovered.level);
        }

        public void CloseLevelMenu()
        {
            _levelClicked = false;
            levelSelectMenu.SetActive(false);
        }

        private void Hover(GameObject target)
        {
            Renderer gameObjectRenderer = target.GetComponent<Renderer>();
            _currentHoveredBaseMaterial = gameObjectRenderer.material;
            
            gameObjectRenderer.SetMaterials(new List<Material>{_currentHoveredBaseMaterial, hoverMaterial});
            
            levelText.gameObject.SetActive(true);
            timerText.gameObject.SetActive(true);
            levelText.SetText(_currentHovered.level.ToString());
        }
    
        private void Unhover(GameObject target)
        {
            Renderer gameObjectRenderer = target.GetComponent<Renderer>();
            
        
            gameObjectRenderer.SetMaterials(new List<Material>{_currentHoveredBaseMaterial});
        }
    }
}