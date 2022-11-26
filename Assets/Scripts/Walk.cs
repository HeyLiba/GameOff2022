using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    //Rays
    [SerializeField] Vector2 frontRay = Vector2.right * 2;
    [SerializeField] Vector2 upRay = Vector2.up *2;
    [SerializeField] Vector2 jumpRay = new Vector2(2,2);

    //Movement
    [SerializeField] float moveSpeed = 5;
    private float moveDirX = +1;
    [SerializeField] float jumpSpeed = 5;
    private bool isGrounded = false;
    private Rigidbody2D rb;

    [SerializeField] private float jumpColldown = 0.6f;
    [SerializeField] private float coyoteTime = 0.2f;
    private float lastJump = -999.0f;
    private float lastGrounded = -999.0f;

    // rays and masks
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new(1.0f, 0.5f);
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask rayMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float now = Time.time;
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        if (isGrounded)
            lastGrounded = now;

        RaycastHit2D hitFront = Physics2D.Raycast(transform.position, frontRay.normalized, frontRay.magnitude, rayMask);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, upRay.normalized, upRay.magnitude, rayMask);
        RaycastHit2D hitJump = Physics2D.Raycast(transform.position, jumpRay.normalized, jumpRay.magnitude, rayMask);

        // hit debugs
        if (hitFront)
            Debug.DrawLine(transform.position, hitFront.point, Color.blue);
        if (hitUp)
            Debug.DrawLine(transform.position, hitUp.point, Color.green);
        if (hitJump)
            Debug.DrawLine(transform.position, hitJump.point, Color.red);
        if (hitFront && hitJump)
        {
            moveDirX *= -1;
            jumpRay.x *= -1;
            frontRay.x *= -1;
        }
        else

        if (hitFront && isGrounded && !hitUp && !hitJump 
            && lastGrounded + coyoteTime > now 
            && lastJump + jumpColldown < now)
        {
            rb.AddForce(jumpSpeed * Vector2.up, ForceMode2D.Impulse);
            lastJump = now;
        }

        if (isGrounded)
            rb.AddForce(moveDirX * moveSpeed * Vector2.right);

    }
}
