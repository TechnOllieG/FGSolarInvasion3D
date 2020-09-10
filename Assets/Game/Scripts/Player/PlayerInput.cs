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

<<<<<<< HEAD
        private PlayerMovement _playerMovement;
=======
        private MovementController _movementController;
>>>>>>> 20a66d1a13151c70b8c5d2739206a4bc29049d41
        private OrbitCamera _cameraController;

        private void Awake()
        {
<<<<<<< HEAD
            _playerMovement = GetComponent<PlayerMovement>();
=======
            _movementController = GetComponent<MovementController>();
>>>>>>> 20a66d1a13151c70b8c5d2739206a4bc29049d41
            _cameraController = GameManager.PlayerCamera.GetComponent<OrbitCamera>();
        }

        private void Update()
        {
<<<<<<< HEAD
            _playerMovement.forwardBackMovement = Input.GetAxis(forwardBackMovement);
            _playerMovement.leftRightMovement = Input.GetAxis(leftRightMovement);
=======
            _movementController.forwardBackInput = Input.GetAxis(forwardBackMovement);
            _movementController.leftRightInput = Input.GetAxis(leftRightMovement);
>>>>>>> 20a66d1a13151c70b8c5d2739206a4bc29049d41
            
            _cameraController.cameraHorizontalInput = Input.GetAxis(orbitCamera) * mouseSensitivity;
            _cameraController.cameraVerticalInput = Input.GetAxis(pitchCamera) * mouseSensitivity;
            _cameraController.cameraZoomInput = Input.GetAxis(zoomInOutCamera);
        }
    }
}
