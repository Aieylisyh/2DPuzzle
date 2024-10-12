using com;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SceneId
{
    None,

    Admission,
    Roof,
    Calender,
    Checklist,
    Corridor,

    Classroom1,
    VendingMachine,
    Classroom2,
    AdmissionEnding,
}

public partial class RLSystem : MonoBehaviour
{
    public static RLSystem instance;

    [SerializeField] Image _continueBtn;

    public SceneSwitcher sceneSwitcher;

    private void Awake()
    {
        instance = this;
    }

    public void StartScene(SceneId s)
    {
        ToggleContinueButton(false);
        btnBackToCorrider.SetActive(false);

        switch (s)
        {
            case SceneId.None:
                break;
            case SceneId.Admission:
                InitDreamAdmissionScene();
                break;
            case SceneId.Roof:
                eb.ToggleShow(true);
                eb.ToggleEyeBlink(true);
                break;
            case SceneId.Calender:
                InitCalenderScene();
                break;
            case SceneId.Checklist:
                InitCheckListScene();
                break;
            case SceneId.Corridor:
                InitCorridorScene();
                break;
            case SceneId.Classroom1:
                InitClassroom1Scene();
                break;
            case SceneId.VendingMachine:
                InitVendingMachineScene();
                break;
            case SceneId.Classroom2:
                InitClassroom2Scene();
                break;
            case SceneId.AdmissionEnding:
                InitAdmissionEndingScene();
                break;
        }
    }

    void InitDreamAdmissionScene()
    {
        envelopeClose.SetActive(false);
        envelopeOpen.SetActive(false);
        admissionFold.SetActive(false);
        envelopeHalf.SetActive(false);
        admission.SetActive(false);
        envelopeCloseButton.SetActive(false);
        ToggleContinueButton(false);
        DisplayEnvelope();
        ScreenEffectToggle.instance.ToggleDreamReality(true);
    }

    public void OnClickContinueButton()
    {
        switch (sceneSwitcher.crtId)
        {
            case SceneId.None:
                break;
            case SceneId.Admission:
                sceneSwitcher.Set(SceneId.Roof);
                break;
            case SceneId.Roof:
                var tt = 1.2f;
                UiImageScreenFader.instance.FadeInBlack(null, tt);
                StartCoroutine(DelayAction(tt, () =>
                {
                    SoundSystem.instance.Play("put on clothes");
                    sceneSwitcher.Set(SceneId.Calender);
                    StartCoroutine(DelayAction(2, () =>
                    {
                        UiImageScreenFader.instance.FadeOutBlack(null);
                    }));
                }));
                break;
            case SceneId.Calender:
                sceneSwitcher.Set(SceneId.Checklist);
                break;
            case SceneId.Checklist:
                sceneSwitcher.Set(SceneId.Corridor);
                break;
            case SceneId.Corridor:
                var fadeToBlackTime = 1.2f;
                UiImageScreenFader.instance.FadeInBlack(null, fadeToBlackTime);
                StartCoroutine(DelayAction(fadeToBlackTime, () =>
                {
                    sceneSwitcher.Set(SceneId.Classroom1);
                }));
                break;
            case SceneId.Classroom1:
                sceneSwitcher.Set(SceneId.VendingMachine);
                break;
            case SceneId.VendingMachine:
                sceneSwitcher.Set(SceneId.Classroom2);
                break;
            case SceneId.Classroom2:
                sceneSwitcher.Set(SceneId.AdmissionEnding);
                break;
            case SceneId.AdmissionEnding:
                //sceneSwitcher.Set(SceneId.AdmissionEnding);
                break;
        }
        ToggleContinueButton(false);
    }

    public void ToggleContinueButton(bool b)
    {
        var btn = _continueBtn;
        btn.gameObject.SetActive(b);
        if (b)
        {
            var c = btn.color;
            c.a = 0;
            btn.color = c;
            c.a = 1;
            btn.DOColor(c, 1f);
        }
    }

    IEnumerator DelayAction(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}