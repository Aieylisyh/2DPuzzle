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
        ScreenEffectToggle.instance.ToggleDreamReality(false);
        foreach (var f in onDeskItems)
        {
            f.gameObject.SetActive(true);
        }
        bigCalender.gameObject.SetActive(false);

        var s = new List<string>();
        s.Add("All right");
        s.Add("just remember to bring all the latest version of the university application list.");
        s.Add("I am goanna need them today.");
        DialogBehaviour.instance.SetDialog(s);
        DialogBehaviour.instance.Show();
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
        UiImageScreenFader.instance.FadeWhite(
            () => { bigCalender.gameObject.SetActive(true); },
            0.4f, 0.4f, 0.1f
            );

        var s = new List<string>();
        s.Add("Today is the last day to apply.");
        s.Add("I really need to as Mrs.Fernandes for reference letter.");
        s.Add("I don't know what I was afraid of these days.");
        s.Add("Maybe it's just procrastination.");
        s.Add("Come on, you know she is nice, you did well on her Spanish Class...");
        s.Add("Let’s do it.");
        DialogBehaviour.instance.SetDialog(s);
        DialogBehaviour.instance.SetCallback(
         () =>
         {
             StartCoroutine(DelayAction(1.1f, () =>
             {
                 ToggleContinueButton(true);
             }));
         }
         );
        DialogBehaviour.instance.Show();

    }
}
