using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    public class Level : MonoBehaviour
    {
        public int sceneNumber;
        public String levelName;
        public Material baseMaterial;
        public Material hoverMaterial;
    
        private Renderer _gameObjectRenderer;
        private readonly List<Material> _materials = new List<Material>();

        private void Awake()
        {
            _gameObjectRenderer = gameObject.GetComponent<Renderer>();
        
            _materials.Clear();
            _materials.Add(baseMaterial);
            _gameObjectRenderer.SetMaterials(_materials);
        }

        public void Hover()
        {
            _materials.Clear();
            _materials.Add(baseMaterial);
            _materials.Add(hoverMaterial);
        
            _gameObjectRenderer.SetMaterials(_materials);
        }
    
        public void Unhover()
        {
            _materials.Clear();
            _materials.Add(baseMaterial);
        
            _gameObjectRenderer.SetMaterials(_materials);
        }

        public void Load()
        {
            SceneManager.LoadSceneAsync(sceneNumber);
        }
    }
}
