using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwing : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public Transform gunTip, cam, player;
    public LayerMask Grappleable;

    //swinging
    public float maxSwingDistance = 25f;
    private Vector3 swingPoint;
    private SpringJoint joint;
    public KeyCode swingKey = KeyCode.Mouse0;

    private Vector3 currentGrapplePosition;

    public bool isswinging;


    //joints
    public float jointSpring, jointDamper, jointMassScale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(swingKey)) StartSwinging();
        if (Input.GetKeyUp(swingKey)) StopSwinging();
      
    }

    void StartSwinging()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance, Grappleable))
        {
            swingPoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //customize values
            joint.spring = jointSpring;
            joint.damper = jointDamper;
            joint.massScale = jointMassScale;

            lineRenderer.positionCount = 2;

            isswinging = true; 
            
        }
    }

    void StopSwinging()
    {
        lineRenderer.positionCount = 0;
        Destroy(joint);

        isswinging = false;
    }

    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(1, swingPoint);
    }

    private void LateUpdate()
    {
        DrawRope();
    }
}
