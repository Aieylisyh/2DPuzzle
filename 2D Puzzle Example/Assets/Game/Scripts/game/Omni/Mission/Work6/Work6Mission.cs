using Assets.Game.Scripts.game.Omni.Mission;
using com;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace Assets.Game.Scripts.game.Omni.Mission.Work6
{
    public class Work6Mission : MissionBehaviour
    {
        public Work6Popup popup;
        public GameObject talk1;
        public GameObject talk2;
        public GameObject talk3;
        public GameObject talk4;
        public GameObject talk5;
        public GameObject talk6;
        public GameObject videoButton;
        public VideoPlayer vp;

        public GameObject missionObj;
        public Work6Email email;
        public GameObject header;
        public MissionPrototype work6proto;

        protected override void Awake()
        {
            if (missionObj != null)
                view = missionObj;

            base.Awake();

            if (vp != null)
                vp.loopPointReached += OnVideoFinished;
        }

        void Start()
        {
            Hide();
        }

        void OnVideoFinished(VideoPlayer source)
        {
            if (vp != null)
                vp.gameObject.SetActive(false);
            if (SoundSystem.instance != null)
                SoundSystem.instance.Play("click");
        }

        public void StartWork6ByPlot()
        {
            if (work6proto == null || MissionSystem.instance == null || MissionListPanel.instance == null)
                return;

            var newMission = new MissionData();
            newMission.proto = work6proto;
            newMission.state = MissionData.State.Unread;
            newMission.missionPackageId = "work6";
            MissionSystem.instance.missions.Add(newMission);
            MissionListPanel.instance.AddMission(newMission);

            if (SoundSystem.instance != null)
                SoundSystem.instance.Play("strange");
        }

        public override void Hide()
        {
            if (popup != null)
                popup.SetPopupState(Work6Popup.PopupState.None);

            SetActiveIfNotNull(talk1, false);
            SetActiveIfNotNull(talk2, false);
            SetActiveIfNotNull(talk3, false);
            SetActiveIfNotNull(talk4, false);
            SetActiveIfNotNull(talk5, false);
            SetActiveIfNotNull(talk6, false);
            SetActiveIfNotNull(videoButton, true);

            if (vp != null)
            {
                vp.Stop();
                var clearRt = vp.GetComponent<ClearRenderTexture>();
                if (clearRt != null)
                    clearRt.ApplyIdleColor();
                else
                    ClearRenderTexture.FillRenderTexture(vp.targetTexture, new Color(0.12f, 0.12f, 0.14f, 1f));
            }

            SetActiveIfNotNull(missionObj, false);
            SetActiveIfNotNull(header, false);
        }

        public override void ResetMission()
        {
            Hide();
            SetActiveIfNotNull(missionObj, true);
            SetActiveIfNotNull(talk1, true);

            if (email != null)
                email.Hide();

            SetActiveIfNotNull(header, true);
        }

        public void OnClickVideoButton()
        {
            SetActiveIfNotNull(videoButton, false);

            if (vp != null)
                vp.Play();

            SetActiveIfNotNull(talk1, false);
            SetActiveIfNotNull(talk2, true);
            StartCoroutine(OnVideoPlayedIE());
        }

        public void OnClickNo()
        {
            if (popup != null)
                popup.SetPopupState(Work6Popup.PopupState.None);

            SetActiveIfNotNull(talk5, false);
            SetActiveIfNotNull(talk6, true);
            SetActiveIfNotNull(header, false);

            if (email != null)
            {
                SetActiveIfNotNull(email.header, true);
                email.OnStartEmailSequence();
            }
        }

        IEnumerator OnVideoPlayedIE()
        {
            yield return new WaitForSeconds(0.1f);
            yield return new WaitForSeconds(3f);
            SetActiveIfNotNull(talk2, false);
            SetActiveIfNotNull(talk3, true);
            yield return new WaitForSeconds(7.0f);
            SetActiveIfNotNull(talk3, false);
            SetActiveIfNotNull(talk4, true);
            yield return new WaitForSeconds(5.4f);
            SetActiveIfNotNull(talk4, false);
            SetActiveIfNotNull(talk5, true);
            yield return new WaitForSeconds(3.4f);

            if (popup != null)
                popup.SetPopupState(Work6Popup.PopupState.Show);
        }
        //接到任务看视频前-台词1
        //视频0秒（开始看）-台词2
        //视频3秒-台词3
        //视频11秒-台词4
        //视频结束-台词5
        //选完no弹窗消失后-台词6
    }
}
