using UnityEngine;

namespace FG
{
    public class LaserWeapon : BaseWeapon, IWeapon
    {
        public float coolDown = 1f;
        public LayerMask layersToTarget;
        public float Shoot()
        {
            
            return coolDown;
        }
    }
}
