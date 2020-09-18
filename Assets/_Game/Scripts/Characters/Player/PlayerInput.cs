using System;
using UnityEngine;

namespace FG
{
    public class PlayerInput : MonoBehaviour
    {
        public enum MouseButton
        {
            LeftClick = 0,
            RightClick = 1
        }

        #region InputIDs
        private const string leftRightMovement = "Horizontal";
        private const string forwardBackMovement = "Vertical";
        private const string rollMovement = "Roll";
        private const string scrollWheel = "Mouse ScrollWheel";
        private const string orbitCamera = "Mouse X";
        private const string pitchCamera = "Mouse Y";
        #endregion

        public float mouseSensitivity = 1f;
        public MouseButton shootButton = MouseButton.LeftClick;

        private CharacterController _characterController;
        private WeaponManager _weaponManager;

        private void Awake()
        {
            _characterController = GameManager.Player.GetComponent<CharacterController>();
            _weaponManager = GameManager.Player.GetComponent<WeaponManager>();
        }

        private void Update()
        {
            if (_characterController == isActiveAndEnabled)
            {
                _characterController.forwardBackInput = Input.GetAxis(forwardBackMovement);
                _characterController.leftRightInput = Input.GetAxis(leftRightMovement);
                _characterController.rollInput = Input.GetAxis(rollMovement);
            }

            if (_weaponManager == isActiveAndEnabled)
            {
                _weaponManager.SelectedWeapon = Convert.ToInt16(Input.GetAxis(scrollWheel) * 10);
                _weaponManager.fireWeapon = Input.GetMouseButton((int)shootButton);
            }
        }
    }
}
