using System.Net.Security;
using UnityEngine;

namespace FG
{
    public class LaserWeapon : BaseWeapon, IWeapon
    {
        public float coolDown = 1f;
        public GameObject shot;
        public float bulletSpeed = 1f;

        private byte _alternatingOrder = 0;

        public float Shoot()
        {
            GameObject[] currentShot = new GameObject[2];
            if (shootLeft)
            {
                currentShot[0] = Instantiate(shot, leftWeaponTransform.position, leftWeaponTransform.rotation);
            }

            if (shootRight)
            {
                currentShot[1] = Instantiate(shot, rightWeaponTransform.position, rightWeaponTransform.rotation);
            }

            if (alternating)
            {
                switch (_alternatingOrder)
                {
                    case 0: 
                        currentShot[0] = Instantiate(shot, leftWeaponTransform.position, leftWeaponTransform.rotation);
                        _alternatingOrder = 1;
                        break;
                    case 1:
                        currentShot[0] = Instantiate(shot, rightWeaponTransform.position, rightWeaponTransform.rotation);
                        _alternatingOrder = 0;
                        break;
                    default:
                        _alternatingOrder = 0;
                        goto case 0;
                }
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
