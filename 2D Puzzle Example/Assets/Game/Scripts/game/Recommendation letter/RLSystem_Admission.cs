using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class RLSystem : MonoBehaviour
{

    public GameObject envelopeClose;
    public GameObject envelopeOpen;
    public GameObject admissionFold;
    public GameObject envelopeHalf;
    public GameObject admission;
    public GameObject envelopeStartPos;

    public float evenlopeStartMoveDuration;
    public Ease envelopeStartMoveEase;
    public GameObject envelopeCloseButton;
    public float envelopeDragDistanceThreshold;

    public void CheckDagEndPlace(float distance)
    {
        if (envelopeDragDistanceThreshold > distance)
        {
            admission.SetActive(true);
            var img = admission.GetComponent<Image>();
            img.color = new Color(1, 1, 1, 0);
            img.DOColor(Color.white, 2.0f).SetEase(Ease.InCubic);
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
