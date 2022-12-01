using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{

    public Transform target;
    [SerializeField] private float lerpFactor = 0.2f;
    private void LateUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, target.position, lerpFactor);
    }
}
