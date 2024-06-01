using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 碰撞检测 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //条件：两个物体都有collider，且其中至少一个有rigidbody，collider相碰
        Debug.Log("我撞到了 " + collision.collider.gameObject.name);
    }

     private void OnCollisionExit(Collision collision)
    {
        Debug.Log("我离开了 " + collision.collider.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        //条件：两个物体都有collider，且其中至少一个collider的isTrigger打勾，且其中至少一个有rigidbody，collider相碰
         Debug.Log("我TriggerEnter了 " + other.gameObject.name);
    }

     private void OnTriggerExit(Collider other)
    {
         Debug.Log("我OnTriggerExit了 " + other.gameObject.name);
    }
}
