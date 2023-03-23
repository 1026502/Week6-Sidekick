using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSwing : MonoBehaviour
{

    public LineRenderer lr;
    public Transform grapplecube;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();

        
    }

    // Update is called once per frame
    void Update()
    {

        lr.SetPosition(0, this.gameObject.transform.position);
        lr.SetPosition(1, grapplecube.transform.position);

    }
}
