using Assets.Game.Scripts.game.Omni.Mission;
using Assets.Linyifei_Game.Script.Onmi_sys;
using com;
using DG.Tweening;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Omni
{

    public partial class Omni2DSystem : MonoBehaviour
    {
        public BgmController bgmController1;
        public BgmController bgmController2;

        public void SwitchBgm()
        {
            bgmController1.gameObject.SetActive(false);
            bgmController2.gameObject.SetActive(true);
        }

        public void StopBgm()
        {
            bgmController1.gameObject.SetActive(false);
            bgmController2.gameObject.SetActive(false);
        }
    }
}