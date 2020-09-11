using UnityEngine;

namespace FG
{
    [DefaultExecutionOrder(100)]
    public class WeaponPickup : MonoBehaviour
    {
        public string script;
        [HideInInspector] public int choiceIndex = 0;
    }
}
