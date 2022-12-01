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
    private SpriteRenderer spriteRenderer;

    //sounds
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip pushBackSound;
    private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gunJoint = gun.gameObject.GetComponent<SpringJoint2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        float now = Time.time;

        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        if (isGrounded)
            lastGrounded = now;

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

        if (Input.GetKey(KeyCode.Space) && lastGrounded + coyoteTime > now && lastJump + jumpColldown < now)
        {
            rb.AddForce(jumpSpeed * Vector2.up);
            audioSource.PlayOneShot(jumpSound, 0.1f);
            lastJump = now;
        }


    }

    private void Flip()
    {
        Vector3 anch = gunJoint.connectedAnchor;
        anch.x *= -1;
        gun.gameObject.transform.Rotate(0, 180f, 0);
        gunJoint.connectedAnchor = anch;
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 dir = (transform.position - collision.transform.position).normalized;
            if (dir.y > 0.8)
            {
                collision.gameObject.GetComponent<Temperature>().Heat(1, collision.transform.position);
                rb.AddForce(dir * 10, ForceMode2D.Impulse);
                audioSource.PlayOneShot(jumpSound, 0.1f);
            }
            else
            {
                rb.AddForce(dir * 10, ForceMode2D.Impulse);
                audioSource.PlayOneShot(pushBackSound, 0.1f);
            }
        }
    }
}
