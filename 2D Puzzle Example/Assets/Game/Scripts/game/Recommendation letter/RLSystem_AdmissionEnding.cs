using com;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class RLSystem : MonoBehaviour
{
    public TextMeshProUGUI intertitle;
    public void InitAdmissionEndingScene()
    {
        UiImageScreenFader.instance.FadeInWhite(ShowTimePassedByIntertitle, 0);
    }

    void ShowTimePassedByIntertitle()
    {
        intertitle.text = "Three Months Later...";
        intertitle.maxVisibleCharacters = 0;
        StartCoroutine(AddIntertitleMaxVisibleCharacters(
            () =>
            {
                UiImageScreenFader.instance.FadeOutWhite(ShowAdmissionSceneFinal, 1);
            }
            ));
    }

    IEnumerator AddIntertitleMaxVisibleCharacters(Action cb)
    {
        yield return new WaitForSeconds(1);
        while (intertitle.maxVisibleCharacters < intertitle.text.Length + 10)
        {
            yield return new WaitForSeconds(0.1f);
            intertitle.maxVisibleCharacters += 1;
        }
        yield return new WaitForSeconds(1);
        intertitle.text = "";

        envelopeClose.SetActive(false);
        envelopeOpen.SetActive(false);
        admissionFold.SetActive(false);
        admissionFold.GetComponent<RectTransform>().anchoredPosition = _admissionFoldStartPos;
        envelopeHalf.SetActive(false);
        admission.SetActive(false);
        envelopeCloseButton.SetActive(false);
        ToggleContinueButton(false);
        ScreenEffectToggle.instance.ToggleDreamReality(false);

        cb?.Invoke();
    }

    void ShowAdmissionSceneFinal()
    {
        DisplayEnvelope();
        ScreenEffectToggle.instance.ToggleDrunk(true);
    }

    public void InitRoofScene()
    {
        ScreenEffectToggle.instance.ToggleDreamReality(false);
        ScreenEffectToggle.instance.ToggleBlurry(true);
        UiImageScreenFader.instance.FadeInBlack(
            () =>
            {
                SoundSystem.instance.Play("alarm");
                StartCoroutine(DelayAction(3.5f, () =>
                {
                    UiImageScreenFader.instance.FadeOutBlack(null);

                    eb.ToggleShow(true);
                    eb.ToggleEyeBlink(true);
                }));
            }
            , 0);
    }

    void ShowFinalWords()
    {
        intertitle.text = "This story is adapted from the author's real-life experiences.";
        intertitle.maxVisibleCharacters = 0;
        StartCoroutine(AddIntertitleMaxVisibleCharacters_FinalWords());
    }

    IEnumerator AddIntertitleMaxVisibleCharacters_FinalWords()
    {
        yield return new WaitForSeconds(1);
        while (intertitle.maxVisibleCharacters < intertitle.text.Length + 20)
        {
            yield return new WaitForSeconds(0.05f);
            intertitle.maxVisibleCharacters += 1;
        }
        yield return new WaitForSeconds(1);
    }
}