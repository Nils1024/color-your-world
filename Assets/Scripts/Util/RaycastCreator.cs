using UnityEngine;
using UnityEngine.InputSystem;

namespace Util
{
    public static class RaycastCreator
    {
        public static bool RayCastFromCamera(Camera main, float distance, out RaycastHit hitDest, bool debug)
        {
            Ray ray = main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if(debug)
            {
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.red, 1f);
            }

            return Physics.Raycast(ray, out hitDest, distance);
        }
    }
}
