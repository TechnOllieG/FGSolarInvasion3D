using UnityEngine;

namespace FG
{
    public enum OrbitAxis
    {
        X = 0,
        Y = 1,
        Z = 2
    }
    public class SpaceOrbit : MonoBehaviour
    {
        public float orbitSpeed = 20f;
        public OrbitAxis orbitAxis = OrbitAxis.X;
        
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            float tempSpeed = orbitSpeed * Time.deltaTime;
            switch (orbitAxis)
            {
                case OrbitAxis.X:
                    _transform.Rotate(tempSpeed, 0f, 0f);
                    break;
                case OrbitAxis.Y:
                    _transform.Rotate(0f, tempSpeed, 0f);
                    break;
                case OrbitAxis.Z:
                    _transform.Rotate(0f, 0f, tempSpeed);
                    break;
                default:
                    Debug.Log("SpaceOrbit script: OrbitAxis variable not containing valid data");
                    break;
            }
        }
    }
}

