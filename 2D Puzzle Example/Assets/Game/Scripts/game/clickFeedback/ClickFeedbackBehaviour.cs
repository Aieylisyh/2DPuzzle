using System.Collections;
using com;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ClickFeedbackBehaviour : MonoBehaviour
{
    public Image img;
    public Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
        transform.localScale = Vector3.one * 0.06f;
        transform.DOScale(0.24f, 0.5f);
        img.DOFade(0, 0.5f);
        GetComponent<RectTransform>().anchoredPosition3D = new Vector3(mousePos.x, mousePos.y, 0);
        SoundSystem.instance.Play("item");
    }
}
