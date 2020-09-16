using UnityEngine;

namespace FG
{
    public class Target : MonoBehaviour
    {
        public float hitPoints = 20f;
        
        private WeaponManager _weaponManager;

        private void OnTriggerEnter(Collider other)
        {
            Shot shotScript = other.GetComponent<Shot>();
            if (other.CompareTag("Weapon"))
            {
                Damage(shotScript.damageToApply);
                Destroy(other.gameObject);
            }
        }

        public void Damage(float damage)
        {
            hitPoints -= damage;
            if (hitPoints <= 0)
            {
                Debug.Log(name + " has died/been destroyed");
                Destroy(gameObject);
            }
        }
    }
}
