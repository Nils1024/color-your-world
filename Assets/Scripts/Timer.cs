using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI _timerText;
    private float _elapsedTime;
    private bool isStopped;

    private void Awake()
    {
        _timerText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isStopped)
        {
            _elapsedTime += Time.deltaTime;
            
            int totalSeconds = Mathf.FloorToInt(_elapsedTime);
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;

            _timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
    
    public void Stop()
    {
        isStopped = true;
    }
    
    public void Resume()
    {
        isStopped = false;
    }
}
