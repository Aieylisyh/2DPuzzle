using Assets.Game.Scripts.game.Omni.Mission;
using com;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.game.Omni.Mission.VideoRecord
{
    public class MapMission2System : MapMissionBehaviour
    {
        [System.Serializable]
        public class MapData
        {
            public float startTime;
            public float endTime;
            public GameObject hint;
        }

        public MapData[] mapDatas;

        public Image selfTalk;
        public Sprite talkSp_start;
        public Sprite talkSp_popupEnd;
        public Sprite talkSp_videoStart;
        public Sprite talkSp_suc;
        public GameObject transparentButton;
        public GameObject popupPanel;

        protected override void BindSerializedMapDatas()
        {
            if (mapDatas == null)
            {
                runtimeMapDatas = null;
                return;
            }

            runtimeMapDatas = new MapDataRuntime[mapDatas.Length];
            for (int i = 0; i < mapDatas.Length; i++)
            {
                var d = mapDatas[i];
                runtimeMapDatas[i] = new MapDataRuntime
                {
                    startTime = d.startTime,
                    endTime = d.endTime,
                    hint = d.hint
                };
            }
        }

        protected override void Awake()
        {
            base.Awake();

            if (selfTalk != null)
                selfTalk.enabled = false;
        }

        protected override void OnResetMission()
        {
            base.OnResetMission();

            if (currentMissionDone)
                return;

            if (selfTalk != null)
            {
                selfTalk.enabled = true;
                selfTalk.sprite = talkSp_start;
            }

            SetActiveIfNotNull(transparentButton, true);
            SetActiveIfNotNull(popupPanel, false);
        }

        protected override void ApplyCompletedState()
        {
            base.ApplyCompletedState();

            if (selfTalk != null)
            {
                selfTalk.enabled = true;
                selfTalk.sprite = talkSp_suc;
            }
        }

        public void OnClickISeePopup()
        {
            SetActiveIfNotNull(transparentButton, false);
            SetActiveIfNotNull(popupPanel, false);

            if (selfTalk != null)
            {
                selfTalk.enabled = true;
                selfTalk.sprite = talkSp_popupEnd;
            }
        }

        public void OnClickTransparentButton()
        {
            SetActiveIfNotNull(transparentButton, false);
            SetActiveIfNotNull(popupPanel, true);

            if (SoundSystem.instance != null)
                SoundSystem.instance.Play("warning");
        }

        public void DebugCompleteMission()
        {
            ToggleFinishedButton(false);
            SubmitMission();
        }

        public override void OnClickVideoPlayButton()
        {
            base.OnClickVideoPlayButton();

            if (selfTalk != null)
            {
                selfTalk.enabled = true;
                selfTalk.sprite = talkSp_videoStart;
            }
        }

        protected override void OnSubmitSuccess()
        {
            if (selfTalk != null)
            {
                selfTalk.enabled = true;
                selfTalk.sprite = talkSp_suc;
            }

            base.OnSubmitSuccess();
            StartCoroutine(StartWork6Delayed());
        }

        IEnumerator StartWork6Delayed()
        {
            yield return new WaitForSeconds(3f);
            if (MissionSystem.instance != null && MissionSystem.instance.work6Mission != null)
                MissionSystem.instance.work6Mission.StartWork6ByPlot();
        }
    }
}
