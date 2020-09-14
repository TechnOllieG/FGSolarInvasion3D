using UnityEngine;

namespace FG
{
    public class Missile : BaseWeapon, IWeapon
    {
        public float coolDown = 5f;
        public float Shoot()
        {
            
            return coolDown;
        }
    }
}
