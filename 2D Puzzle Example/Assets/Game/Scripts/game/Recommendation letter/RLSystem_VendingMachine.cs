using com;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class RLSystem : MonoBehaviour
{
    public GameObject pp2;

    public void OnClick贩卖机()
    {
        var s = new List<string>();
        s.Add("Maybe I should give Mrs.Fernandes something she likes...");
        DialogBehaviour.instance.SetDialog(s);
        DialogBehaviour.instance.Show();
    }

    public void OnClick其他货物()
    {
        var s = new List<string>();
        s.Add("I don't think she likes this...");
        DialogBehaviour.instance.SetDialog(s);
        DialogBehaviour.instance.Show();
    }

    [SerializeField] RectTransform shakeMachineTrans;

    public void OnClick巧克力()
    {
        var s = new List<string>();
        s.Add("Yes, I remember there were chocolates on her desk!");
        DialogBehaviour.instance.SetCallback(
       () =>
       {
           ToggleContinueButton(true);
           SoundSystem.instance.Play("drop");
           shakeMachineTrans.DOShakeAnchorPos(0.5f, 20, 12);
       }
       );
        DialogBehaviour.instance.SetDialog(s);
        DialogBehaviour.instance.Show();
    }

    public void InitVendingMachineScene()
    {
        vendingMachineTodoListCg.blocksRaycasts = true;
        vendingMachineTodoListCg.alpha = 1;
        vendingMachineTodoListCross.gameObject.SetActive(false);
        vendingMachineTodoListNotes.gameObject.SetActive(false);
    }

    [SerializeField] CanvasGroup vendingMachineTodoListCg;

    void OnVendingMachineTodoListFinished()
    {
        vendingMachineTodoListCg.DOFade(0, 1).OnComplete(() =>
        {
            {
                vendingMachineTodoListCg.blocksRaycasts = false;
                var s = new List<string>();
                s.Add("<color=#555555>(In front of Vending Machine)</color>");
                s.Add("May be I should by some snacks for Mrs. Fernandes, what does she like to eat?");
                DialogBehaviour.instance.SetDialog(s);

                DialogBehaviour.instance.Show();
            }
        });
    }

    [SerializeField] Image vendingMachineTodoListCross;
    [SerializeField] Image vendingMachineTodoListNotes;
    public void OnClickVendingMachineTodoListItem()
    {
        ShowCheckMark(vendingMachineTodoListCross);
        StartCoroutine(DelayAction(2.5f, () => { ShowCheckMark(vendingMachineTodoListNotes); }));
        StartCoroutine(DelayAction(6f, () => { OnVendingMachineTodoListFinished(); }));
    }
}