using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        envelopeClose.SetActive(false);
        envelopeOpen.SetActive(false);
        admissionFold.SetActive(false);
        envelopeHalf.SetActive(false);
        admission.SetActive(false);
        envelopeCloseButton.SetActive(false);

        DisplayEnvelope();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckDagEndPlace(float distance)
    {
        if(envelopeDragDistanceThreshold > distance)
        {
            admission.SetActive(true);
            envelopeOpen.SetActive(false);
            admissionFold.SetActive(false);
            envelopeHalf.SetActive(false);
        }
    }
   void DisplayEnvelope ()
    {
        var endpos = envelopeClose.transform.position;
        envelopeClose.SetActive(true);
        envelopeClose.transform.position = envelopeStartPos.transform.position;
        envelopeClose.transform.DOMove(endpos, evenlopeStartMoveDuration).SetEase(envelopeStartMoveEase).OnComplete(OnEnvelopeMoveAnmationEnd);

    }

    public void OnClickEnvelopeClose ()
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
