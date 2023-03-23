using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public LayerMask pickupMask;
    public Camera playerCamera;
    public Transform pickupTarget;
    public float pickupRange;
    public Rigidbody currentObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentObject)
            {
                currentObject.useGravity = true;
                currentObject = null;
                return;
            }

            Ray CameraRay = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(CameraRay, out RaycastHit hitInfo, pickupRange, pickupMask))
            {
                currentObject = hitInfo.rigidbody;
                currentObject.useGravity = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (currentObject)
        {
            Vector3 DirectionToPoint = pickupTarget.position - currentObject.transform.position;
            float distanceToPoint = DirectionToPoint.magnitude;

            currentObject.velocity = DirectionToPoint * 12f * distanceToPoint;
        }
    }
}
