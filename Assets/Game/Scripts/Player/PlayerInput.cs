using UnityEngine;

namespace FG
{
    public class PlayerInput : MonoBehaviour
    {
        #region InputIDs
        private const string leftRightMovement = "Horizontal";
        private const string forwardBackMovement = "Vertical";
        private const string zoomInOutCamera = "Mouse ScrollWheel";
        private const string orbitCamera = "Mouse X";
        private const string pitchCamera = "Mouse Y";
        #endregion

        public float mouseSensitivity = 1f;

        private PlayerMovement _playerMovement;
        private OrbitCamera _cameraController;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _cameraController = GameManager.PlayerCamera.GetComponent<OrbitCamera>();
        }

        private void Update()
        {
            _playerMovement.forwardBackMovement = Input.GetAxis(forwardBackMovement);
            _playerMovement.leftRightMovement = Input.GetAxis(leftRightMovement);
            
            _cameraController.cameraHorizontalInput = Input.GetAxis(orbitCamera) * mouseSensitivity;
            _cameraController.cameraVerticalInput = Input.GetAxis(pitchCamera) * mouseSensitivity;
            _cameraController.cameraZoomInput = Input.GetAxis(zoomInOutCamera);
        }
    }
}
