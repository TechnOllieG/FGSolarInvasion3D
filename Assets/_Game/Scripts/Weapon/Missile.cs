using UnityEngine;

namespace FG
{
    public class Missile : BaseWeapon, IWeapon
    {
        public float coolDown = 10f;
        public float Shoot()
        {
            
            return coolDown;
        }
    }
}
