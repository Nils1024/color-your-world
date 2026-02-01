using UnityEngine;

namespace Services
{
    public class TransitionService : MonoBehaviour
    {
        [SerializeField] public Animator transitionAnimator;
        
        private static TransitionService _instance;

        public static TransitionService GetTransitionService()
        {
            return _instance;
        }
        
        private void Start()
        {
            if (_instance != null)
            {
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}