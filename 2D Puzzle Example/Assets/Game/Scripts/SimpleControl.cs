using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleControl : MonoBehaviour
{
    public Rigidbody rb;
    public float cooldown = 1f;
    public float force = 100;
    private float _nextAddForceTime;
    public Camera cam;

    void Start()
    {
        _nextAddForceTime = 0;
    }


    void Update()
    {
        if (Time.time >= _nextAddForceTime)
        {
            AddForce();
            _nextAddForceTime = Time.time + cooldown;
        }

        CheckClicked();
    }

    void AddForce()
    {
        rb.AddForce(Vector3.up * force);
    }

    //public int layermask;
    public int layermask;

    void CheckClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);

            //public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
            RaycastHit raycastHit;

            Physics.Raycast(mouseWorldPosition, cam.transform.forward, out raycastHit, 100f, layermask);

            if (raycastHit.collider != null)
            {
                Debug.Log("ÉäÏß¼ì²â½á¹û" + raycastHit.collider.gameObject.name);
            }
        }
    }

    void TestValue(int t)
    {
        t = t + 10;
    }

    void TestValue2(ref int t)
    {
        t = t + 10;
    }

    int TestAndReturnValue(int t)
    {
        t = t + 10;
        return t;
    }
}
