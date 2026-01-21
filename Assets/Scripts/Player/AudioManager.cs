using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public List<AudioClip> audioClips;
        private AudioSource audioSource;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }
    }
}
