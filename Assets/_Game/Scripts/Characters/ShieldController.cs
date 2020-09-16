using UnityEngine;
using System.Collections;

namespace FG
{
    [RequireComponent(typeof(Rigidbody))]
    public class ShieldController : MonoBehaviour
    {
        public Transform focus;
        public bool shieldEnabled = true;
        public GameObject shieldObject;
        public float hitPoints = 10f;
        public float shieldCooldown = 10f;
        
        private float _initialHitPoints;
        private Transform _transform;

        private void OnValidate()
        {
            if (shieldEnabled && shieldObject != null)
            {
                shieldObject.SetActive(true);
            }
            else
            {
                shieldObject.SetActive(false);
            }
        }

        private void Awake()
        {
            _initialHitPoints = hitPoints;
            _transform = transform;
        }

        private void Update()
        {
            if (focus == isActiveAndEnabled)
            {
                _transform.position = focus.position;
            }
            else
            {
                shieldEnabled = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Shot shotScript = other.GetComponent<Shot>();
            if (other.CompareTag("Weapon"))
            {
                StartCoroutine(Damage(shotScript.damageToApply));
                Destroy(other.gameObject);
            }
        }

        public IEnumerator Damage(float damage)
        {
            hitPoints -= damage;

            if (hitPoints <= 0)
            {
                shieldObject.SetActive(false);
                hitPoints = _initialHitPoints;
                yield return new WaitForSeconds(shieldCooldown);
                if (shieldEnabled)
                {
                    shieldObject.SetActive(true);
                }
            }
        }
    }
}