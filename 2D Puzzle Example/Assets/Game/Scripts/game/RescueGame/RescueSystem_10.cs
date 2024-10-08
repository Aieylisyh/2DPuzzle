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

        public Image finalOfRescue_rebirthImage;
        public Image finalOfRescue_bgImage;

        IEnumerator StartScene_FinalOfRescue(float delay)
        {
            finalOfRescue_rebirthImage.color = new Color(1, 1, 1, 0);
            finalOfRescue_bgImage.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(delay);
            finalOfRescue_bgImage.DOFade(1, 3);
            yield return new WaitForSeconds(4);
            finalOfRescue_bgImage.DOFade(0, 2);
            yield return new WaitForSeconds(1.5f);
            finalOfRescue_rebirthImage.DOFade(1, 3);
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