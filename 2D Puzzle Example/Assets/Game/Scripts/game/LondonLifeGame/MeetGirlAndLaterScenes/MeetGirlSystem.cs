using com;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MeetGirlSystem : MonoBehaviour
{
    public static MeetGirlSystem instance;

    [SerializeField] PanelCanvasGroupSwitcher _pcgs;

    [SerializeField] Image girlView;
    [SerializeField] CanvasGroup girlRequestView;
    [SerializeField] GameObject mythinkView;

    [SerializeField] CanvasGroup[] MyOptionsView;

    private void Awake()
    {
        instance = this;

    }

    public void Reinit()
    {
        _pcgs.Show(true, true);
        StartCoroutine(GrilScene());
        SceneTextSystem.instance.SetText(4, false);
        // image1.DOFade(1, 1);
        //   clockCg.DOFade(1, 1).SetDelay(0.8f);
    }

    IEnumerator GrilScene()
    {
        girlView.color = new Color(1, 1, 1, 0);
        girlRequestView.alpha = 0;
        foreach (var opv in MyOptionsView)
            opv.alpha = 0;

        mythinkView.SetActive(false);
        yield return new WaitForSeconds(1);
        girlView.DOFade(1, 1.2f);
        yield return new WaitForSeconds(1.2f);
        girlRequestView.DOFade(1, 1.2f);
        yield return new WaitForSeconds(1.5f);
        mythinkView.SetActive(true);
        yield return new WaitForSeconds(2);
        foreach (var opv in MyOptionsView)
            opv.DOFade(1, 1);
    }

    IEnumerator EndScene()
    {
        yield return new WaitForSeconds(2);
        _pcgs.Show(false, false);

    }

    public void End()
    {
        StartCoroutine(EndScene());
    }
}