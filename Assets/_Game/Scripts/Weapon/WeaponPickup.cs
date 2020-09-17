using UnityEngine;

namespace FG
{
    public class WeaponPickup : MonoBehaviour
    {
        [HideInInspector] public string script;
        [HideInInspector] public int choiceIndex = 0;

        public bool animateSpin = false;
        public float animationSpinSpeed = 30f;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (animateSpin)
            {
                _transform.Rotate(0f, animationSpinSpeed * Time.deltaTime, 0f, Space.World);
            }
        }
    }
}
