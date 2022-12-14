using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootForce = 20.0f;
    [SerializeField] private float gunKnockBackForce = 0.1f;
    [SerializeField] private float playerKnockBackForce = 4.0f;
    [SerializeField] private float shootCoolDown = 1.5f;
     private float lastShoot = -999f;
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Rigidbody2D gunRb;

    private Inventory inventory;

    [SerializeField] private AudioClip shootSound;
    private AudioSource audioSource;


    private void Start()
    {
        inventory = GetComponent<Inventory>();    
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && lastShoot+shootCoolDown < Time.time)
        {
            Shoot();
            lastShoot = Time.time;
        }
    }

    void Shoot()
    {
        if (inventory.UseAmmo())
        {
            audioSource.PlayOneShot(shootSound, 0.2f);
            Rigidbody2D bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.AddForce(shootForce * shootPoint.transform.right, ForceMode2D.Impulse);
            gunRb.AddForce(-gunKnockBackForce * shootPoint.transform.right, ForceMode2D.Impulse);
            playerRb.AddForce(-playerKnockBackForce * shootPoint.transform.right, ForceMode2D.Impulse);
        }
    }
}
