using System.Collections;
using Objects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Paint : MonoBehaviour
    {
        // Update is called once per frame
        private void Update()
        {
            InteractRaycast();
        }

        private void InteractRaycast()
        {
            Vector3 playerPosition = transform.position;
            Vector3 forwardDirection = transform.forward;

            Ray interactionRay = new Ray(playerPosition, forwardDirection);
            float interactionRayLength = 5.0f;

            // Debugging
            Vector3 interactionRayEndpoint = playerPosition + forwardDirection * interactionRayLength;
            Debug.DrawLine(playerPosition, interactionRayEndpoint);

            bool isHit = Physics.Raycast(interactionRay, out RaycastHit interactionRayHit, interactionRayLength);

            if(isHit)
            {
                var mouse = Mouse.current;
            
                if (mouse.leftButton.isPressed)
                {
                    if(interactionRayHit.collider.TryGetComponent(out Colorable colorable))
                    {
                        if (!colorable.IsColored())
                        {
                            CreateLine(
                                transform.position - new Vector3(0, 0.5f, 0),
                                interactionRayHit.point - interactionRayHit.normal * 0.02f,
                                colorable.coloredMaterials[0]);
                        }
                        
                        colorable.OnClick();
                    }
                }
            }
        }

        private void CreateLine(Vector3 start, Vector3 end, Material material)
        {
            GameObject line = new GameObject("Line");
            LineRenderer lr = line.AddComponent<LineRenderer>();
            
            lr.material = new Material(material);
            Color c = lr.material.color;
            c.a = 0.3f;
            lr.material.color = c;
            
            lr.positionCount = 2;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.useWorldSpace = true;
            lr.numCapVertices = 16;
            lr.numCornerVertices = 16;
            lr.textureMode = LineTextureMode.Stretch;
            lr.alignment = LineAlignment.View;
            lr.shadowBias = 0f;
            
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);

            StartCoroutine(FadeAndDestroyLine(lr, 0.25f));
        }

        private IEnumerator FadeAndDestroyLine(LineRenderer lr, float duration)
        {
            float time = 0f;

            Color startColor = lr.material.color;
            Color endColor = startColor;
            endColor.a = 0f;

            while (time < duration)
            {
                float t = time / duration;
                lr.material.color = Color.Lerp(startColor, endColor, t);
                time += Time.deltaTime;
                yield return null;
            }

            Destroy(lr.gameObject);
        }
    }
}
