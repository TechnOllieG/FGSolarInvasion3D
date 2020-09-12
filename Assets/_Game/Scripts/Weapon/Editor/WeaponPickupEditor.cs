using UnityEngine;
using UnityEditor;

namespace FG
{
    [CustomEditor(typeof(WeaponPickup))]
    public class WeaponPickupEditor : Editor
    {
        private static string[] _scripts = { "placeholder" };
        private int _choiceIndex = 0;
 
        public override void OnInspectorGUI ()
        {
            var weaponPickup = target as WeaponPickup;
            
            // Get all IWeapon scripts from player and add their names to the dropdown menu
            IWeapon[] playerWeapons = GameObject.FindWithTag("Player").GetComponents<IWeapon>();
            _scripts = new string[playerWeapons.Length];

            for (int i = 0; i < playerWeapons.Length; i++)
            {
                _scripts[i] = playerWeapons[i].Name;
            }

            _choiceIndex = weaponPickup.choiceIndex;
            
            // Draw the default inspector
            DrawDefaultInspector();
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("What weapon script (attached to player) should be activated,\n" +
                                    "when picking up this object?", MessageType.Info);
            // Creates dropdown menu
            _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _scripts);
            // Update the selected choice in the underlying object
            weaponPickup.script = _scripts[_choiceIndex];
            weaponPickup.choiceIndex = _choiceIndex;
            // Save the changes back to the object
            EditorUtility.SetDirty(target);
        }
    }
}
