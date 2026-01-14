using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class Paint : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            InteractRaycast();
        }

        void InteractRaycast()
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
            
                if (mouse.leftButton.wasPressedThisFrame)
                {
                    if(interactionRayHit.collider.TryGetComponent(out Objects.Colorable colorable))
                    {
                        colorable.OnClick();
                    }
                }
            }
        }
    }
}
