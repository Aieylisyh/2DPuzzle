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
        public CanvasGroup lastSceneCg;
        public CanvasGroup currentSceneCg;

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

                   lastSceneCg.alpha = 0;
                   lastSceneCg.interactable = false;
                   lastSceneCg.blocksRaycasts = false;
                   currentSceneCg.alpha = 0;
                   currentSceneCg.interactable = false;
                   currentSceneCg.blocksRaycasts = false;
                   currentSceneCg.DOKill();
                   currentSceneCg.DOFade(1, 2);
               }));
        }

    }

}