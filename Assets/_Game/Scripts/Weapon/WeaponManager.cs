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
            set
            {
                // if input value (probably scrollwheel unless changed) is positive
                if (value > 0)
                {
                    int tempValue = selectedWeapon + 1;
                    if (tempValue > weapons.Count - 1) // If the new value to be set is higher than the maximum index of the list
                    {
                        selectedWeapon = 0;
                    }
                    else
                    {
                        selectedWeapon = tempValue;
                    }
                }
                else if (value < 0)
                {
                    int tempValue = selectedWeapon - 1;
                    if (tempValue < 0)
                    {
                        selectedWeapon = weapons.Count - 1;
                    }
                    else
                    {
                        selectedWeapon = tempValue;
                    }
                }
            }
        }

        [Tooltip("Optional text component to display currently selected weapon on screen")]
        public Text selectedWeaponDisplay;
        public string selectedWeaponPrefix = "Selected Weapon: ";
        [Tooltip("Transforms of empty objects where the ammunition will shoot from")]
        public Transform[] weaponOutputs = new Transform[2];
        public int selectedWeapon = 0;

        [NonSerialized] public bool fireWeapon = false;
        [NonSerialized] public List<Weapon> weapons = new List<Weapon>();
        
        private float _currentCooldown = 0f;
        private float _timeOfExecution = 0f;
        private int _oldWeapon = 0;
        private int _oldWeaponText = 0;

        private void Awake()
        {
            // Creates temporary array to hold all IWeapon scripts attached to this.gameObject
            IWeapon[] tempWeapons = GetComponents<IWeapon>();

            // Adds all enabled IWeapon scripts to the list
            foreach (IWeapon tempScript in tempWeapons)
            {
                if (tempScript.Enabled)
                {
                    string tempName = tempScript.Name;
                    weapons.Add(new Weapon() {Script = tempScript, Name = tempName});
                }
            }
        }

        private void Update()
        {
            // Updates UI Text object that states which weapon is currently selected (if selectedWeaponDisplay is assigned)
            if (_oldWeaponText != selectedWeapon && selectedWeaponDisplay == isActiveAndEnabled)
            {
                UpdateSelectedWeaponDisplay();
            }
            
            if (fireWeapon)
            {
                // Resets cooldown if weapon is changed
                if (_oldWeapon != selectedWeapon)
                {
                    _currentCooldown = 0f;
                    _timeOfExecution = 0f;
                    _oldWeapon = selectedWeapon;
                }
                // Fires the currently selected weapon if cooldown is over
                if (Time.time - _timeOfExecution > _currentCooldown && _oldWeapon == selectedWeapon)
                {
                    _timeOfExecution = Time.time;
                    _oldWeapon = selectedWeapon;
                    _currentCooldown = weapons[selectedWeapon].Script.Shoot();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("WeaponPickup") && CompareTag("Player"))
            {
                // Gets the string that holds the name of the script to enable from the pickup
                WeaponPickup currentPickup = other.gameObject.GetComponent<WeaponPickup>();
                string scriptName = currentPickup.script;
                
                // Gets the component attached to the player with the script name that is equal to the string in the pickup
                IWeapon tempScript = (IWeapon)GetComponent(scriptName);
                string tempName = tempScript.Name;

                // Adds the relevant script to the list, enables it and destroys the pickup
                weapons.Add(new Weapon() {Script = tempScript, Name = tempName});
                tempScript.Enabled = true;
                Destroy(other.gameObject);
            }
        }

        private void UpdateSelectedWeaponDisplay()
        {
            selectedWeaponDisplay.text = selectedWeaponPrefix + weapons[selectedWeapon].Name;
            _oldWeaponText = selectedWeapon;
        }
    }
}
