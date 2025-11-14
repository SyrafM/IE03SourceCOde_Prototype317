using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CCTVLookScript : MonoBehaviour
{

    [Header("Scanning Settings")]
    [Tooltip("How far left/right (in degrees) the camera will rotate from its starting rotation.")]
    public float scanAngle = 60f;

    [Tooltip("Rotation speed in degrees per second.")]
    public float scanSpeed = 30f;

    [Tooltip("How long (in seconds) to pause at the leftmost and rightmost scan points.")]
    public float scanStop = 1f;

    [Header("Rotation Offset")]
    [Tooltip("Offset (in degrees) added to the base forward direction. Use this to adjust the forward direction without rotating the object in the editor.")]
    public float forwardOffset = 0f; // <-- NEW: stable offset

    private float startYRot;                 // world Y at Start
    private float baseYRot;                  // startYRot + forwardOffset
    private Quaternion baseRotation;         // Quaternion representing base (center) rotation
    private bool rotatingRight = true;
    private bool isStopping = false;

    [HideInInspector]
    public float currentYRotOffset = 0f;     // tracks current scan offset in degrees

    void Start()
    {
        // record the world-space starting Y rotation (use transform.eulerAngles.y)
        startYRot = transform.eulerAngles.y;

        // compute base Y (start + offset) and the base quaternion
        baseYRot = startYRot + forwardOffset;
        baseRotation = Quaternion.Euler(23f, baseYRot, 0f);

        // ensure initial rotation is exactly the base rotation
        transform.rotation = baseRotation;
    }

    void Update()
    {
        if (GameStateManagerScript.instance.getcamera())
        {
            if (!isStopping)
                HandleScanning();
        }
    }

    void HandleScanning()
    {
        // delta in degrees this frame (positive when rotatingRight, negative otherwise)
        float delta = scanSpeed * Time.deltaTime * (rotatingRight ? 1f : -1f);
        currentYRotOffset += delta;

        // clamp within scan range and trigger stop coroutine when hitting edges
        if (currentYRotOffset >= scanAngle)
        {
            currentYRotOffset = scanAngle;
            StartCoroutine(StopAtEdge(false));
        }
        else if (currentYRotOffset <= -scanAngle)
        {
            currentYRotOffset = -scanAngle;
            StartCoroutine(StopAtEdge(true));
        }

        // Apply rotation: baseRotation * rotationAroundY(currentYRotOffset)
        Quaternion sweepRot = Quaternion.Euler(0f, currentYRotOffset, 0f);
        transform.rotation = baseRotation * sweepRot;
    }

    IEnumerator StopAtEdge(bool nextDirectionRight)
    {
        isStopping = true;
        yield return new WaitForSeconds(scanStop);
        rotatingRight = nextDirectionRight;
        isStopping = false;
    }
}

