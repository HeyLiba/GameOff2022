using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float level = 1f;
    [SerializeField] Color expiredColor = Color.gray;
    [SerializeField] private bool expired = false;
    [SerializeField] GameObject destroyEffect;
    private Rigidbody2D rb;

    private void Start()
    {
        if (expired)
            GetComponent<SpriteRenderer>().color = expiredColor;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //NOTE: only ExpiredBullet layer can collide with player and enemy! 
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            if (!expired)
            {
                collision.gameObject.GetComponent<Temperature>().Heat(level, collision.GetContact(0).point);
                collision.gameObject.GetComponent<Rigidbody2D>()
                    .AddForce((collision.transform.position - transform.position).normalized * 10);
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            }
            else
            {
                collision.gameObject.GetComponent<Inventory>().AddAmmo();
            }
            DestroyBullet();
        }
    }

    private void FixedUpdate()
    {
        if(rb.velocity.magnitude < 10)
        {
            expireBullet();
        }
    }

    private void expireBullet()
    {
        gameObject.layer = LayerMask.NameToLayer("ExpiredBullet");
        GetComponent<SpriteRenderer>().color = expiredColor;
        expired = true;
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
