using UnityEngine;

public class Paint : MonoBehaviour
{
    public Material newMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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
        RaycastHit interactionrayHit;
        float interactionrayLength = 5.0f;

        Vector3 interactionRayEndpoint = forwardDirection * interactionrayLength;
        Debug.DrawLine(playerPosition, interactionRayEndpoint);

        bool isHit = Physics.Raycast(interactionRay, out interactionrayHit, interactionrayLength);

        if(isHit)
        {
            GameObject hitGameObject = interactionrayHit.transform.gameObject;

            MeshRenderer gameObjectRenderer = hitGameObject.GetComponent<MeshRenderer>();
            gameObjectRenderer.material = newMaterial;
        }
    }
}
