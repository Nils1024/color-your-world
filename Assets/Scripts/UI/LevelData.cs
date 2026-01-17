using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelData : MonoBehaviour
{
    public int sceneNumber;
    public Material baseMaterial;
    public Material hoverMaterial;
    
    private Renderer _gameObjectRenderer;
    private List<Material> _materials = new List<Material>();

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

    public void Click()
    {
        SceneManager.LoadSceneAsync(sceneNumber);
    }
}
