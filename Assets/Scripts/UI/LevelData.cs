using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelData : MonoBehaviour
{
    public int sceneNumber;
    public Material baseMaterial;
    public Material hoverMaterial;

    public void Hover()
    {
        Renderer _gameObjectRenderer = gameObject.GetComponent<Renderer>();
        
        List<Material> newMaterials = new List<Material>();
        newMaterials.Add(baseMaterial);
        newMaterials.Add(hoverMaterial);
        
        _gameObjectRenderer.SetMaterials(newMaterials);
    }

    public void Click()
    {
        SceneManager.LoadSceneAsync(sceneNumber);
    }
}
