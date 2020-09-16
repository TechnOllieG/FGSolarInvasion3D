using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    public float hitPoints = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Shot shotScript = other.GetComponent<Shot>();
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
