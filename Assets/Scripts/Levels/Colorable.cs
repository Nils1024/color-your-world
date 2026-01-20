using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class Colorable : MonoBehaviour
    {
        public List<Material> uncoloredMaterials;
        public List<Material> coloredMaterials;
        public bool colorAllOtherObjects;

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
                
                if(colorAllOtherObjects)
                {
                    foreach (Transform child in transform.parent)
                    {
                        if(child.gameObject == gameObject)
                            continue;
                    
                        if (child.TryGetComponent(out Colorable childColorable))
                        {
                            if (childColorable.colorAllOtherObjects)
                            {
                                childColorable.OnClick();
                            }
                        }
                    }
                }
            }
        }

        public bool isColored()
        {
            return _isColored;
        }
    }
}
