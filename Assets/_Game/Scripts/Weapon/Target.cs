using UnityEngine;
using UnityEngine.Assertions;

namespace FG
{
    public class Target : MonoBehaviour
    {
        public float hitPoints = 20f;
        public GameObject particleWhenHit;
        public GameObject particleWhenKilled;
        public float particlePlayLength = 3f;
        [Space]
        public float smallCollisionForce = 100f;
        public float smallCollisionDamage = 5f;
        [Space]
        public float bigCollisionForce = 200f;
        public float bigCollisionDamage = 10f;

        private CanvasManager _canvasManager;
        
        private WeaponManager _weaponManager;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            try
            {
                _canvasManager = GameManager.CanvasManagerInstance;
            }
            catch
            {
                Debug.Log("CanvasManager not found, assuming this is intentional");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Shot shotScript = other.GetComponent<Shot>();
            if (other.CompareTag("Weapon"))
            {
                if (particleWhenHit != null)
                {
                    Destroy(Instantiate(particleWhenHit, other.transform.position, Quaternion.identity, _transform), particlePlayLength);
                }
                Damage(shotScript.damageToApply);
                Destroy(other.gameObject);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            float collisionForce = other.impulse.magnitude / Time.fixedDeltaTime;
        
            if (collisionForce < smallCollisionForce)
            {
                Debug.Log("Small collision with " + name);
                Damage(smallCollisionDamage);
            }
            else if (collisionForce < bigCollisionForce)
            {
                Debug.Log("Big collision with " + name);
                Damage(bigCollisionDamage);
            }
            else
            {
                Damage(hitPoints);
            }
        }

        private void Damage(float damage)
        {
            hitPoints -= damage;
            if (CompareTag("Player"))
            {
                _canvasManager.UpdateHP(hitPoints);
            }
            if (hitPoints <= 0)
            {
                if (particleWhenKilled != null)
                {
                    Destroy(Instantiate(particleWhenKilled, _transform.position, Quaternion.identity), particlePlayLength);
                }
                Debug.Log(name + " has died/been destroyed");
                if (CompareTag("Player"))
                {
                    _canvasManager.GameOver();
                }
                else if (CompareTag("Enemy"))
                {
                    GameManager.PlayerPoints++;
                }
                Destroy(gameObject);
            }
        }
    }
}
