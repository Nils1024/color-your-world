using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Objects
{
    public class Colorable : MonoBehaviour
    {
        [SerializeField] private string uniqueColorableId;
        public string UniqueId => uniqueColorableId;
        
        public List<Material> uncoloredMaterials;
        public List<Material> coloredMaterials;
        public Material highlightedMaterial;
        public bool colorAllOtherObjects;

        private Renderer _gameObjectRenderer;
        private bool _isColored;

        private void Awake()
        {
            _gameObjectRenderer = GetComponent<Renderer>();
            _gameObjectRenderer.SetMaterials(uncoloredMaterials);
        }
        
        #if UNITY_EDITOR
            private void OnValidate()
            {
                uniqueColorableId = Guid.NewGuid().ToString();
                EditorUtility.SetDirty(this);
                
                if (string.IsNullOrEmpty(uniqueColorableId))
                {
                    
                }
                
                if (highlightedMaterial == null)
                {
                    Material mat = AssetDatabase.LoadAssetAtPath<Material>(
                        "Assets/Materials/Special/SimpleOutline - Yellow.mat"
                    );

                    if (mat != null)
                    {
                        highlightedMaterial = mat;
                        EditorUtility.SetDirty(this);
                    }
                    else
                    {
                        Debug.LogWarning("Material not found at path! Check the spelling.");
                    }
                }
            }
        #endif

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
            gameObject.layer = 0;
        }

        public void Highlight()
        {
            List<Material> highlightedMaterials = new List<Material>(uncoloredMaterials);

            if (highlightedMaterials.Count > 1)
            {
                highlightedMaterials.RemoveAt(1);
            }
            
            highlightedMaterials.Add(highlightedMaterial);
            
            _gameObjectRenderer.SetMaterials(highlightedMaterials);
            gameObject.layer = 3;
            
            Debug.Log(gameObject.name);
        }

        public bool IsColored()
        {
            return _isColored;
        }
    }
}
