using UnityEngine;

namespace FG
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        public static Camera PlayerCamera { get; private set; }
        public static GameObject Player { get; private set; }
        public static WeaponManager WeaponManager { get; private set; }

        private void Awake()
        {
            PlayerCamera = Camera.main;
            Player = GameObject.FindWithTag("Player");
            WeaponManager = Player.GetComponent<WeaponManager>();
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
