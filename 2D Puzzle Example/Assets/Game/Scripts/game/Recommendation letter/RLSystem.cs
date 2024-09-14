using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public partial class RLSystem : MonoBehaviour
{
    public static RLSystem instance;
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


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //admission letter scene
        ToggleOff_AdmissionLetterScene();

        DisplayEnvelope();

        //roof scene
        nextBtn_RoofScene.gameObject.SetActive(false);

    }

    void ToggleOff_AdmissionLetterScene()
    {
        envelopeClose.SetActive(false);
        envelopeOpen.SetActive(false);
        admissionFold.SetActive(false);
        envelopeHalf.SetActive(false);
        admission.SetActive(false);
        envelopeCloseButton.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

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
