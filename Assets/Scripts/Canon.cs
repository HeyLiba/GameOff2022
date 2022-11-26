using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] Rigidbody2D bulletPrefab;
    [SerializeField] float shootForce = 10f;
    [SerializeField] float rayDistance = 10f;
    [SerializeField] private float shootCoolDown = 2f;
    private float lastShoot = -999f;
    [SerializeField] private LayerMask rayMask;
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, transform.right, rayDistance, rayMask);
        if (hit.collider)
        {
            Debug.DrawLine(hit.point, shootPoint.position, Color.black);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                float now = Time.time;
                if (lastShoot + shootCoolDown < now)
                {
                    StartCoroutine(Shoot());
                    lastShoot = now;
                }
            }
        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootCoolDown);
        Rigidbody2D bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        bullet.AddForce(shootForce * transform.right, ForceMode2D.Impulse);
    }
}
