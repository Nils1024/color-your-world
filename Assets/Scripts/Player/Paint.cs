using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    public Material newMaterial;

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
        RaycastHit interactionRayHit;
        float interactionRayLength = 5.0f;

        // Debugging
        Vector3 interactionRayEndpoint = playerPosition + forwardDirection * interactionRayLength;
        Debug.DrawLine(playerPosition, interactionRayEndpoint);

        bool isHit = Physics.Raycast(interactionRay, out interactionRayHit, interactionRayLength);

        if(isHit)
        {
            GameObject hitGameObject = interactionRayHit.transform.gameObject;

            MeshRenderer gameObjectRenderer = hitGameObject.GetComponent<MeshRenderer>();

            List<Material> newMaterials = new List<Material>();
            newMaterials.Add(newMaterial);
            
            gameObjectRenderer.SetMaterials(newMaterials);
        }
    }
}
