using UnityEngine;

namespace FG
{
    public class Shot : MonoBehaviour
    {
        public float damageToApply = 2f;
        [Tooltip("How long until despawning shot")]
        public float despawnTime = 10f;

        private void Awake()
        {
            Destroy(transform.parent.gameObject, despawnTime);
        }
    }
}
