using System;
using UnityEngine;

namespace FG
{
    public class HomingShot : MonoBehaviour, IShot
    {
        public float DamageToApply
        {
            get => damageToApply;
            set => damageToApply = value;
        }

        public Transform target;
        public float damageToApply = 15f;
        [Tooltip("How long until despawning shot")]
        public float despawnTime = 20f;

        [NonSerialized] public float speed;

        private Rigidbody _rigidbody;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            Destroy(gameObject, despawnTime);
        }

        private void Update()
        {
            if (target == isActiveAndEnabled)
            {
                transform.LookAt(target);
            }
            _rigidbody.velocity = speed * _transform.forward;
        }
    }
}