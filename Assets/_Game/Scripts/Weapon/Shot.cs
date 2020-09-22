using System;
using UnityEngine;

namespace FG
{
    public class Shot : MonoBehaviour, IShot
    {
        public float DamageToApply
        {
            get => damageToApply;
        }
        
        [NonSerialized] public float damageToApply = 2f;
        [NonSerialized] public float despawnTime = 10f;

        private void Awake()
        {
            Destroy(transform.parent.gameObject, despawnTime);
        }
    }
}
