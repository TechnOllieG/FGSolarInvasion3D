using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour
{
    // Third person camera controller script, should be placed on a camera Game Object, requires input floats (see region: InputFloats) to be written to by a separate input script.

    #region Inspector
    // Inspector values:
    [Header("General settings")]
    
    [Tooltip("What the camera should follow")]
    public Transform focus;
    [Tooltip("How long away from the focus the camera should be when starting out"), Range(1f, 40f)]
    public float cameraDistance = 40f;
    [Tooltip("The speed of manual/automatic camera orbits"), Range(1f, 360f)]
    public float rotationSpeed = 90f;
    [Tooltip("How much the center of the focus should be offset (if any)")]
    public Vector3 centerOffset = Vector3.zero;
    [Tooltip("Change to true if you use the camera to follow a flying vehicle. Will allow camera to follow the rotation of the vehicle")]
    public bool enableFlightRotations = false;

    [Header("Zoom settings")]
    
    [Tooltip("If zooming in and out should be allowed")]
    public bool zoom = true;
    [Tooltip("How fast should the zoom be?")]
    public float zoomMultiplier = 1f;
    [Tooltip("The clamped minimum distance between the camera and focus object")]
    public float minZoomRange = 1f;
    [Tooltip("The clamped maximum distance between the camera and focus object")]
    public float maxZoomRange = Mathf.Infinity;
    
    [Header("Focus radius settings")]
    
    [Tooltip("Raise this value to only move the camera when the focus has left the specified radius"), Min(0f)]
    public float focusRadius = 1f;
    [Tooltip("The higher the value, the faster the camera will center at the focus point"), Range(0f, 1f)]
    public float focusCentering = 0.75f;

    [Header("Manual camera orbit settings")]

    [Tooltip("Whether or not to enable manual camera orbiting using the mouse")]
    public bool enableManualOrbit = true;
    [Tooltip("The min angle to clamp camera's vertical orbit to"), Range(-90f, 90f)]
    public float minVerticalAngle = -30f;
    [Tooltip("The max angle to clamp camera's vertical orbit to"), Range(-90f, 90f)]
    public float maxVerticalAngle = 60f;
    [Tooltip("If mouse direction should match camera orbit direction, (mouse left = camera left)")]
    public bool invertAxis = false;
    
    [Header("Automatic camera alignment settings")]
    
    [Tooltip("Whether or not to enable automatic aligning of the camera orbit to be behind focus")]
    public bool enableAutomaticAligning = true;
    [Tooltip("The amount of seconds before automatically aligning camera orbit to be behind focus"), Min(0f)]
    public float alignDelay = 5f;
    [Tooltip("The range away from the target rotation the automatic rotation should be smoothed"), Range(0f, 90f)]
    public float alignSmoothRange = 45f;
    // End of inspector variables
    #endregion
    
    #region InputFloats
    // A main input script should write to these three floats
    [NonSerialized] public float cameraHorizontalInput;
    [NonSerialized] public float cameraVerticalInput;
    [NonSerialized] public float cameraZoomInput;
    #endregion

    #region PrivateVariables
    private Transform _transform;
    private Vector3 _focusPoint, _previousFocusPoint;
    private Quaternion _focusRotation, _previousFocusRotation;
    private Vector2 _cameraRotationEuler = new Vector2(45f, 0f);
    private float _lastManualRotationTime;
    #endregion

    private void OnValidate () {
        if (maxVerticalAngle < minVerticalAngle) {
            maxVerticalAngle = minVerticalAngle;
        }
        if (maxZoomRange < minZoomRange) {
            maxZoomRange = minZoomRange;
        }
    }
    
    private void Awake()
    {
        _transform = transform;
        _focusPoint = focus.position;
        
        _focusRotation = focus.localRotation;
        _previousFocusRotation = _focusRotation;
        transform.localRotation = Quaternion.Euler(_cameraRotationEuler);
    }

    private void LateUpdate()
    {
        UpdateFocusPoint();
        ManualZoom();
        Quaternion cameraRotation;
        bool automaticRotation = false; // bool to store the response of the method AutomaticRotation()
        bool manualRotation = false;
        bool flightRotation = false;

        if (enableManualOrbit)
        {
            manualRotation = ManualRotation();
        }

        if (enableAutomaticAligning)
        {
            automaticRotation = AutomaticRotation();
        }

        if (enableFlightRotations)
        {
            flightRotation = FlightRotation();
        }

        if (manualRotation || automaticRotation || flightRotation)
        {
            ConstrainAngles();
            cameraRotation = Quaternion.Euler(_cameraRotationEuler);
        }
        else
        {
            cameraRotation = transform.localRotation;
        }
        Vector3 lookDirection = cameraRotation * Vector3.forward;
        Vector3 cameraPosition = (_focusPoint - lookDirection * cameraDistance) + centerOffset;
        
        if (Physics.Raycast(_focusPoint, -lookDirection, out RaycastHit hit, cameraDistance))
        {
            cameraPosition = (_focusPoint - lookDirection * hit.distance) + centerOffset;
        }
        
        _transform.SetPositionAndRotation(cameraPosition, cameraRotation);
    }

    private void UpdateFocusPoint()
    {
        _previousFocusPoint = _focusPoint;
        Vector3 targetPoint = focus.position;
        if (focusRadius > 0f)
        {
            float distance = Vector3.Distance(targetPoint, _focusPoint);
            float t = 1f;
            if (distance > 0.01f && focusCentering > 0f)
            {
                t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
            }
            if (distance > focusRadius)
            {
                t = Mathf.Min(t, focusRadius / distance);
            }
            _focusPoint = Vector3.Lerp(targetPoint, _focusPoint, t);
        }
        else {
            _focusPoint = targetPoint;
        }
    }
    
    private bool ManualRotation()
    {
        Vector2 input = new Vector2(-cameraVerticalInput, cameraHorizontalInput);
        const float e = 0.001f;

        if (input.x < -e || input.x > e || input.y < -e || input.y > e)
        {
            if (!invertAxis)
            {
                _cameraRotationEuler += rotationSpeed * Time.unscaledDeltaTime * input;
            }
            else
            {
                _cameraRotationEuler -= rotationSpeed * Time.unscaledDeltaTime * input;
            }
            _lastManualRotationTime = Time.unscaledTime;
            return true;
        }
        return false;
    }

    private void ConstrainAngles()
    {
        _cameraRotationEuler.x = Mathf.Clamp(_cameraRotationEuler.x, minVerticalAngle, maxVerticalAngle);

        if (_cameraRotationEuler.y < 0f)
        {
            _cameraRotationEuler.y += 360f;
        }
        else if (_cameraRotationEuler.y >= 360f)
        {
            _cameraRotationEuler.y -= 360f;
        }
    }
    
    private bool AutomaticRotation()
    {
        // If it has been alignDelay secs since last ManualRotation or over
        if (Time.unscaledTime - _lastManualRotationTime < alignDelay)
        {
            return false;
        }
        
        // Calculates movement vector between where the object is now and where it was a little while ago
        Vector2 movement = new Vector2
        (
            _focusPoint.x - _previousFocusPoint.x,
            _focusPoint.z - _previousFocusPoint.z
        );
        // Returns the squared length of movement vectors
        float movementDeltaSqr = movement.sqrMagnitude;
        if (movementDeltaSqr < 0.000001f)
        {
            return false;
        }
        float headingAngle = GetAngle(movement / Mathf.Sqrt(movementDeltaSqr));
        float deltaAbs = Mathf.Abs(Mathf.DeltaAngle(_cameraRotationEuler.y, headingAngle));
        float rotationChange = rotationSpeed * Mathf.Min(Time.unscaledDeltaTime, movementDeltaSqr);
        if (deltaAbs < alignSmoothRange)
        {
            rotationChange *= deltaAbs / alignSmoothRange;
        }
        else if (180f - deltaAbs < alignSmoothRange)
        {
            rotationChange *= (180f - deltaAbs) / alignSmoothRange;
        }
        _cameraRotationEuler.y =
            Mathf.MoveTowardsAngle(_cameraRotationEuler.y, headingAngle, rotationChange);
        
        return true;
    }

    private bool FlightRotation()
    {
        _cameraRotationEuler = _focusRotation.eulerAngles;
        _previousFocusRotation = _focusRotation;
       return true;
    }
    
    private static float GetAngle(Vector2 direction)
    {
        float angle = Mathf.Acos(direction.y) * Mathf.Rad2Deg;
        return direction.x < 0f ? 360f - angle : angle;
    }

    private void ManualZoom()
    {
        if (zoom)
        {
            cameraDistance += cameraZoomInput * zoomMultiplier;
            cameraDistance = Mathf.Clamp(cameraDistance, minZoomRange, maxZoomRange);
        }
    }
}
