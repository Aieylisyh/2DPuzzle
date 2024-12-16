using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MimicPanelObject : MonoBehaviour
{
    public float delay;
    public float duration;
    public bool useCrtLocalPos;
    public Vector3 startPos;//local
    public Vector3 startEular;//local
    public Vector3 endPos;//local
    public Vector3 endEular;//local
    public DG.Tweening.Ease ease;
    public bool ignoreReset;

    public UnityEvent tweenStartAction;
    public UnityEvent tweenEndAction;

    public void Init()
    {
        //Debug.Log(gameObject.name);
        var mr = GetComponent<MeshRenderer>();
        if (mr != null) mr.enabled = false;

        if (useCrtLocalPos)
        {
            startPos = transform.localPosition;
            endPos = transform.localPosition;
        }

        if (ignoreReset)
            return;

        transform.DOKill();
        transform.localPosition = startPos;
        transform.localEulerAngles = startEular;
    }

    public void StartAnim()
    {
        StartCoroutine(Co());
    }

    void EndCb()
    {
        tweenEndAction?.Invoke();
    }


    IEnumerator Co()
    {
        yield return new WaitForSeconds(delay);
        tweenStartAction?.Invoke();
        transform.DOKill();
        transform.DOLocalMove(endPos, duration).SetEase(ease);
        transform.DOLocalRotate(endEular, duration).SetEase(ease).OnComplete(EndCb);
    }
}