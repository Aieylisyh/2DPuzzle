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
                    sticker.canRemove = true;
                }
                );
            yield return new WaitForSeconds(5.0f);

            pb.Boot();
        }
    }
}