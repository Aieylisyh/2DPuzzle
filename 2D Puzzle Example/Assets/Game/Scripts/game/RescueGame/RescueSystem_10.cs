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
        public CanvasGroup sceneCg_FinalOfRescue;

        IEnumerator StartScene_FinalOfRescue(float delay)
        {
            yield return new WaitForSeconds(delay + 0.8f);
        }

        public void OnPuzzleEnd_FinalOfRescue()
        {

        }

        public void EndGame()
        {
            ToggleCg(sceneCg_FinalOfRescue, false);
        }
    }
}