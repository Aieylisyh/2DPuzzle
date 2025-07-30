using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float direction;           // 1 = ÓÒ, -1 = ×ó, 0 = Í£

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ÅÐ¶Ïµã»÷ÔÚ½ÇÉ«×ó»òÓÒ
            direction = mousePos.x > transform.position.x ? 1f : -1f;
        }

        // ¿ÉÑ¡£ºËÉ¿ª¼´Í£
        if (Input.GetMouseButtonUp(0))
            direction = 0f;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

}