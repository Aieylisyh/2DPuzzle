using UnityEngine;
using UnityEngine.UI;

public class AppleMove : MonoBehaviour
{
    public float angularSpeed;

    private float angle;
    public float radius;

    private void Awake()
    {
        Debug.Log("--->执行了 Awake");
    }
    private void Start()
    {
        Debug.Log("--->执行了 Start");
        //var img = GetComponent<Image>();
        //img.color = Color.black;
        angle = 0;
    }

    private void Update()
    {
        var rect = GetComponent<RectTransform>();

        var p = GetPointAlongCircle();
        rect.anchoredPosition = p;
    }

    Vector2 GetPointAlongCircle()
    {
        angle += angularSpeed * Time.deltaTime;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
    }
}