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
        public RectTransform screenRect;
        public RectTransform screenRect_fullScreenRef;
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
                StartCoroutine(ZoomToFullScreen());
            }
        }

        IEnumerator ZoomToFullScreen()
        {
            yield return new WaitForSeconds(0.7f);
            screenRect.DOScale(screenRect_fullScreenRef.localScale.x, 2);
            screenRect.DOAnchorPos(screenRect_fullScreenRef.anchoredPosition, 2);
        }

        public void OnLoginSuc()
        {
            pb.TurnOffPasswordScreen();
        }
    }
}