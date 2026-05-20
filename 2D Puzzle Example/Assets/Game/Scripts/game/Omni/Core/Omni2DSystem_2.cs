using Assets.Game.Scripts.game.Omni.Mission;
using com;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Omni
{

    public partial class Omni2DSystem : MonoBehaviour
    {
        public void OnLoginSuc()
        {
            pb.TurnOffPasswordScreen();

            ToggleCg(cg_computer_welcome, true);
            ToggleCg(cg_computer_mainUI, false);
            StartCoroutine(LoginSucIE());
        }

        IEnumerator LoginSucIE()
        {
            yield return new WaitForSeconds(1.8f);
            ToggleCg(cg_computer_welcome, false);
            ToggleCg(cg_computer_mainUI, true);
            RefreshMissionDoneNum(0);
            yield return new WaitForSeconds(2f);

            MissionSystem.instance.Add(0);
        }
    }
}