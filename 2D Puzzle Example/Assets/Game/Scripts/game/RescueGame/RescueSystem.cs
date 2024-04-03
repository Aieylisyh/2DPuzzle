using com;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.AudioSettings;

namespace Rescue
{

    public class RescueSystem : MonoBehaviour
    {
        public static RescueSystem instance;

        void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                //Debug.Log("debug fast OnClickExitJisawScene");

            }
        }

        public void OnClickMobile()
        {
            if (stage != GameStage.SceneGirlInBed_PendingMobileInteraction)
                return;
            SoundSystem.instance.Play("click");
            ToggleOffMobileAlarm();
        }

        private void Start()
        {
            StartSceneGirlInBed();
            zoomParentOrginalPos = zoomParent.position;
        }

        public RectTransform mobile;
        public RectTransform zoomParent;

        public GameStage stage;
        public float mobileVibStrength = 1;
        public int mobileVibVibrato = 10;
        public float zoomParentZoomInDuration = 2;
        public RectTransform zoomParentZoomInRectTrans;
        private Vector3 zoomParentOrginalPos;

        public AudioSource mobileVibSound;

        public enum GameStage
        {
            None = 0,
            SceneGirlInBed_beforeMobileInteraction = 1,
            SceneGirlInBed_PendingMobileInteraction = 2,
            SceneGirlInBed_AfterMobileInteraction = 3,
        }


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
                    //
                }
                );
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

        IEnumerator DelayAction(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
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
    }
}