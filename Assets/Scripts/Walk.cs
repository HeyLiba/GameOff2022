using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    //Rays
    [SerializeField] Vector2 frontRay = Vector2.right * 5;
    [SerializeField] Vector2 upRay = Vector2.up *2;
    [SerializeField] Vector2 jumpRay = new Vector2(5,2);

    //Movement
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float jumpForce = 5;

    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        
    }
}
