using UnityEngine;

namespace FG
{
    public class BaseWeapon : MonoBehaviour
    {
        public bool enableScript = false;
        public string scriptName = "Missile";
        public WeaponShootingOrder weaponShootingOrder = WeaponShootingOrder.Alternating;
        
        public void Enable()
        {
            enableScript = true;
        }

        public void Disable()
        {
            enableScript = false;
        }

        public bool CheckEnable()
        {
            return enableScript;
        }

        public string Name()
        {
            return scriptName;
        }
        
        public WeaponShootingOrder ShootingOrder()
        {
            return weaponShootingOrder;
        }
    }
}
