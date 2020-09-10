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
        
        private CharacterController _characterController;
        private OrbitCamera _cameraController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _cameraController = GameManager.PlayerCamera.GetComponent<OrbitCamera>();
        }

        private void Update()
        {
            _characterController.forwardBackInput = Input.GetAxis(forwardBackMovement);
            _characterController.leftRightInput = Input.GetAxis(leftRightMovement);

            if (_cameraController == isActiveAndEnabled)
            {
                _cameraController.cameraHorizontalInput = Input.GetAxis(orbitCamera) * mouseSensitivity;
                _cameraController.cameraVerticalInput = Input.GetAxis(pitchCamera) * mouseSensitivity;
                _cameraController.cameraZoomInput = Input.GetAxis(zoomInOutCamera);
            }
        }
    }
}
