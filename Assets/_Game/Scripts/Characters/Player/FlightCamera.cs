using UnityEngine;

public class FlightCamera : MonoBehaviour
{
    public Transform focus;
    public float cameraDistance;
    public Vector3 offset = Vector3.zero;
    public Vector3 rotationOffset = Vector3.zero;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void LateUpdate()
    {
        if (focus == isActiveAndEnabled)
        {
            Vector3 lookDirection = _transform.rotation * Vector3.forward;
            Vector3 cameraPosition = focus.position - lookDirection * cameraDistance;
            Quaternion cameraRotation = Quaternion.Euler(focus.rotation.eulerAngles + rotationOffset);
            _transform.SetPositionAndRotation(cameraPosition + offset, cameraRotation);
        }
    }
}
