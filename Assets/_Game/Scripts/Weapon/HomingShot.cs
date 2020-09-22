using System;
using UnityEngine;

namespace FG
{
    public class HomingShot : MonoBehaviour, IShot
    {
        public float DamageToApply
        {
            get => damageToApply;
        }

        [NonSerialized] public Transform target;
        [NonSerialized] public float damageToApply = 20f;
        [NonSerialized] public float despawnTime = 20f;

        [NonSerialized] public float speed = 0f;

        private Rigidbody _rigidbody;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponentInParent<Rigidbody>();
        }

        private void Update()
        {
            if (target == isActiveAndEnabled)
            {
                transform.LookAt(target);
            }
            _rigidbody.velocity = speed * _transform.forward;
        }

        public void DelayedDestroy()
        {
            Destroy(transform.parent.gameObject, despawnTime);
        }
    }
}