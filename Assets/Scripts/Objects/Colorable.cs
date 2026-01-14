using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class Colorable : MonoBehaviour
    {
        public bool isColorable = true;
        public Material material;

        private Renderer _gameObjectRenderer;

        private void Awake()
        {
            _gameObjectRenderer = GetComponent<Renderer>();
        }

        public void OnClick()
        {
            if (isColorable)
            {
                List<Material> newMaterials = new List<Material>();
                newMaterials.Add(material);

                _gameObjectRenderer.SetMaterials(newMaterials);
            }
        }
    }
}
