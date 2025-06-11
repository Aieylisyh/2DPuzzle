using Assets.Game.Scripts.game.Omni.InGameUi;
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

        public CanvasGroup cg_office_2d_computer_screen;

        bool _isScreenOn;
        public GameObject blackScreen;

        public PasswordBehaviour pb;

        void StartGameSetup()
        {
            _isScreenOn = false;
            blackScreen.SetActive(true);
        }

        public void OnScreenPowerButtonClicked()
        {
            if (_isScreenOn)
            {
                _isScreenOn = false;
                blackScreen.SetActive(true);
                pb.TurnOffPasswordScreen();
            }
            else
            {
                _isScreenOn = false;
                blackScreen.SetActive(false);
                pb.Reboot();
            }
        }

        public void OnLoginSuc()
        {
            pb.TurnOffPasswordScreen();
        }
    }
}