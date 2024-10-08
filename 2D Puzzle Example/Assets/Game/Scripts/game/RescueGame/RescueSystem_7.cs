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
            yield return new WaitForSeconds(delay + 0.8f);
        }

        public void OnPuzzleEnd_ClockNarrative()
        {

        }

        public void StartPuzzle_ClockNarrative()
        {
            ToggleCg(sceneCg_ClockNarrative, false);
        }
    }
}