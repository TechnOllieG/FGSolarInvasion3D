using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FG
{
    public class Weapon
    {
        public IWeapon Script { get; set; }
        public string Name { get; set; }
    }
    
    public class WeaponManager : MonoBehaviour
    {
        public int SelectedWeapon
        {
            get => selectedWeapon;
            set => selectedWeapon = Mathf.Clamp(value, 0, _numberOfWeapons - 1);
        }

        public Text selectedWeaponDisplay;
        public GameObject leftWeapon;
        public GameObject rightWeapon;
        public int selectedWeapon = 0;

        [NonSerialized] public bool fireWeapon = false;

        private int _numberOfWeapons = 0;
        private float currentCooldown = 0f;
        private float timeOfExecution = 0f;
        private int oldWeapon = 0;
        private List<Weapon> _weapons = new List<Weapon>();

        private void Awake()
        {
            IWeapon[] tempWeapons = GetComponents<IWeapon>();

            foreach (IWeapon tempScript in tempWeapons)
            {
                if (tempScript.Enabled)
                {
                    string tempName = tempScript.Name;
                    _weapons.Add(new Weapon() {Script = tempScript, Name = tempName});
                }
            }
            
            _numberOfWeapons = _weapons.Count;
        }

        private void Update()
        {
            
            
            if (fireWeapon)
            {
                if (oldWeapon != selectedWeapon && timeOfExecution > 0)
                {
                    currentCooldown = 0f;
                    timeOfExecution = 0f;
                    oldWeapon = selectedWeapon;
                }
                if (Time.unscaledTime - timeOfExecution > currentCooldown && oldWeapon == selectedWeapon)
                {
                    timeOfExecution = Time.unscaledTime;
                    oldWeapon = selectedWeapon;
                    currentCooldown = _weapons[selectedWeapon].Script.Shoot();
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

                _weapons.Add(new Weapon() {Script = tempScript, Name = tempName});
                _numberOfWeapons = _weapons.Count;
                
                tempScript.Enabled = true;
                Destroy(other.gameObject);
            }
        }
    }
}
