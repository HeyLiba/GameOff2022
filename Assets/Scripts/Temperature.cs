using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temperature : MonoBehaviour
{
    public float MaxTemperature = 3f;
    private float temperature = 0f;
    public virtual void Heat(float level, Vector2 position)
    {
        temperature += level;
        CheckTemperature();
    }

    private void CheckTemperature()
    {
        if (temperature > MaxTemperature)
        {
            OverHeat();
        }
    }

    public virtual void OverHeat()
    {
        GetComponent<Inventory>().DropAll();
        Destroy(gameObject);
    }
}
