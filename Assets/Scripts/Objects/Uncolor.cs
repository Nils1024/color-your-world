using System;
using UnityEngine;

public class Uncolor : MonoBehaviour
{
    private void Awake()
    {
        foreach(GameObject child in transform)
        {
            child.GetComponent<Renderer>();
        }
    }
}
