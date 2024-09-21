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
}

public partial class RLSystem : MonoBehaviour
{
    public static RLSystem instance;

    [SerializeField] Image _continueBtn;
    public ScreenFader screenFader;
    public SceneSwitcher sceneSwitcher;


    private void Awake()
    {
        instance = this;
    }

    public void StartScene(SceneId s)
    {
        ToggleContinueButton(false);

        switch (s)
        {
            case SceneId.None:
                break;
            case SceneId.Admission:
                envelopeClose.SetActive(false);
                envelopeOpen.SetActive(false);
                admissionFold.SetActive(false);
                envelopeHalf.SetActive(false);
                admission.SetActive(false);
                envelopeCloseButton.SetActive(false);
                ToggleContinueButton(false);
                DisplayEnvelope();
                break;
            case SceneId.Roof:
                eb.ToggleShow(true);
                eb.ToggleEyeBlink(true);
                break;
            case SceneId.Calender:
                break;
            case SceneId.Checklist:
                break;
            case SceneId.Corridor:
                break;
        }
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
                sceneSwitcher.Set(SceneId.Calender);
                break;
            case SceneId.Calender:
                sceneSwitcher.Set(SceneId.Checklist);
                break;
            case SceneId.Checklist:
                sceneSwitcher.Set(SceneId.Corridor);
                break;
            case SceneId.Corridor:
                //TODO
                break;
        }
        ToggleContinueButton(false);
    }

    void ToggleContinueButton(bool b)
    {
        _continueBtn.gameObject.SetActive(b);
        if (b)
        {
            var c = _continueBtn.color;
            c.a = 0;
            _continueBtn.color = c;
            c.a = 1;
            _continueBtn.DOColor(c, 1f);
        }
    }

    IEnumerator DelayAction(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    public void CheckDagEndPlace(float distance)
    {
        if (envelopeDragDistanceThreshold > distance)
        {
            admission.SetActive(true);
            var img = admission.GetComponent<Image>();
            img.color = new Color(1, 1, 1, 0);
            img.DOColor(Color.white, 2f);
            envelopeOpen.SetActive(false);
            admissionFold.SetActive(false);
            envelopeHalf.SetActive(false);
            OnAdmissionLetterSceneEnd();
        }
    }
    void DisplayEnvelope()
    {
        var endpos = envelopeClose.transform.position;
        envelopeClose.SetActive(true);
        envelopeClose.transform.position = envelopeStartPos.transform.position;
        envelopeClose.transform.DOMove(endpos, evenlopeStartMoveDuration).SetEase(envelopeStartMoveEase).OnComplete(OnEnvelopeMoveAnmationEnd);
    }

    public void OnClickEnvelopeClose()
    {
        envelopeClose.SetActive(false);
        envelopeOpen.SetActive(true);
        admissionFold.SetActive(true);
        envelopeHalf.SetActive(true);
    }

    void OnEnvelopeMoveAnmationEnd()
    {
        envelopeCloseButton.SetActive(true);
    }
}