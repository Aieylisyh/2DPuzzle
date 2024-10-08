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
        public CanvasGroup sceneCg_DialogTwoPerson;

        IEnumerator StartScene_DialogTwoPerson(float delay)
        {
            yield return new WaitForSeconds(delay + 0.8f);
        }

        public void OnPuzzleEnd_DialogTwoPerson()
        {

        }

        public void StartPuzzle_FinalOfRescue()
        {
            ToggleCg(sceneCg_DialogTwoPerson, false);
        }
    }
}