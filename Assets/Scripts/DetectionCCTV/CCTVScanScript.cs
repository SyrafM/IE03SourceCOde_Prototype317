using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVScanScript : MonoBehaviour
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
    public float forwardOffset = 0f;

    [Header("Detection Settings")]
    [Tooltip("Maximum distance the CCTV can detect.")]
    public float detectionRange = 10f;

    [Tooltip("Angle width (in degrees) of the CCTV’s vision cone.")]
    public float detectionAngle = 45f;

    [Tooltip("Tag for the Player object.")]
    public string playerTag = "Player";

    [Tooltip("Tag for obstruction objects (like walls).")]
    public string obstructionTag = "Obstruction";

    private float startYRot;
    private float baseYRot;
    private Quaternion baseRotation;
    private bool rotatingRight = true;
    private bool isStopping = false;
    public string caughtText;

    [HideInInspector]
    public float currentYRotOffset = 0f;

    private MeshRenderer meshRenderer;

    void Start()
    {
        // record the world-space starting Y rotation (use transform.eulerAngles.y)
        startYRot = transform.eulerAngles.y;

        // compute base Y (start + offset) and the base quaternion
        baseYRot = startYRot + forwardOffset;
        baseRotation = Quaternion.Euler(0f, baseYRot, 0f);

        // ensure initial rotation is exactly the base rotation
        transform.rotation = baseRotation;

        // Make the CCTV object invisible if it has a MeshRenderer
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
            meshRenderer.enabled = false;
    }

    void Update()
    {
        if (GameStateManagerScript.instance.getcamera())
        { 
            if (!isStopping)
                HandleScanning();

        DetectTargets();
    }
    }

    void HandleScanning()
    {
        float delta = scanSpeed * Time.deltaTime * (rotatingRight ? 1f : -1f);
        currentYRotOffset += delta;

        // clamp within scan range
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

    void DetectTargets()
    {
        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;

        // get all colliders in range
        Collider[] hits = Physics.OverlapSphere(origin, detectionRange);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag(playerTag))
            {
                Vector3 dirToTarget = (hit.transform.position - origin).normalized;
                float angleToTarget = Vector3.Angle(forward, dirToTarget);

                if (angleToTarget < detectionAngle / 2f)
                {
                    float distToTarget = Vector3.Distance(origin, hit.transform.position);

                    // Check for obstructions via Raycast
                    if (Physics.Raycast(origin, dirToTarget, out RaycastHit rayHit, distToTarget))
                    {
                        if (rayHit.collider.CompareTag(obstructionTag))
                        {
                            Debug.DrawRay(origin, dirToTarget * distToTarget, Color.yellow); // blocked
                            // Debug.Log("View blocked by: " + rayHit.collider.name);
                        }
                        else if (rayHit.collider.CompareTag(playerTag))
                        {
                            Debug.DrawRay(origin, dirToTarget * distToTarget, Color.red);
                            //HudController.instance.EnableCaughtText(caughtText);
                            GameStateManagerScript.instance.gameOver();
                        }
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // visualize detection range and angle in Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Vector3 leftBoundary = Quaternion.Euler(0, -detectionAngle / 2f, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, detectionAngle / 2f, 0) * transform.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * detectionRange);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * detectionRange);
    }
}
