using UnityEngine;

/// <summary>
/// Robust 2D camera follow for orthographic scenes.
/// Attach to Main Camera. Assign a Player transform.
/// Includes smoothing, look-ahead, and optional world-bounds clamping.
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(Camera))]
public class CameraFollow2DPro : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("The transform to follow (e.g., Player).")]
    public Transform target;

    [Header("Positioning")]
    [Tooltip("Offset from target in world units. For 2D, set z to -10 so camera sits 'in front'.")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("Smoothing")]
    [Tooltip("0 = instant snap, 1 = never catch up. Typical: 0.1 - 0.25.")]
    [Range(0f, 1f)]
    public float smoothFactor = 0.15f;

    [Tooltip("Max distance camera can move in a single frame (prevents tunneling on teleports). 0 = unlimited.")]
    public float maxStepPerFrame = 0f;

    [Header("Look-Ahead (Optional)")]
    [Tooltip("Enable look-ahead in the direction of target velocity.")]
    public bool useLookAhead = true;

    [Tooltip("How far to look ahead at max speed (world units).")]
    public float lookAheadDistance = 1.5f;

    [Tooltip("Velocity magnitude at which look-ahead reaches full distance.")]
    public float lookAheadAtSpeed = 8f;

    [Header("World Bounds (Optional)")]
    [Tooltip("Set to true to clamp camera within world bounds (prevents showing outside your level).")]
    public bool clampToBounds = false;

    [Tooltip("A BoxCollider2D (or any AABB provider) that represents world bounds in world space.")]
    public BoxCollider2D worldBounds;

    // Cached components for performance
    private Camera _cam;

    // Internal state (no per-frame allocations)
    private Vector3 _currentVelocity = Vector3.zero; // reserved if switching to SmoothDamp later
    private Vector3 _desiredPosition;
    private Vector3 _smoothedPosition;

    // For computing look-ahead without requiring a Rigidbody2D on the target
    private Vector3 _lastTargetPos;
    private Vector3 _targetVelocity;
    private bool _hadInitialTargetSample = false;

    private void Awake()
    {
        // Cache camera and validate orthographic mode for 2D
        _cam = GetComponent<Camera>();
        if (_cam == null)
        {
            Debug.LogError("[CameraFollow2DPro] No Camera component found. Disabling script.");
            enabled = false;
            return;
        }

        if (!_cam.orthographic)
        {
            Debug.LogWarning("[CameraFollow2DPro] Camera is not orthographic. This script is designed for 2D; consider switching to orthographic.");
        }

        // Warn about missing target early
        if (target == null)
        {
            Debug.LogWarning("[CameraFollow2DPro] No target assigned. Camera will remain stationary until a target is set.");
        }
    }

    private void LateUpdate()
    {
        // If no target → do nothing but keep running (you may assign at runtime)
        if (target == null) return;

        // Compute target velocity (frame-to-frame). Robust if target lacks Rigidbody2D.
        if (_hadInitialTargetSample)
        {
            _targetVelocity = (target.position - _lastTargetPos) / Mathf.Max(Time.deltaTime, 0.0001f);
        }
        else
        {
            _targetVelocity = Vector3.zero;
            _hadInitialTargetSample = true;
        }
        _lastTargetPos = target.position;

        // Look-ahead vector scales with speed up to lookAheadAtSpeed
        Vector3 lookAhead = Vector3.zero;
        if (useLookAhead && lookAheadDistance > 0f && lookAheadAtSpeed > 0.01f)
        {
            float speed = _targetVelocity.magnitude;
            float t = Mathf.Clamp01(speed / lookAheadAtSpeed);
            Vector3 dir = _targetVelocity.sqrMagnitude > 0.0001f ? _targetVelocity.normalized : Vector3.zero;
            lookAhead = dir * (lookAheadDistance * t);
            // Zero Z for 2D; maintain camera Z via offset
            lookAhead.z = 0f;
        }

        // Desired camera position = target + offset + lookahead
        _desiredPosition = target.position + offset + lookAhead;

        // Smooth lerp (frame-rate independent enough for camera)
        // Note: Lerp uses a factor; we clamp to [0,1] for safety.
        float alpha = Mathf.Clamp01(1f - Mathf.Pow(1f - Mathf.Clamp01(smoothFactor), Time.timeScale > 0 ? Time.timeScale : 1f));
        _smoothedPosition = Vector3.Lerp(transform.position, _desiredPosition, alpha);

        // Limit per-frame jump if configured (helps when target teleports)
        if (maxStepPerFrame > 0f)
        {
            Vector3 delta = _smoothedPosition - transform.position;
            float dist = delta.magnitude;
            if (dist > maxStepPerFrame)
            {
                _smoothedPosition = transform.position + delta.normalized * maxStepPerFrame;
            }
        }

        // Clamp within world bounds (optional)
        if (clampToBounds && worldBounds != null)
        {
            _smoothedPosition = ClampToWorldBounds(_smoothedPosition);
        }

        // Finally set camera position
        transform.position = _smoothedPosition;
    }

    /// <summary>
    /// Clamp a camera position so the visible area stays within the world bounds.
    /// Assumes orthographic camera for 2D.
    /// </summary>
    private Vector3 ClampToWorldBounds(Vector3 camPos)
    {
        // Safety: if worldBounds is not enabled/valid, skip
        if (!worldBounds.enabled)
            return camPos;

        Bounds b = worldBounds.bounds;

        // Compute half extents of camera view in world units
        float vertExtent = _cam.orthographicSize;
        float horizExtent = vertExtent * _cam.aspect;

        // If the world is smaller than camera view, center inside to avoid NaNs
        float minX = b.min.x + horizExtent;
        float maxX = b.max.x - horizExtent;
        float minY = b.min.y + vertExtent;
        float maxY = b.max.y - vertExtent;

        // Edge case: level narrower/shorter than camera → lock to center
        float clampedX = (b.size.x < horizExtent * 2f) ? b.center.x : Mathf.Clamp(camPos.x, minX, maxX);
        float clampedY = (b.size.y < vertExtent * 2f) ? b.center.y : Mathf.Clamp(camPos.y, minY, maxY);

        // Preserve Z from incoming desired position (usually offset.z = -10)
        return new Vector3(clampedX, clampedY, camPos.z);
    }

    /// <summary>
    /// Public API to change target safely at runtime (e.g., after respawn).
    /// </summary>
    public void SetTarget(Transform newTarget, bool snapToTarget = false)
    {
        if (newTarget == null)
        {
            Debug.LogWarning("[CameraFollow2DPro] SetTarget called with null. Ignoring.");
            return;
        }

        target = newTarget;
        _hadInitialTargetSample = false; // recalc velocity baseline on next frame

        if (snapToTarget)
        {
            Vector3 snapped = newTarget.position + offset;
            if (clampToBounds && worldBounds != null)
                snapped = ClampToWorldBounds(snapped);
            transform.position = snapped;
        }
    }
}
