using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] int ammo = 2;
    [SerializeField] private TMPro.TMP_Text ammoPopupPrefab;
    [SerializeField] private TMPro.TMP_Text plusOnePopupPrefab;
    [SerializeField] private Transform popupTarget;
    
    
    [SerializeField] private Rigidbody2D expiredBulletPrefap;
    [SerializeField] private float dropForce = 0.5f;

    public void AddAmmo()
    {
        ammo += 1;
        PlusOnePopup();
    }

    public bool UseAmmo()
    {
        if (ammo < 1)
        {
            AmmoPopup();
            return false;
        }
        else
        {
            ammo -= 1;
            AmmoPopup();
            return true;
        }
    }

    public int GetAmmo()
    {
        return ammo;
    }

    private void AmmoPopup()
    {
        if (ammo > 0)
        {
            TMPro.TMP_Text popup = Instantiate(ammoPopupPrefab, popupTarget.position, Quaternion.identity);
            popup.text = ammo.ToString();
        }
        else
        {
            TMPro.TMP_Text popup = Instantiate(ammoPopupPrefab, popupTarget.position, Quaternion.identity);
            popup.text = "out\nof\nammo";
            popup.fontSize /= 1.5f;
        }
    }

    private void PlusOnePopup()
    {
        Instantiate(plusOnePopupPrefab, popupTarget.position, Quaternion.identity);
    }


    public void DropAll()
    {
        for (int i = 0; i < ammo; i++)
        {
            Rigidbody2D b = Instantiate(expiredBulletPrefap, transform.position, Quaternion.identity);
            b.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);
        }
    }

    public void DropOne()
    {
        if (UseAmmo())
        {
            Rigidbody2D b = Instantiate(expiredBulletPrefap, transform.position, Quaternion.identity);
            b.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);
        }
    }
}
