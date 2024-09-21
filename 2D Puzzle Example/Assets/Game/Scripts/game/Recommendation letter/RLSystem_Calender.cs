using com;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public partial class RLSystem : MonoBehaviour
{
    [SerializeField] CanvasGroup desk;

    [SerializeField] CanvasGroup calendar;

    [SerializeField] RectTransform[] onDeskItems;

    public void InitCalenderScene()
    {
        /*
        //stage1;
        fade in black, wear clother sound
        fade out show desk
        user click on papers
        all papers clicked, then focus on calender

        user click calender, then show big calender
        show dialogs
        show continue button, user click continue button

        show check list
        user click first item
        show checkmark
        show continue button, user click continue button

        fade in black, walk sound
        fade out show map
        show dialogs Which room should I go?...
        user click on a room in map.

         */

        foreach (var f in onDeskItems)
        {
            f.gameObject.SetActive(true);
        }
    }

    IEnumerator CalenderScene_Start()
    {
        yield return new WaitForSeconds(1);
    }

    public void OnClickPaperOnDesk(RectTransform rt)
    {
        SoundSystem.instance.Play("eat");
        rt.DOKill();
        rt.DOScale(0.5f, 0.5f).SetEase(Ease.InBack).OnComplete(() => { rt.gameObject.SetActive(false); CheckDeskSceneEnd(); });
    }

    void CheckDeskSceneEnd()
    {
        foreach (var f in onDeskItems)
        {
            if (f.gameObject.activeSelf)
                return;
        }

        ToggleContinueButton(true);
    }

    public void OnClickExitCalender()
    {
        //Show check list
    }
}
