using System;
using UnityEngine;

namespace Services
{
    public class DataStoreService : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void OnApplicationFocus(bool focus)
        {
            //TODO: Save (It should save when we lose focus to prevent data loss)
        }
        
        private void OnApplicationQuit()
        {
            //TODO: Save
        }
    }
}