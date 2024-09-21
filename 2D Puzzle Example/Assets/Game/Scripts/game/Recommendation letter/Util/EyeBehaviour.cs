using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EyeBehaviour : MonoBehaviour
{
    [SerializeField] RectTransform eyeFillUp;
    [SerializeField] RectTransform eyeFillDown;

    [SerializeField] RectTransform eyeBowUp;
    [SerializeField] RectTransform eyeBowDown;

    void Start()
    {

    }

    public void ToggleEyeBlink(bool b)
    {
        StopAllCoroutines();
        if (b)
        {
            StartCoroutine(EyeLoop());
            return;
        }

        SetClose();
    }

    public void ToggleShow(bool b)
    {
        gameObject.SetActive(b);
    }

    IEnumerator EyeLoop()
    {
        SetClose();
        int i = 2;
        while (i >= 0)
        {
            i--;
            DoOpen();
            yield return new WaitForSeconds(duration + 0.2f);
            DoClose();
            yield return new WaitForSeconds(duration + 0.2f);
        }
        RLSystem.instance.ToggleContinueButton(true);
        //RLSystem.instance.sceneSwitcher.Set(SceneId.Calender);
    }

    void SetOpen()
    {
        SetViewState(eyeFillUp, 400, 1f, true);
        SetViewState(eyeFillDown, -400, 1, true);
        SetViewState(eyeBowUp, 0, 1.2f, true);
        SetViewState(eyeBowDown, 600, -1.2f, true);
    }

    void SetClose()
    {
        SetViewState(eyeFillUp, 0, 1, true);
        SetViewState(eyeFillDown, 0, 1, true);
        SetViewState(eyeBowUp, -400, 0.36f, true);
        SetViewState(eyeBowDown, 600, -0.36f, true);
    }

    void DoOpen()
    {
        SetViewState(eyeFillUp, 400, 1, false);
        SetViewState(eyeFillDown, -400, 1, false);
        SetViewState(eyeBowUp, 0, 1.2f, false);
        SetViewState(eyeBowDown, 600, -1.2f, false);
    }

    void DoClose()
    {
        SetViewState(eyeFillUp, 0, 1, false);
        SetViewState(eyeFillDown, 0, 1, false);
        SetViewState(eyeBowUp, -400, 0.36f, false);
        SetViewState(eyeBowDown, 600, -0.36f, false);
    }

    [SerializeField] float duration;
    void SetViewState(RectTransform rect, float posY, float scale, bool instant)
    {
        rect.DOKill();
        var ac0 = rect.anchoredPosition;
        var ac1 = ac0;
        ac1.y = posY;
        var sc0 = rect.localScale;
        var sc1 = sc0;
        sc1.y = scale;

        if (instant)
        {
            rect.anchoredPosition = ac1;
            rect.localScale = sc1;
        }
        else
        {
            rect.DOAnchorPos(ac1, duration);
            rect.DOScale(sc1, duration);
        }

    }
}