using UnityEngine;

public class FlightCamera : MonoBehaviour
{
    public Transform focus;
    public float cameraDistance;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        Vector3 lookDirection = _transform.rotation * Vector3.forward;
        Vector3 cameraPosition = focus.position - lookDirection * cameraDistance;
        _transform.SetPositionAndRotation(cameraPosition, focus.rotation);
    }
}
