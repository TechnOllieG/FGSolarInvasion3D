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
        public static CanvasManager CanvasManagerInstance { get; private set; }
        
        public bool lockCursor = true;

        public static int PlayerPoints
        {
            get => _playerPoints;
            set
            {
                _playerPoints = value;
                CanvasManagerInstance.UpdatePoints(_playerPoints);
            }
        }
        
        private static int _playerPoints = 0;

        private void Awake()
        {
            PlayerCamera = Camera.main;
            Assert.IsNotNull(PlayerCamera, "There is no object with tag \"Main Camera\" in scene");
            Player = GameObject.FindWithTag("Player");
            Assert.IsNotNull(Player, "There is no object with tag \"Player\" in scene");
            GameObject canvasManager = GameObject.FindWithTag("CanvasManager");
            CanvasManagerInstance = canvasManager == isActiveAndEnabled ? canvasManager.GetComponent<CanvasManager>() : null;
        }

        private void OnEnable()
        {
            _playerPoints = 0;
            
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
