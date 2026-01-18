using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class Colorable : MonoBehaviour
    {
        public List<Material> uncoloredMaterials;
        public List<Material> coloredMaterials;

        private Renderer _gameObjectRenderer;
        private bool _isColored;

        private void Awake()
        {
            _gameObjectRenderer = GetComponent<Renderer>();
            _gameObjectRenderer.SetMaterials(uncoloredMaterials);
        }

        public void OnClick()
        {
            if(!_isColored)
            {
                _gameObjectRenderer.SetMaterials(coloredMaterials);
                _isColored = true;
            }
        }

        public bool isColored()
        {
            return _isColored;
        }
    }
}
