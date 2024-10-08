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
        public CanvasGroup sceneCg_Rescue;

        IEnumerator StartScene_Rescue(float delay)
        {
            yield return new WaitForSeconds(delay + 0.8f);
        }

        public void OnPuzzleEnd_Rescue()
        {

        }

        public void StartPuzzle_Rescue()
        {
            ToggleCg(sceneCg_Rescue, false);
        }
    }
}