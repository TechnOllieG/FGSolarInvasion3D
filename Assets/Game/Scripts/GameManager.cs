using UnityEngine;

namespace FG
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        public static Camera PlayerCamera { get; private set; }
<<<<<<< HEAD
=======
        public static GameObject Player { get; private set; }
>>>>>>> 20a66d1a13151c70b8c5d2739206a4bc29049d41

        private void Awake()
        {
            PlayerCamera = Camera.main;
<<<<<<< HEAD
=======
            Player = GameObject.FindWithTag("Player");
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
>>>>>>> 20a66d1a13151c70b8c5d2739206a4bc29049d41
        }
    }
}
