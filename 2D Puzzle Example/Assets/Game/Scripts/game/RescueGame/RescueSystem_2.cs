using com;
using DG.Tweening;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Rescue
{
    public partial class RescueSystem : MonoBehaviour
    {
        public Transform dotParent;

        public float dotInterval = 0.1f;

        public AudioSource[] sfxsSwitching;


        void SwitchToWashScene()
        {
            foreach (var s in sfxsSwitching)
            {
                s.Play();
                var v = s.volume;
                s.volume = 0;
                s.DOFade(v, 2);
            }

            var childCount = dotParent.childCount;
            for (var i = 0; i < childCount; i++)
            {
                var delay = i * dotInterval + 0.3f;
                var c = dotParent.GetChild(i);
                StartCoroutine(DelayAction(delay, () =>
                    {
                        c.gameObject.SetActive(true);
                    }));
            }

            var waitedTime = childCount * dotInterval;
            StartCoroutine(DelayAction(waitedTime, () =>
               {
                   foreach (var s in sfxsSwitching)
                   {
                       s.DOFade(0, 1);
                   }
                   ToggleCg(sceneCg_girlInBed1, false);
                   sceneCg_washFace.alpha = 0;
                   sceneCg_washFace.interactable = true;
                   sceneCg_washFace.blocksRaycasts = true;
                   sceneCg_washFace.DOKill();
                   sceneCg_washFace.DOFade(1, 2);
               }));
        }

        public void SwitchToDressingScene()
        {
            Debug.Log("SwitchToDressingScene");
            sceneCg_washFace.gameObject.SetActive(false);
            ToggleCg(sceneCg_dressing, true);
        }

        public void FocusViewOnPickedCloth(Vector2 localAnchoredPos)
        {
            var duration = 3.5f;
            var rect = sceneCg_dressing.GetComponent<RectTransform>();
            rect.DOScale(1.4f, duration);
            rect.DOAnchorPos(-localAnchoredPos, duration);
            StartCoroutine(StartScene_OutsidePuzzle(duration));
        }
    }
}