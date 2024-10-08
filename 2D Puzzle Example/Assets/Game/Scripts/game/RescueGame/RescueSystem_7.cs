using com;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Rescue
{
    public partial class RescueSystem : MonoBehaviour
    {
        public CanvasGroup sceneCg_ClockNarrative;

        IEnumerator StartScene_ClockNarrative(float delay)
        {
            var cg = boyClock.GetComponent<CanvasGroup>();
            cg.blocksRaycasts = true;
            cg.interactable = true;
            cg.alpha = 0;
            foreach (var bg in boyClockBeforeActiveGroup)
                bg.SetActive(true);
            foreach (var bg in boyClockAfterActiveGroup)
                bg.SetActive(false);

            yield return new WaitForSeconds(delay);
            cg.DOFade(1, 1);
        }

        public void OnPuzzleEnd_ClockNarrative()
        {

        }

        public void StartPuzzle_ClockNarrative()
        {
            ToggleCg(sceneCg_ClockNarrative, false);
        }

        [SerializeField] ClockBehaviour boyClock;
        [SerializeField] ClockBehaviour girlClock;
        [SerializeField] GameObject[] boyClockBeforeActiveGroup;
        [SerializeField] GameObject[] boyClockAfterActiveGroup;
        [SerializeField] RectTransform boyPoolDragTarget;
        [SerializeField] float boyPoolDragDistanceThreshold;

        public void ReachBoyClockEnd()
        {
            boyClock.enabled = false;
            var cg = boyClock.GetComponent<CanvasGroup>();
            cg.blocksRaycasts = false;
            cg.interactable = false;
            cg.DOFade(0, 2).OnComplete(ShowDragBoyToPool);
        }

        void ShowDragBoyToPool()
        {
            foreach (var bg in boyClockBeforeActiveGroup)
                bg.SetActive(false);
            foreach (var bg in boyClockAfterActiveGroup)
                bg.SetActive(true);
        }

        public void ReachGirlClockEnd()
        {
            girlClock.enabled = false;
            var cg = girlClock.GetComponent<CanvasGroup>();
            cg.blocksRaycasts = false;
            cg.interactable = false;
            cg.DOFade(0, 2);
        }
    }
}