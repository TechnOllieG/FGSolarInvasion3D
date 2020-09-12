using System;
using System.Collections.Generic;
using UnityEngine;

namespace FG
{
    public class Weapon
    {
        public IWeapon Script { get; set; }
        public string Name { get; set; }
    }
    
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
                if (tempScript.Enabled)
                {
                    string tempName = tempScript.Name;
                    weapons.Add(new Weapon() {Script = tempScript, Name = tempName});
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
                string tempName = tempScript.Name;

                weapons.Add(new Weapon() {Script = tempScript, Name = tempName});
                
                tempScript.Enabled = true;
                Destroy(other.gameObject);
            }
        }
    }
}
