using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TipSystem : MonoBehaviour
{
    public static TipSystem instance;

    public CanvasGroup cg;
    public TMPro.TextMeshProUGUI text;
    // Use this for initialization
    void Awake()
    {
        instance = this;
    }

    public void ShowText(string content, bool autoHide)
    {
        text.text = content;
        cg.DOKill();
        cg.alpha = 0;
        cg.DOFade(1, 1.5f);

        if (autoHide)
        {
            cg.DOFade(0, 1.5f).SetDelay(3);
        }
    }

    public void HideText()
    {
        cg.DOFade(0, 1.5f);
    }
}