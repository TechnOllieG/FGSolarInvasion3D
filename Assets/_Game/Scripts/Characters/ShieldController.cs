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
        public GameObject particleWhenHit;
        public float particlePlayLength = 1.5f;
        
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
                shieldObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Weapon"))
            {
                Shot shotScript = other.GetComponent<Shot>();
                if (particleWhenHit != null)
                {
                    Destroy(Instantiate(particleWhenHit, other.transform.position, Quaternion.identity, _transform), particlePlayLength);
                }
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