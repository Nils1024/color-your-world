using Unity.Cinemachine;
using UnityEngine;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public CinemachineCamera vcam;
        public GameObject levelSelection;
        public GameObject settingsMenu;
        
        private CinemachineSplineDolly _spline;
        private bool _playClicked;
        private bool _reachedSplineEnd;

        private void Awake()
        {
            _spline = vcam.GetComponent<CinemachineSplineDolly>();
        }

        public void PlayGame()
        {
            vcam.gameObject.SetActive(true);
            _playClicked = true;

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        public void OpenSettings()
        {
            Debug.Log("open settings not implemented yet");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void Update()
        {
            if (!_playClicked)
            {
                return;
            }
            
            if (_spline.CameraPosition >= 1.0f)
            {
                _reachedSplineEnd = true;
            }
            
            if (_reachedSplineEnd && _playClicked)
            {
                gameObject.SetActive(false);
                levelSelection.SetActive(true);
                _playClicked = false;
            }
        }
    }
}
