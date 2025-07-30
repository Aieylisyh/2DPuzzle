using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float direction;           // 1 = 右, -1 = 左, 0 = 停

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        instance = this;
    }

    private void Update()
    {
       // if (Input.GetMouseButtonDown(0))
            //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 判断点击在角色左或右

        // 可选：松开即停
        if (Input.GetMouseButtonUp(0))
            direction = 0f;
    }

    public void MoveRight()
    {
        direction = 1f;
    }

    public void MoveLeft()
    {
        direction = -1f;
    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

}