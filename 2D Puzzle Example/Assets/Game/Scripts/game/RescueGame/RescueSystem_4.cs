﻿using com;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Rescue
{
    public partial class RescueSystem : MonoBehaviour
    {
        [SerializeField] Transform mobile_boy;

        IEnumerator StartScene_WakeUpBoy()
        {
            boyInBed_1.SetActive(true);
            boyInBed_2.SetActive(false);
            yield return new WaitForSeconds(0.8f);
            StartCoroutine(MobileVibrateLoop_boy());
            StartCoroutine(DelayAction(4.5f, ZoomInToShowMobile_boy));
        }

        IEnumerator MobileVibrateLoop_boy()
        {
            yield return new WaitForSeconds(2);

            while (stage == GameStage.SceneBoyInBed_PendingMobileInteraction
                ||
                stage == GameStage.SceneBoyInBed_beforeMobileInteraction)
            {
                MobileVibrate();
                yield return new WaitForSeconds(4);
            }
        }

        public void OnClickMobile_boy()
        {
            if (stage != GameStage.SceneBoyInBed_PendingMobileInteraction)
                return;

            ToggleOffMobileAlarm_boy();
        }

        void ZoomInToShowMobile_boy()
        {
            zoomParent.DOMove(zoomParentZoomInRectTrans.transform.position, zoomParentZoomInDuration).SetEase(Ease.InOutCubic);
            zoomParent.DOScale(1.5f, zoomParentZoomInDuration).SetEase(Ease.InOutCubic).OnComplete(
                () =>
                {
                    stage = GameStage.SceneBoyInBed_PendingMobileInteraction;
                }
                );
        }

        void ZoomOutToShowBed_boy()
        {
            zoomParent.DOMove(zoomParentOrginalPos, zoomParentZoomInDuration).SetEase(Ease.InOutCubic);
            zoomParent.DOScale(1, zoomParentZoomInDuration).SetEase(Ease.InOutCubic).OnComplete(
                () =>
                {
                    SwitchInBedImage_boy();
                    StartCoroutine(DelayAction(2.0f, SwitchToWashScene));
                }
                );
        }

        [SerializeField] GameObject boyInBed_1;
        [SerializeField] GameObject boyInBed_2;

        void SwitchInBedImage_boy()
        {
            boyInBed_1.SetActive(false);
            boyInBed_2.SetActive(true);
        }

        void MobileVibrate_boy()
        {
            mobileVibSound.Play();
            mobile_boy.DOShakePosition(2.5f, mobileVibStrength, mobileVibVibrato, fadeOut: false);
        }

        void ToggleOffMobileAlarm_boy()
        {
            SoundSystem.instance.Play("switchoff");
            mobileVibSound.Stop();
            mobile_boy.DOKill();
            stage = GameStage.SceneBoyInBed_AfterMobileInteraction;
            ZoomOutToShowBed_boy();
        }
    }
}