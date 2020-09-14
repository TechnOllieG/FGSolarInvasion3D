using System;
using UnityEngine;

namespace FG
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterController : MonoBehaviour
    {
        [Tooltip("The base constant speed the player will travel with")]
        public float baseSpeed = 50f;
        [Tooltip("How much input from controls before registering an input")]
        public float minInputMagnitude = 0.01f;
        
        [Tooltip("Speed for ship's pitch rotation")]
        public float pitchSpeed = 2f;
        [Tooltip("Speed for ship's roll rotation")]
        public float yawSpeed = 2f;

        [NonSerialized] public float leftRightInput;
        [NonSerialized] public float forwardBackInput;

        private Transform _transform;
        private Quaternion _transformRotation;
        private Rigidbody _rigidbody;
        private Vector3 _playerEulerRotation = Vector3.zero;

        private void Awake()
        {
            _transform = transform;
            _transformRotation = _transform.rotation;
            _rigidbody = GetComponent<Rigidbody>();
        }
        

        private void FixedUpdate()
        {
            if (CalculateTorque())
            {
                _rigidbody.AddRelativeTorque(_playerEulerRotation, ForceMode.Impulse);
            }
            
            _rigidbody.velocity = transform.forward * baseSpeed;
        }

        private bool CalculateTorque()
        {
            bool response = false;
            
            if (forwardBackInput > minInputMagnitude || forwardBackInput < -minInputMagnitude)
            {
                _playerEulerRotation.x = forwardBackInput * pitchSpeed;
                response = true;
            }
            
            if (leftRightInput > minInputMagnitude || leftRightInput < -minInputMagnitude)
            {
                _playerEulerRotation.y = leftRightInput * yawSpeed;
                response = true;
            }
            return response;
        }
    }
}