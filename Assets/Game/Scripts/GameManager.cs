using UnityEngine;

namespace FG
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        public static Camera PlayerCamera { get; private set; }

        private void Awake()
        {
            PlayerCamera = Camera.main;
        }
    }
}
