using UnityEngine;

namespace FG
{
    public class Missile : MonoBehaviour, IWeapon
    {
        public WeaponShootingOrder weaponShootingOrder = WeaponShootingOrder.Alternating;

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
