using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] Color expiredColor = Color.gray;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.layer = LayerMask.NameToLayer("ExpiredBullet");
        GetComponent<SpriteRenderer>().color = expiredColor;
    }
}
