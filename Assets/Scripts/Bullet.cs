using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float level = 1f;
    [SerializeField] Color expiredColor = Color.gray;
    [SerializeField] private bool expired = false;

    private void Start()
    {
        if (expired)
            GetComponent<SpriteRenderer>().color = expiredColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //NOTE: only ExpiredBullet layer can collide with player and enemy! 
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            if (!expired)
            {
                collision.gameObject.GetComponent<Temperature>().Heat(level, collision.GetContact(0).point);
            }
            else
            {
                collision.gameObject.GetComponent<Inventory>().AddAmmo();
            }
            Destroy(gameObject);
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("ExpiredBullet");
            GetComponent<SpriteRenderer>().color = expiredColor;
            expired = true;
        }
    }
}
