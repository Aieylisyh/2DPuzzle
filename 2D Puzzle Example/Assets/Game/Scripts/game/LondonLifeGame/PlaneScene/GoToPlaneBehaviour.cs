﻿using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GoToPlaneBehaviour : MonoBehaviour
{
    [SerializeField] RectTransform me;
    [SerializeField] RectTransform me_run;
    [SerializeField] float jumpHeightStepRatio = 2f;
    [SerializeField] float stepDuration = 0.6f;
    [SerializeField] float startAnimationDuration = 2.5f;

    [SerializeField] RectTransform meFrom;
    [SerializeField] RectTransform meTo;
    [SerializeField] RectTransform meEnd;
    [SerializeField] StepInfo[] steps;
    int _step;
    public UnityEvent evtFinishStartAnimation;


    [System.Serializable]
    public class StepInfo
    {
        public float yOffset = 0;
    }

    private void Awake()
    {
        me.gameObject.SetActive(false);
        me_run.gameObject.SetActive(false);
    }
    public void ShowStartAnimation()
    {
        me_run.gameObject.SetActive(true);
        me_run.localScale = meFrom.localScale;
        me_run.anchoredPosition = meFrom.anchoredPosition;
        me_run.DOAnchorPos(meTo.anchoredPosition, startAnimationDuration);
        me_run.DOScale(meTo.localScale, startAnimationDuration).OnComplete(
            () =>
            {
                evtFinishStartAnimation?.Invoke();
                me.gameObject.SetActive(true);
                me_run.gameObject.SetActive(false);
                SyncStep();
            }
            );
    }

    void SyncStep()
    {
        if (_step > steps.Length)
            return;
        if (_step < 1)
            return;
        me.DOKill();

        var info = steps[_step - 1];
        var r = _step / (float)steps.Length;
        var goodScale = Mathf.Lerp(meTo.localScale.x, meEnd.localScale.x, r);
        var goodAnchorPos = meTo.anchoredPosition;
        goodAnchorPos.y = info.yOffset + Mathf.Lerp(meTo.anchoredPosition.y, meEnd.anchoredPosition.y, r);
        me.DOAnchorPos(goodAnchorPos, stepDuration).SetEase(Ease.OutBack);
        me.DOScale(goodScale, stepDuration);
    }

    void AddStep(int s)
    {
        if (_step >= steps.Length)
        {
            return;
        }

        _step += s;
        if (_step >= steps.Length)
        {
            _step = steps.Length;
            PlaneSceneGameSystem.instance.OnGoToPlaneEnd();
            SyncStep();
            return;
        }

        SyncStep();
    }



    public void OnClickBubble()
    {
        AddStep(1);
    }
}