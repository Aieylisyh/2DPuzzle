using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool up;
    public bool down;
    public bool left;
    public bool right;
    public GameObject movePlayer;
    public Vector2 moveDirection;
    public float moveSpeed;
    public bool move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(up)
        {
            moveDirection = Vector2.up;
        }
        if(down)
        {
            moveDirection = Vector2.down;
        }
        if(left)
        {
            moveDirection = Vector2.left;
        }
        if(right)
        {
            moveDirection = Vector2.right;
        }

        if(move)
        {
            DoMovePlayer();
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            move = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            move = false;
        }
    }
    void DoMovePlayer()
    {
        movePlayer.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
