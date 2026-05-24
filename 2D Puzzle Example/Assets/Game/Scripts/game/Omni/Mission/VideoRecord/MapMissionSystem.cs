using Assets.Game.Scripts.game.Omni.Mission;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.game.Omni.Mission.VideoRecord
{
    public class MapMissionSystem : MapMissionBehaviour
    {
        [System.Serializable]
        public class MapData
        {
            public float startTime;
            public float endTime;
            public GameObject hint;
        }

        public Image selfTalk;
        public Sprite talkSp_start;
        public Sprite talkSp_suc;

        public MapData[] mapDatas;

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
            MissionTalkHelper.Hide(selfTalk);
        }

        protected override void OnResetMission()
        {
            base.OnResetMission();

            if (currentMissionDone)
                return;

            MissionTalkHelper.Show(selfTalk, talkSp_start);
        }

        protected override void ApplyCompletedState()
        {
            base.ApplyCompletedState();
            MissionTalkHelper.Show(selfTalk, talkSp_suc);
        }

        public override void OnClickVideoPlayButton()
        {
            base.OnClickVideoPlayButton();
            MissionTalkHelper.Show(selfTalk, talkSp_start);
        }

        protected override void OnSubmitSuccess()
        {
            MissionTalkHelper.Show(selfTalk, talkSp_suc);
            base.OnSubmitSuccess();
        }
    }
}
