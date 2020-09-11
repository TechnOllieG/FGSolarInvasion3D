using System;
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

        [NonSerialized] public List<Weapon> weapons = new List<Weapon>();

        private void Awake()
        {
            IWeapon[] tempWeapons = GetComponents<IWeapon>();

            foreach (IWeapon tempScript in tempWeapons)
            {
                if (tempScript.CheckEnable())
                {
                    string tempName = tempScript.Name();
                    WeaponShootingOrder tempOrder = tempScript.ShootingOrder();
                    weapons.Add(new Weapon() {Script = tempScript, Name = tempName, ShootingOrder = tempOrder});
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("WeaponPickup"))
            {
                WeaponPickup currentPickup = other.gameObject.GetComponent<WeaponPickup>();
                string scriptName = currentPickup.script;
                
                IWeapon tempScript = (IWeapon)GetComponent(scriptName);
                
                string tempName = tempScript.Name();
                WeaponShootingOrder tempOrder = tempScript.ShootingOrder(); 
                
                weapons.Add(new Weapon() {Script = tempScript, Name = tempName, ShootingOrder = tempOrder});
                
                tempScript.Enable();
                Destroy(other.gameObject);
            }
        }
    }
}
