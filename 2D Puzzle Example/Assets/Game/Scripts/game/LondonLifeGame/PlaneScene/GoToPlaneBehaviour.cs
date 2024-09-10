﻿using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Game.Scripts.game.LondonLifeGame.PlaneScene
{
    public class GoToPlaneBehaviour : MonoBehaviour
    {
        [SerializeField] RectTransform me;
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
        }

        void Start()
        {
            ShowStartAnimation();
        }

        void ShowStartAnimation()
        {
            me.gameObject.SetActive(true);
            me.localScale = meFrom.localScale;
            me.anchoredPosition = meFrom.anchoredPosition;
            me.DOAnchorPos(meTo.anchoredPosition, startAnimationDuration);
            me.DOScale(meTo.localScale, startAnimationDuration).OnComplete(
                () =>
                {
                    evtFinishStartAnimation?.Invoke();
                    SyncStep();
                }
                );
        }

        void SyncStep()
        {
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
            _step += s;
            if (_step > steps.Length)
            {
                _step = steps.Length;
            }

            SyncStep();
        }

        public void OnClickBubble()
        {
            AddStep(1);
        }
    }
}