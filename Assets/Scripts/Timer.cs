using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public String format = "0:00:00";
    
    private TextMeshProUGUI _timerText;
    private float _elapsedTime;

    private void Awake()
    {
        _timerText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        _timerText.text = _elapsedTime.ToString(format);
    }
}
