using UnityEngine;

namespace FG
{
    public class LaserWeapon : MonoBehaviour, IWeapon
    {
        public WeaponShootingOrder weaponShootingOrder = WeaponShootingOrder.BothAtSameTime;

        public void Shoot()
        {
            
        }
        
        public string Name()
        {
            return this.name;
        }

        public WeaponShootingOrder ShootingOrder()
        {
            return weaponShootingOrder;
        }
    }
}
