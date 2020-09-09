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

        private MovementController _movementController;
        private OrbitCamera _cameraController;

        private void Awake()
        {
            _movementController = GetComponent<MovementController>();
            _cameraController = GameManager.PlayerCamera.GetComponent<OrbitCamera>();
        }

        private void Update()
        {
            _movementController.forwardBackInput = Input.GetAxis(forwardBackMovement);
            _movementController.leftRightInput = Input.GetAxis(leftRightMovement);
            
            _cameraController.cameraHorizontalInput = Input.GetAxis(orbitCamera) * mouseSensitivity;
            _cameraController.cameraVerticalInput = Input.GetAxis(pitchCamera) * mouseSensitivity;
            _cameraController.cameraZoomInput = Input.GetAxis(zoomInOutCamera);
        }
    }
}
