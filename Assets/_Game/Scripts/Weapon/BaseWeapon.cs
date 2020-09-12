using UnityEngine;

namespace FG
{
    public class BaseWeapon : MonoBehaviour
    {
        public bool enableScript = false;

        public string Name => GetType().Name.ToString();

        public bool Enabled
        {
            get => enableScript;
            set => enableScript = value;
        }
    }
}
