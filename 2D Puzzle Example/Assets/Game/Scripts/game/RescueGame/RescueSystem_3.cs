using com;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace Rescue
{
    public partial class RescueSystem : MonoBehaviour
    {
        public CanvasGroup sceneCg_OutsidePuzzle;
        public CanvasGroup endImage_OutsidePuzzle;
        public CanvasGroup puzzleContainer_OutsidePuzzle;
        public CanvasGroup chatFrame_OutsidePuzzle;
        public RectTransform movableBody_OutsidePuzzle;

        public bool puzzleStarting = false;

        IEnumerator StartScene_OutsidePuzzle(float delay)
        {
            puzzleStarting = false;
            yield return new WaitForSeconds(delay + 1f);
            Debug.Log("SwitchToOutsidePuzzleScene");
            ToggleCg(sceneCg_girlInBed1, false);
            ToggleCg(sceneCg_washFace, false);
            ToggleCg(sceneCg_dressing, false);

            sceneCg_OutsidePuzzle.gameObject.SetActive(true);
            sceneCg_OutsidePuzzle.interactable = true;
            sceneCg_OutsidePuzzle.blocksRaycasts = true;
            sceneCg_OutsidePuzzle.alpha = 0;
            endImage_OutsidePuzzle.alpha = 0;
            chatFrame_OutsidePuzzle.alpha = 0;
            puzzleContainer_OutsidePuzzle.alpha = 0;



            float durationFadeIn = 1.5f;
            sceneCg_OutsidePuzzle.DOFade(1, durationFadeIn);

            yield return new WaitForSeconds(durationFadeIn + 0.1f);

            float durationShowPuzzle = 1.0f;
            chatFrame_OutsidePuzzle.DOFade(1, durationShowPuzzle);
            yield return new WaitForSeconds(durationShowPuzzle);
            puzzleStarting = true;
            puzzleContainer_OutsidePuzzle.DOFade(1, 2f).SetEase(Ease.OutCubic);
        }

        public void OnStartDrag_OutsidePuzzle()
        {
            AnimateMovableBody_OutsidePuzzle();
        }

        public void OnEndDrag_OutsidePuzzle()
        {
            AnimateMovableBody_OutsidePuzzle();
        }

        void AnimateMovableBody_OutsidePuzzle()
        {

        }

        void OnPuzzleEnd_OutsidePuzzle()
        {

        }
    }
}