using UnityEngine;

namespace FG
{
    public class LaserWeapon : BaseWeapon, IWeapon
    {
        public float coolDown = 0.2f;
        public GameObject shot;
        public float bulletSpeed = 500f;

        private int _alternatingOrder = 0;

        public float Shoot()
        {
            GameObject[] currentShot;
            if (shootAll)
            {
                currentShot = new GameObject[localWeaponOutputs.Length];
                for (int i = 0; i < localWeaponOutputs.Length; i++)
                {
                    currentShot[i] = Instantiate(shot, localWeaponOutputs[i].position, localWeaponOutputs[i].rotation);
                }
            }
            else if (shootAlternating)
            {
                currentShot = new GameObject[1];
                currentShot[0] = Instantiate(shot, localWeaponOutputs[_alternatingOrder].position, localWeaponOutputs[_alternatingOrder].rotation);
                
                _alternatingOrder++;
                if (_alternatingOrder > localWeaponOutputs.Length - 1)
                {
                    _alternatingOrder = 0;
                }
            }
            else
            {
                Debug.Log("Either shootAll or shootAlternating should be true");
                return coolDown;
            }

            foreach(GameObject obj in currentShot)
            {
                Rigidbody tempBody = obj.GetComponent<Rigidbody>();
                tempBody.velocity = bulletSpeed * obj.transform.forward + GetComponent<Rigidbody>().velocity;
            }

            return coolDown;
        }
    }
}
