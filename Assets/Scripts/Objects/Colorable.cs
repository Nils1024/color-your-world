using System.Collections.Generic;
using UnityEngine;

namespace Objects
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
                Color();
                
                if(colorAllOtherObjects)
                {
                    foreach (Transform child in transform.parent)
                    {
                        if(child.gameObject == gameObject)
                            continue;
                    
                        CheckAndPropagate(child);
                    }
                }
            }
        }

        private void CheckAndPropagate(Transform target)
        {
            if (target.TryGetComponent(out Colorable targetColorable))
            {
                if (targetColorable.colorAllOtherObjects)
                {
                    targetColorable.OnClick();
                }
            }
            else
            {
                foreach (Transform child in target)
                {
                    CheckAndPropagate(child);
                }
            }
        }

        public void Color()
        {
            _gameObjectRenderer.SetMaterials(coloredMaterials);
            _isColored = true;
        }

        public bool isColored()
        {
            return _isColored;
        }
    }
}
