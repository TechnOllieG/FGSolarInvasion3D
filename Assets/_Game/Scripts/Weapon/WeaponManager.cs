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
            get => _selectedWeapon;
            set
            {
                if (value > 0)
                {
                    int tempValue = _selectedWeapon + 1;
                    if (tempValue > _numberOfWeapons - 1)
                    {
                        _selectedWeapon = 0;
                    }
                    else
                    {
                        _selectedWeapon = tempValue;
                    }
                }
                else if (value < 0)
                {
                    int tempValue = _selectedWeapon - 1;
                    if (tempValue < 0)
                    {
                        _selectedWeapon = _numberOfWeapons - 1;
                    }
                    else
                    {
                        _selectedWeapon = tempValue;
                    }
                }
            }
        }

        [Tooltip("Optional text component to display currently selected weapon on screen")]
        public Text selectedWeaponDisplay;
        public string selectedWeaponPrefix = "Selected Weapon: ";
        [Tooltip("Transforms of empty objects where the ammunition will shoot from")]
        public Transform[] weaponOutputs = new Transform[2];

        [NonSerialized] public bool fireWeapon = false;

        private int _selectedWeapon = 0;
        private int _numberOfWeapons = 0;
        private float _currentCooldown = 0f;
        private float _timeOfExecution = 0f;
        private int _oldWeapon = 0;
        private int _oldWeaponText = 0;
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
            if (_oldWeaponText != _selectedWeapon && selectedWeaponDisplay != null)
            {
                UpdateSelectedWeaponDisplay();
            }
            
            if (fireWeapon)
            {
                if (_oldWeapon != _selectedWeapon)
                {
                    _currentCooldown = 0f;
                    _timeOfExecution = 0f;
                    _oldWeapon = _selectedWeapon;
                }
                if (Time.time - _timeOfExecution > _currentCooldown && _oldWeapon == _selectedWeapon)
                {
                    _timeOfExecution = Time.time;
                    _oldWeapon = _selectedWeapon;
                    _currentCooldown = _weapons[_selectedWeapon].Script.Shoot();
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

        private void UpdateSelectedWeaponDisplay()
        {
            selectedWeaponDisplay.text = selectedWeaponPrefix + _weapons[_selectedWeapon].Name;
            _oldWeaponText = _selectedWeapon;
        }
    }
}
