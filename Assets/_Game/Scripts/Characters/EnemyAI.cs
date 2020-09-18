using UnityEngine;
using UnityEngine.Assertions;

namespace FG
{
    public class EnemyAI : MonoBehaviour
    {
        public float detectionRange = 20f;
        public float maxSpeed = 10f;
        public float accelerationMultiplier = 10f;
        
        private Transform _playerTransform;
        private Transform _transform;
        private Rigidbody _rigidbody;
        private WeaponManager _weaponManager;
        private bool _accelerate = false;

        private void Awake()
        {
            _transform = transform;
            
            _playerTransform = GameManager.Player.transform;
            Assert.IsNotNull(_playerTransform, "EnemyAI Script cannot access player's transform through GameManager");
            
            _rigidbody = GetComponent<Rigidbody>();
            Assert.IsNotNull(_rigidbody, "No rigidbody is attached to " + name);

            _weaponManager = GetComponent<WeaponManager>();
        }

        private void FixedUpdate()
        {
            if (_playerTransform == isActiveAndEnabled)
            {
                if (_rigidbody.velocity.sqrMagnitude > maxSpeed * maxSpeed) // if enemies velocity is over maxSpeed
                {
                    _accelerate = false;
                }
                else
                {
                    _accelerate = true;
                }

                float distance = Vector3.Distance(_playerTransform.position, transform.position);

                if (distance <= detectionRange) // if player is in range
                {
                    _transform.LookAt(_playerTransform);

                    if (_accelerate)
                    {
                        _rigidbody.AddForce((accelerationMultiplier * Time.fixedDeltaTime) * _transform.forward);
                    }

                    _weaponManager.fireWeapon = true;
                }
                else
                {
                    _weaponManager.fireWeapon = false;
                }
            }
            else
            {
                _weaponManager.fireWeapon = false;
            }
        }
    }
}