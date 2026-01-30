using TMPro;
using UnityEngine;
using Util;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        public float elapsedTime;
        
        private TextMeshProUGUI _timerText;
        private bool _isStopped;

        private void Awake()
        {
            _timerText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (!_isStopped)
            {
                elapsedTime += Time.deltaTime;
                _timerText.text = Tools.timeFloatToString(elapsedTime);
            }
        }
    
        public void Stop()
        {
            _isStopped = true;
        }
    
        public void Resume()
        {
            _isStopped = false;
        }
    }
}
