using UnityEngine;
using UnityEditor;

namespace FG
{
    [CustomEditor(typeof(WeaponPickup))]
    public class WeaponPickupEditor : Editor
    {
        public string[] scripts = new [] { "placeholder" };
        private int _choiceIndex = 0;
 
        public override void OnInspectorGUI ()
        {
            // Draw the default inspector
            DrawDefaultInspector();
            _choiceIndex = EditorGUILayout.Popup(_choiceIndex, scripts);
            var weaponPickup = target as WeaponPickup;
            // Update the selected choice in the underlying object
            weaponPickup.script = scripts[_choiceIndex];
            // Save the changes back to the object
            EditorUtility.SetDirty(target);
        }
        
        void OnGUI ()
        {
            // Choose an option from the list
            _choiceIndex = EditorGUILayout.Popup(_choiceIndex, scripts);
            // Update the selected option on the underlying instance of SomeClass
            var weaponPickup = target as WeaponPickup;
            weaponPickup.script = scripts[_choiceIndex];
        }
    }
    
    [DefaultExecutionOrder(100)]
    public class WeaponPickup : MonoBehaviour
    {
        [Tooltip("IWeapon script to enable to player when picking up")]
        public string script;

        private void OnValidate()
        {
            WeaponPickupEditor.scripts
        }
    }
}
