using com;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public partial class RLSystem : MonoBehaviour
{
    [SerializeField] RectTransform deskScene;
    [SerializeField] RectTransform bigCalender;
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
        bigCalender.gameObject.SetActive(false);
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

        FocusOnCalender();
        //ToggleContinueButton(true);
    }

    [SerializeField] Button _calenderButton;
    [SerializeField] RectTransform _calenderFocus;
    [SerializeField] float _calenderFocusSize = 1.5f;
    void FocusOnCalender()
    {
        var ac = _calenderFocus.anchoredPosition;
        deskScene.DOAnchorPos(-ac, 1);
        deskScene.DOScale(_calenderFocusSize, 1).OnComplete(() =>
        {
            _calenderButton.enabled = true;
        });
    }
    public void OnClickEnterCalender()
    {
        bigCalender.gameObject.SetActive(true);
    }
}
