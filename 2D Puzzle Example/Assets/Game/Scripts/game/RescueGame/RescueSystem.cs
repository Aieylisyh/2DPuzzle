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

        public Image girlInbedImg_closeEye;
        public Image girlInbedImg_openEye;

        public AudioSource mobileVibSound;

        public enum GameStage
        {
            None = 0,
            SceneGirlInBed_beforeMobileInteraction = 1,
            SceneGirlInBed_PendingMobileInteraction = 2,
            SceneGirlInBed_AfterMobileInteraction = 3,
        }

        IEnumerator DelayAction(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}