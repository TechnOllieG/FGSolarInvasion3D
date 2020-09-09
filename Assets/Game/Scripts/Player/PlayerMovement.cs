using System;
using UnityEngine;

namespace FG
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        public float movementSpeedMultiplier = 1f;
        [Tooltip("The transform of the object player forces shall be applied relative to, for example: the player camera")]
        public Transform playerInputSpace = default;
        
        [NonSerialized] public float leftRightMovement;
        [NonSerialized] public float forwardBackMovement;
        
        private Transform _playerTransform;
        private Rigidbody _rigidbody;
        private Vector3 _movement; // The movement velocity to apply to the rigidbody

        private void Awake()
        {
            _playerTransform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (playerInputSpace)
            {
                Vector3 forward = playerInputSpace.forward.normalized;
                forward.y = 0f;
                Vector3 right = playerInputSpace.right.normalized;
                right.y = 0f;
                _movement = (forward * forwardBackMovement + right * leftRightMovement) * movementSpeedMultiplier;
            }
            else
            {
                _movement = new Vector3(leftRightMovement, 0f, forwardBackMovement) * movementSpeedMultiplier;
            }
            _rigidbody.velocity = _movement;
        }
    }
}