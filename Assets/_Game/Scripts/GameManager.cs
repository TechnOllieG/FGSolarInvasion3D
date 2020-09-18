using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace FG
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        public static Camera PlayerCamera { get; private set; }
        public static GameObject Player { get; private set; }
        public bool lockCursor = true;

        private void Awake()
        {
            PlayerCamera = Camera.main;
            Player = GameObject.FindWithTag("Player");
            Assert.IsNotNull(Player, "There is no object with tag \"Player\" in scene");
        }

        private void OnEnable()
        {
            if (lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        
        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        public static void ReleaseMouse()
        {
            Cursor.lockState = CursorLockMode.None;
        }

        public static void LoadMainScene()
        {
            SceneManager.LoadScene("Main");
        }

        public static void LoadMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public static void Quit()
        {
            Application.Quit();
        }
    }
}
