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
    [SerializeField] private float shootDelay = 2f;
    private float lastShoot = -999f;
    [SerializeField] private LayerMask rayMask;

    private Inventory inventory;

    private void Start()
    {
        inventory = GetComponent<Inventory>();   
    }

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
        if (inventory.UseAmmo())
        {
            yield return new WaitForSeconds(shootDelay);
            Rigidbody2D bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.AddForce(shootForce * transform.right, ForceMode2D.Impulse);
        }
    }
}
