using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Assets.Game.Scripts.game.Omni.Mission.VideoRecord
{
    public class MapMission2System : MonoBehaviour
    {
        public GameObject view;
        public Image selfTalk;
        bool currentMissionDone;

        public Sprite talkSp_start;
        public Sprite talkSp_popupEnd;
        public Sprite talkSp_videoStart;
        public Sprite talkSp_suc;
        public GameObject redDotButton;
        public GameObject transparentButton;
        public GameObject popupPanel;
        public MapData[] mapDatas;

        [System.Serializable]
        public class MapData
        {
            public float startTime;
            public float endTime;
            public GameObject hint;
        }

        private void Awake()
        {
            view.SetActive(false);
            vp.Stop();
            vp.loopPointReached += OnVideoFinished;
            selfTalk.enabled = false;
            redDotButton.SetActive(false);
        }

        private void OnVideoFinished(VideoPlayer source)
        {
            videoPlayButton.SetActive(true);
            redDotButton.SetActive(false);
        }

        public void OnClickISeePopup()
        {
            transparentButton.SetActive(false);
            popupPanel.SetActive(false);
            selfTalk.enabled = true;
            selfTalk.sprite = talkSp_popupEnd;
        }

        public void OnClickTransparentButton()
        {
            transparentButton.SetActive(false);
            popupPanel.SetActive(true);
        }

        /// <summary>
        /// 首次开始这个任务
        /// </summary>
        public void ResetMission()
        {
            view.SetActive(true);

            videoPlayButton.SetActive(true);
            redDotButton.SetActive(false);

            foreach (var g in submitSucToShows)
                g.SetActive(false);
            foreach (var g in submitFailToShows)
                g.SetActive(false);

            ToggleFinishedButton(false);
            vp.Stop();

            if (currentMissionDone)
            {
                foreach (var g in submitSucToShows)
                    g.SetActive(true);
                foreach (var g in submitSucToHides)
                    g.SetActive(false);

                selfTalk.enabled = true;
                selfTalk.sprite = talkSp_suc;
                videoPlayButton.SetActive(false);
                redDotButton.SetActive(false);
                return;
            }
            selfTalk.enabled = true;
            selfTalk.sprite = talkSp_start;
            transparentButton.SetActive(true);
            popupPanel.SetActive(false);
        }


        public void Hide()
        {
            view.SetActive(false);
        }


        public GameObject[] submitSucToShows;
        public GameObject[] submitSucToHides;
        public GameObject[] submitFailToShows;
        public GameObject[] submitFailToHides;

        public GameObject finishBtn_ok;
        public GameObject finishBtn_notOk;

        void RefreshFinishBtn()
        {
            bool allChecked = true;
            foreach (var d in mapDatas)
            {
                if (!d.hint.activeSelf)
                    allChecked = false;
            }
            ToggleFinishedButton(allChecked);
        }

        public void OnClickMapRedDot()
        {
            var t = vp.time;
            Debug.Log("OnClickMapRedDot t " + t);
            foreach (var d in mapDatas)
            {
                if (d.startTime < t && d.endTime > t)
                    d.hint.SetActive(true);
            }

            RefreshFinishBtn();
        }

        void ToggleFinishedButton(bool ok)
        {
            finishBtn_ok.SetActive(ok);
            finishBtn_notOk.SetActive(!ok);
        }

        public void DebugCompleteMission()
        {
            finishBtn_ok.SetActive(false);
            finishBtn_notOk.SetActive(false);
            SubmitMission();
        }

        public void OnClickVideoPlayButton()
        {
            vp.Play();
            videoPlayButton.SetActive(false);
            redDotButton.SetActive(true);
            selfTalk.enabled = true;
            selfTalk.sprite = talkSp_videoStart;
        }

        public void SubmitMission()
        {
            Debug.Log("SubmitMission");

            selfTalk.enabled = true;
            selfTalk.sprite = talkSp_suc;

            currentMissionDone = true;
            foreach (var g in submitSucToShows)
                g.SetActive(true);
            foreach (var g in submitSucToHides)
                g.SetActive(false);
        }

        public GameObject videoPlayButton;
        public VideoPlayer vp;
    }
}