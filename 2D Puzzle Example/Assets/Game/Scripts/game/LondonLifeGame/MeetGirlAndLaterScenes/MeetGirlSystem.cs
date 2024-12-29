using com;
using DG.Tweening;
using System.Collections;
using TMPro;
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
    [SerializeField] TextMeshProUGUI textGirlSay;

    private void Awake()
    {
        instance = this;
    }

    public void Reinit()
    {
        _pcgs.Show(true, true);
        StartCoroutine(GrilScene());
        SceneTextSystem.instance.SetText(4, false);
        girlBegCount = 0;
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
        yield return new WaitForSeconds(1.5f);
        girlRequestView.DOFade(1, 1.2f);
        yield return new WaitForSeconds(1.5f);
        mythinkView.SetActive(true);
        //SoundSystem.instance.Play("bubble");
        yield return new WaitForSeconds(2);
        foreach (var opv in MyOptionsView)
            opv.DOFade(1, 1);
        SoundSystem.instance.Play("bubble");
    }

    int girlBegCount = 0;

    public void EndGirlBegScene()
    {
        girlBegCount++;
        if (girlBegCount == 2)
        {
            StartCoroutine(EndGirlBegSceneCo());
        }
    }

    IEnumerator EndGirlBegSceneCo()
    {
        SoundSystem.instance.Play("bubble");
        textGirlSay.text = "No, please consider my...";
        textGirlSay.rectTransform.DOShakeScale(1.6f, 1, 8);
        yield return new WaitForSeconds(4f);
        _pcgs.Show(false, false);
        DialogWithGirlSystem.instance.Reinit();
    }
}