using Assets.Game.Scripts.game.Omni.InGameUi;
using Assets.Game.Scripts.game.Omni.Mission;
using com;
using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Omni
{

    public partial class Omni2DSystem : MonoBehaviour
    {
        bool _isScreenOn;
        bool _hasLoggedIn;
        public CanvasGroup cg_office_2d_main;
        public CanvasGroup cg_computer_welcome;
        public CanvasGroup cg_computer_bg;
        public CanvasGroup cg_computer_mainUI;
        public RectTransform screenRect;
        public RectTransform screenRect_fullScreenRef;
        public PasswordBehaviour pb;

        public Sticker sticker;
        public Image bootBtnImg;
        public Sprite bootBtnOn;
        public Sprite bootBtnOff;
        public TextMeshProUGUI missionDoneNumTxt;
        public int missionDoneNum = 1342;

        public void RefreshMissionDoneNum(int delta)
        {
            missionDoneNum += delta;
            missionDoneNumTxt.text = missionDoneNum.ToString();
        }

        void StartGameSetup()
        {
            _isScreenOn = false;
            bootBtnImg.sprite = bootBtnOff;
        }

        public void OnScreenPowerButtonClicked()
        {
            if (!_isScreenOn)
            {
                _isScreenOn = true;
                bootBtnImg.sprite = bootBtnOn;

                StartCoroutine(ZoomToFullScreen());
            }
        }

        /// <summary>
        /// F2 调试：跳过开机动画与密码界面，等效于登录成功。
        /// </summary>
        public void SkipBootAndLogin()
        {
            if (_hasLoggedIn)
                return;

            StopAllCoroutines();

            if (screenRect != null)
                screenRect.DOKill();
            if (cg_computer_welcome != null)
                cg_computer_welcome.DOKill();
            if (cg_computer_bg != null)
                cg_computer_bg.DOKill();

            _isScreenOn = true;
            if (bootBtnImg != null && bootBtnOn != null)
                bootBtnImg.sprite = bootBtnOn;

            if (screenRect != null && screenRect_fullScreenRef != null)
            {
                screenRect.localScale = screenRect_fullScreenRef.localScale;
                screenRect.anchoredPosition = screenRect_fullScreenRef.anchoredPosition;
            }

            if (cg_computer_bg != null)
                cg_computer_bg.alpha = 1;

            ToggleCg(cg_computer_welcome, false);
            ToggleCg(cg_computer_mainUI, false);

            if (pb != null)
                pb.TurnOffPasswordScreen();

            if (sticker != null)
                sticker.canRemove = true;

            OnLoginSuc();
        }

        IEnumerator ZoomToFullScreen()
        {
            yield return new WaitForSeconds(0.4f);
            screenRect.DOScale(screenRect_fullScreenRef.localScale.x, 2);
            screenRect.DOAnchorPos(screenRect_fullScreenRef.anchoredPosition, 2);
            yield return new WaitForSeconds(2f);
            cg_computer_welcome.DOKill();
            cg_computer_welcome.DOFade(1, 2).OnComplete(
                () =>
                {
                    cg_computer_bg.alpha = 1;
                    cg_computer_welcome.DOFade(0, 1.5f).SetDelay(1f);
                }
                );
            yield return new WaitForSeconds(5.0f);

            pb.Boot();
        }
    }
}