using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool allowMove = true;
    public float moveSpeed = 5f;

    void FixedUpdate()
    {
        if(allowMove)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;

            transform.Translate(moveDirection * Time.deltaTime * moveSpeed, Space.World);
        }


        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            allowMove = false;
        }
        else
        {
            allowMove = true;
        }    
    }
}