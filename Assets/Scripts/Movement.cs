using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D gun;
    private SpringJoint2D gunJoint;
    const float MAX_GUN_DISTANCE = 3.0f;
    //speed
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float jumpSpeed = 40.0f;

    //jump
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new(1.0f,0.5f);
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpColldown = 0.6f;
    [SerializeField] private float coyoteTime = 0.2f;
    private float lastJump = -999.0f;
    private float lastGrounded = -999.0f;
    private bool isGrounded = false;
    private bool facingRight = true;

    //move
    private float horizontalInput;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gunJoint = gun.gameObject.GetComponent<SpringJoint2D>();
    }
    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (isGrounded) {
            rb.AddForce(horizontalInput * speed * Vector2.right);
        }
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }

        // clamp gun distance
        if (Vector3.Distance(gun.gameObject.transform.position,transform.position) > MAX_GUN_DISTANCE)
        {
            gun.gameObject.transform.position = transform.position + Vector3.ClampMagnitude(gun.gameObject.transform.position - transform.position, MAX_GUN_DISTANCE);
        }
    }

    private void Update()
    {
        float now = Time.time;

        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        if (isGrounded)
            lastGrounded = now;

        if (Input.GetKey(KeyCode.Space) && lastGrounded + coyoteTime > now && lastJump + jumpColldown < now)
        {
            rb.AddForce(jumpSpeed * Vector2.up);
            lastJump = now;
        }


    }

    private void Flip()
    {
        Vector3 anch = gunJoint.connectedAnchor;
        anch.x *= -1;
        gun.gameObject.transform.Rotate(180.0f,0, 0);
        gunJoint.connectedAnchor = anch;
        facingRight = !facingRight;
    }
}
