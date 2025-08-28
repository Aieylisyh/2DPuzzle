using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Assets.Game.Scripts.game.Omni.Mission.VideoRecord
{
    public class VideoRecordMissionSystem : MonoBehaviour
    {
        public GameObject view;
        public Image selfTalk;
        bool currentMissionDone;
        public Sprite talkSp_start;
        public Sprite talkSp_suc;
        public Sprite talkSp_fail;

        [System.Serializable]
        public class QuestionAndAnswers
        {
            public GameObject[] checkmarks;
            public GameObject currentCheckmark;
            public Sprite talkSp;

            public void Check(GameObject checkmarkClicked)
            {
                Reset();
                checkmarkClicked.SetActive(true);
            }

            public bool IsCorrect()
            {
                bool result = true;
                int checkedCount = 0;
                foreach (var cm in checkmarks)
                {
                    if (cm.activeSelf)
                    {
                        checkedCount++;
                        if (cm != currentCheckmark)
                        {
                            result = false;
                        }
                    }
                }

                if (checkedCount != 1)
                {
                    result = false;
                }

                return result;
            }

            public bool IsChecked()
            {
                int checkedCount = 0;
                foreach (var cm in checkmarks)
                {
                    if (cm.activeSelf)
                    {
                        checkedCount++;
                    }
                }
                return checkedCount > 0;
            }

            public void Reset()
            {
                foreach (var cm in checkmarks)
                {
                    cm.SetActive(false);
                }
            }
        }

        public QuestionAndAnswers[] questions;

        private void Awake()
        {
            view.SetActive(false);
            ResetAllQuestions();
            vp.Stop();
            vp.loopPointReached += OnVideoFinished;
            selfTalk.enabled = false;
        }

        public void 点击选项(GameObject 选项对应的checkmark)
        {
            foreach (var q in questions)
            {
                if (q.checkmarks.Contains(选项对应的checkmark))
                {
                    q.Check(选项对应的checkmark);

                    if (q.IsCorrect() && q.talkSp != null)
                    {
                        selfTalk.sprite = q.talkSp;
                        selfTalk.enabled = true;
                    }
                    else
                    {
                        selfTalk.enabled = false;
                    }
                    break;
                }
            }

            RefreshFinishBtn();
        }

        private void OnVideoFinished(VideoPlayer source)
        {
            videoPlayButton.SetActive(true);
            // throw new NotImplementedException();
        }

        void ResetAllQuestions()
        {
            foreach (var q in questions)
                q.Reset();
        }
        /// <summary>
        /// 首次开始这个任务
        /// </summary>
        public void ResetMission()
        {
            view.SetActive(true);
            ResetAllQuestions();

            foreach (var g in submitSucToShows)
                g.SetActive(false);
            foreach (var g in submitFailToShows)
                g.SetActive(false);

            ToggleFinishedButton(false);

            if (currentMissionDone)
            {
                foreach (var g in submitSucToShows)
                    g.SetActive(true);
                foreach (var g in submitSucToHides)
                    g.SetActive(false);
            }

            vp.Stop();
            videoPlayButton.SetActive(true);
        }


        public void Hide()
        {
            view.SetActive(false);
        }

        /// <summary>
        /// 失败了重新开始
        /// </summary>
        public void RetryMission()
        {
            //ResetAllQuestions();
            foreach (var g in submitFailToShows)
                g.SetActive(false);
        }

        public GameObject[] submitSucToShows;
        public GameObject[] submitSucToHides;
        public GameObject[] submitFailToShows;
        public GameObject[] submitFailToHides;
        /// <summary>
        /// 点击finish触发（至少勾选了3个餐厅出现可以点击的finish）
        /// </summary>
        public void SubmitMission()
        {
            bool result = true;
            foreach (var q in questions)
            {
                if (!q.IsCorrect())
                    result = false;
            }

            Debug.Log("SubmitMission" + result);
            if (MissionSystem.instance.cheatMode_alwaysCorrect)
                result = true;


            if (result)
            {
                selfTalk.enabled = true;
                selfTalk.sprite = talkSp_suc;

                currentMissionDone = true;
                foreach (var g in submitSucToShows)
                    g.SetActive(true);
                foreach (var g in submitSucToHides)
                    g.SetActive(false);
                return;
            }

            selfTalk.enabled = true;
            selfTalk.sprite = talkSp_fail;
            foreach (var g in submitFailToShows)
                g.SetActive(true);
            foreach (var g in submitFailToHides)
                g.SetActive(false);
        }

        public GameObject finishBtn_ok;
        public GameObject finishBtn_notOk;

        void RefreshFinishBtn()
        {
            bool allChecked = true;
            foreach (var q in questions)
            {
                if (!q.IsChecked())
                    allChecked = false;
            }
            ToggleFinishedButton(allChecked);
        }

        void ToggleFinishedButton(bool ok)
        {
            finishBtn_ok.SetActive(ok);
            finishBtn_notOk.SetActive(!ok);
        }

        public void OnClickVideoPlayButton()
        {
            vp.Play();
            videoPlayButton.SetActive(false);
            selfTalk.enabled = true;
            selfTalk.sprite = talkSp_start;
        }

        public GameObject videoPlayButton;
        public VideoPlayer vp;
    }
}