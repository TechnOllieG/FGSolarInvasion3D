using UnityEngine;

namespace FG
{
    public class HomingMissile : BaseWeapon, IWeapon
    {
        public float coolDown = 10f;
        public GameObject shot;
        public float bulletSpeed = 500f;
        public float damageToApply = 20f;
        public float despawnTime = 20f;
        
        [Tooltip("Radius of the Sphere Cast being shot out of the main camera, signifies how close to the center of the camera the enemy has to be to lock on.")]
        public float radiusOfSphereCast = 2f;
        [Tooltip("Max range of the Sphere Cast (more details > Radius Of Sphere Cast tooltip)")]
        public float maxSphereCastRange = 50f;
        [Tooltip("Which layers the Sphere Cast will target (more details > Radius Of Sphere Cast tooltip)")]
        public LayerMask sphereCastLayerMask;
        [Tooltip("How long the target has to be in view before locking on (in secs)")]
        public float delayUntilLockedOn = 3f;
        [Space, Tooltip("Show debug lines as gizmos in inspector to see how the sphere cast is being cast")]
        public bool debugSphereCast = false;
        
        private bool _homingEnabled = false;
        private Transform _currentTarget = null;
        private int _alternatingOrder = 0;
        private float _timeOfBeginTarget = 0f;

        private void OnDrawGizmos()
        {
            if (debugSphereCast)
            {
                Transform camTransform = Camera.main.transform;
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(camTransform.position, radiusOfSphereCast);
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(camTransform.position, camTransform.position + maxSphereCastRange * camTransform.forward);

                Gizmos.color = Color.gray;
                Gizmos.DrawLine(camTransform.position + camTransform.up * radiusOfSphereCast, camTransform.position + camTransform.up * radiusOfSphereCast + camTransform.forward * maxSphereCastRange);
                Gizmos.DrawLine(camTransform.position + -camTransform.up * radiusOfSphereCast, camTransform.position + -camTransform.up * radiusOfSphereCast + camTransform.forward * maxSphereCastRange);
                Gizmos.DrawLine(camTransform.position + camTransform.right * radiusOfSphereCast, camTransform.position + camTransform.right * radiusOfSphereCast + camTransform.forward * maxSphereCastRange);
                Gizmos.DrawLine(camTransform.position + -camTransform.right * radiusOfSphereCast, camTransform.position + -camTransform.right * radiusOfSphereCast + camTransform.forward * maxSphereCastRange);

                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(camTransform.position + maxSphereCastRange * camTransform.forward, radiusOfSphereCast);
            }
        }

        private void Update()
        {
            if (enableScript)
            {
                if (weaponManager.weapons[weaponManager.selectedWeapon].Name == GetType().Name)
                {
                    RaycastHit hitInfo;
                    if (Physics.SphereCast(cameraTransform.position, radiusOfSphereCast, cameraTransform.forward,
                        out hitInfo, maxSphereCastRange, sphereCastLayerMask, QueryTriggerInteraction.Ignore))
                    {
                        if (_currentTarget != hitInfo.transform)
                        {
                            _homingEnabled = false;
                            _currentTarget = hitInfo.transform;
                            _timeOfBeginTarget = Time.time;
                            if (canvasManager == isActiveAndEnabled)
                                canvasManager.UpdateHomingCrosshair(HomingCrosshairState.Targeting);
                        }
                        else if (Time.time - _timeOfBeginTarget > delayUntilLockedOn)
                        {
                            _homingEnabled = true;
                            Debug.Log("Homing is enabled");
                            if (canvasManager == isActiveAndEnabled)
                                canvasManager.UpdateHomingCrosshair(HomingCrosshairState.Locked, _currentTarget);
                        }
                    }
                    else
                    {
                        _homingEnabled = false;
                        _currentTarget = null;
                        _timeOfBeginTarget = 0f;
                        if (canvasManager == isActiveAndEnabled)
                            canvasManager.UpdateHomingCrosshair(HomingCrosshairState.Resting);
                    }
                }
                else
                {
                    _homingEnabled = false;
                    if (canvasManager == isActiveAndEnabled)
                        canvasManager.UpdateHomingCrosshair(HomingCrosshairState.Disabled);
                    
                }
            }
        }

        public float Shoot()
        {
            GameObject[] currentShot;
            
            if (shootAll)
            {
                currentShot = new GameObject[localWeaponOutputs.Length];
                for (int i = 0; i < localWeaponOutputs.Length; i++)
                {
                    currentShot[i] = Instantiate(shot, localWeaponOutputs[i].position, transform.rotation);
                }
            }
            else if (shootAlternating)
            {
                currentShot = new GameObject[1];
                currentShot[0] = Instantiate(shot, localWeaponOutputs[_alternatingOrder].position, transform.rotation);
                
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
                HomingShot shotScript = obj.GetComponentInChildren<HomingShot>();
                
                shotScript.speed = bulletSpeed;
                shotScript.damageToApply = damageToApply;
                shotScript.despawnTime = despawnTime;

                if (_homingEnabled)
                {
                    shotScript.target = _currentTarget;
                }

                shotScript.DelayedDestroy();
            }
            return coolDown;
        }
    }
}
