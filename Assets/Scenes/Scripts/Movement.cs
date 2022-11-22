using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
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

    //move
    private float horizontalInput;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (isGrounded) {
            rb.AddForce(horizontalInput * speed * Vector2.right);
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
}
