using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public PlayerSwing ps;
    public float moveSpeed;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    //groundcheck
    public float playerheight;
    public float groundDrag;
    public LayerMask whatisGround;
    public bool grounded;


    //jumping
    public float jumpForce;
    public float jumpcd;
    public float airmultiplier;
    public bool rdyjump;

    //Swinging
    public float swingSpeedMax;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<PlayerSwing>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; 
    }

    // Update is called once per frame
    void Update()
    {
        //linked to capsule size.
        grounded = Physics.Raycast(transform.position, Vector3.down, playerheight * 0.5f + 0.2f, whatisGround); 
        MyInput();
        SpeedControl();

        //drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        if (ps.isswinging)
        {
            moveSpeed = swingSpeedMax;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();

        
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.Space) && rdyjump && grounded)
        {
            rdyjump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpcd);
        }

        if (Input.GetKey(KeyCode.Period))
        {
            int i = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(i);
        }

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!grounded)
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airmultiplier, ForceMode.Force);
    }

    private void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    private void ResetJump()
    {
        rdyjump = true;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > (moveSpeed*2))
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed * 2;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
