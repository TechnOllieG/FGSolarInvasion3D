using UnityEngine;

namespace FG
{
    public class Shot : MonoBehaviour, IShot
    {
        public float DamageToApply
        {
            get => damageToApply;
            set => damageToApply = value;
        }
        
        public float damageToApply = 2f;
        [Tooltip("How long until despawning shot")]
        public float despawnTime = 10f;

        private void Awake()
        {
            Destroy(transform.parent.gameObject, despawnTime);
        }
    }
}
