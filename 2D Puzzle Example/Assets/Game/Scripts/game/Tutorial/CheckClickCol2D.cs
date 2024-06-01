using UnityEngine;

public class CheckClickCol2D : MonoBehaviour
{
    public Camera cam;
    public LayerMask layermask;
    public float distance = 1f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mouseWorldPosition);
            RaycastHit2D raycastHit = Physics2D.Raycast(mouseWorldPosition, Vector2.one, distance, layermask);
            if (raycastHit.collider != null)
            {
                Debug.Log("ÉäÏß¼ì²â½á¹û" + raycastHit.collider.gameObject.name);
            }
        }
    }
}
