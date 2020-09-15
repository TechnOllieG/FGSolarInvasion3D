using UnityEngine;

namespace FG
{
    public class BaseWeapon : MonoBehaviour
    {
        public enum WeaponMode
        {
            Twin,
            Alternating,
            LeftOnly,
            RightOnly
        }
        
        public bool enableScript = false;

        public string Name => GetType().Name.ToString();

        public bool Enabled
        {
            get => enableScript;
            set => enableScript = value;
        }

        public WeaponMode weaponMode = WeaponMode.Twin;
        
        protected bool shootLeft = false;
        protected bool shootRight = false;
        protected bool alternating = false;
        
        protected Transform leftWeaponTransform;
        protected Transform rightWeaponTransform;

        private void Awake()
        {
            leftWeaponTransform = GameManager.WeaponManager.leftWeapon.transform;
            rightWeaponTransform = GameManager.WeaponManager.rightWeapon.transform;
        }

        private void OnValidate()
        {
            if (weaponMode == WeaponMode.Twin)
            {
                shootLeft = true;
                shootRight = true;
                alternating = false;
            }

            if (weaponMode == WeaponMode.Alternating)
            {
                shootLeft = false;
                shootRight = false;
                alternating = true;
            }

            if (weaponMode == WeaponMode.LeftOnly)
            {
                shootLeft = true;
                shootRight = false;
                alternating = false;
            }

            if (weaponMode == WeaponMode.RightOnly)
            {
                shootLeft = false;
                shootRight = true;
                alternating = false;
            }
        }
    }
}
