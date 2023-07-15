using System.Collections;
using com;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ClickFeedbackBehaviour : MonoBehaviour
{
    public Image img;
    public Vector3 mousePos;
    public float rx;
    public float ry;
    public float offsetX;
    public float offsetY;
    void Start()
    {
        Destroy(gameObject, 1);

        transform.localScale = Vector3.one * 0.06f;
        transform.DOScale(0.24f, 0.5f);
        img.DOFade(0, 0.5f);
        img.raycastTarget = false;

        var p = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.Log(mousePos + " " + p);
        var finalPos = new Vector2(p.x * rx + offsetX, p.y * ry + offsetY);
        transform.position = finalPos;

        var rect = GetComponent<RectTransform>();
        var anchoredPos = rect.anchoredPosition3D;
        anchoredPos.z = 0;
        rect.anchoredPosition = anchoredPos;
        //GetComponent<RectTransform>().anchoredPosition3D = new Vector3(finalPos.x, finalPos.y, 0);

        SoundSystem.instance.Play("click");
    }
}
