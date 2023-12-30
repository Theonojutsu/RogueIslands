using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("References")]
    private PlayerMovementGrappling pm;
    [SerializeField] Transform cam;
    public Transform gunTip;
    [SerializeField] LayerMask whatIsGrappleable;
    [SerializeField] LineRenderer lr;

    [Header("Grappling")]
    [SerializeField] float maxGrappleDistance;
    [SerializeField] float grappleDelayTime;
    [SerializeField] float overshootYAxis;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    [SerializeField] float grapplingCd;
    private float grapplingCdTimer;

    [Header("Input")]
    [SerializeField] KeyCode grappleKey = KeyCode.Mouse1;

    private bool grappling;

    private void Start()
    {
        pm = GetComponent<PlayerMovementGrappling>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey)) StartGrapple();

        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime;
    }

    private void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;

        grappling = true;

        pm.freeze = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }
    }

    private void ExecuteGrapple()
    {
        pm.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        pm.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        pm.freeze = false;

        grappling = false;

        grapplingCdTimer = grapplingCd;
    }

    public bool IsGrappling()
    {
        return grappling;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
