using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public enum WeaponShootingOrder
    {
        BothAtSameTime,
        Alternating,
        RightWeaponOnly,
        LeftWeaponOnly,
    }

    public class Weapon
    {
        public IWeapon Script { get; set; }
        public string Name { get; set; }
        public WeaponShootingOrder ShootingOrder { get; set; }
    }
    
    [DefaultExecutionOrder(0)]
    public class WeaponManager : MonoBehaviour
    {
        public GameObject leftWeapon;
        public GameObject rightWeapon;
        public int selectedWeapon = 0;

        private List<Weapon> _weapons = new List<Weapon>();

        private void Awake()
        {
            IWeapon[] tempWeapons = GetComponents<IWeapon>();

            foreach (IWeapon tempScript in tempWeapons)
            {
                string tempName = tempScript.Name();
                WeaponShootingOrder tempOrder = tempScript.ShootingOrder();
                _weapons.Add(new Weapon() {Script = tempScript, Name = tempName, ShootingOrder = tempOrder});
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("WeaponPickup"))
            {
                WeaponPickup currentPickup = other.gameObject.GetComponent<WeaponPickup>();
                //currentPickup.scriptToApply.Name;
                //IWeapon temp = AddComponent()
            }
        }
    }
}
