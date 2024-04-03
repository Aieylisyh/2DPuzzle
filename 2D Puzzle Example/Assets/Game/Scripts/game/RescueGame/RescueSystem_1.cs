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

        void StartSceneGirlInBed()
        {
            //mobile vibrate
            //mobile zoom
            //player -> click mobile
            //stop vibrate, sound ding
            stage = GameStage.SceneGirlInBed_beforeMobileInteraction;
            StartCoroutine(MobileVibrateLoop());
            StartCoroutine(DelayAction(4.5f, ZoomInToShowMobile));
        }

        void ZoomInToShowMobile()
        {
            zoomParent.DOMove(zoomParentZoomInRectTrans.transform.position, zoomParentZoomInDuration).SetEase(Ease.InOutCubic);
            zoomParent.DOScale(2, zoomParentZoomInDuration).SetEase(Ease.InOutCubic).OnComplete(
                () =>
                {
                    stage = GameStage.SceneGirlInBed_PendingMobileInteraction;
                }
                );
        }

        void ZoomOutToShowBed()
        {
            zoomParent.DOMove(zoomParentOrginalPos, zoomParentZoomInDuration).SetEase(Ease.InOutCubic);
            zoomParent.DOScale(1, zoomParentZoomInDuration).SetEase(Ease.InOutCubic).OnComplete(
                () =>
                {
                    SwitchInBedImage();
                }
                );
        }

        void SwitchInBedImage()
        {
            girlInbedImg_closeEye.enabled = false;
            girlInbedImg_openEye.enabled = true;
        }

        void MobileVibrate()
        {
            mobileVibSound.Play();
            mobile.DOShakePosition(2.5f, mobileVibStrength, mobileVibVibrato, fadeOut: false);
        }

        void ToggleOffMobileAlarm()
        {
            mobileVibSound.Stop();
            mobile.DOKill();
            stage = GameStage.SceneGirlInBed_AfterMobileInteraction;
            ZoomOutToShowBed();
        }

        IEnumerator MobileVibrateLoop()
        {
            yield return new WaitForSeconds(2);

            while (stage == GameStage.SceneGirlInBed_PendingMobileInteraction
                ||
                stage == GameStage.SceneGirlInBed_beforeMobileInteraction)
            {
                MobileVibrate();
                yield return new WaitForSeconds(4);
            }
        }


        public void OnClickMobile()
        {
            if (stage != GameStage.SceneGirlInBed_PendingMobileInteraction)
                return;

            SoundSystem.instance.Play("switchoff");
            ToggleOffMobileAlarm();
        }

    }
}