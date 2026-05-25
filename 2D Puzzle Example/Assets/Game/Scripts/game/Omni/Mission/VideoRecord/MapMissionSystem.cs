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

        bool hasDismissedOpenTalk;

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
            ResetFirstSubmitTalkFlag();
            hasDismissedOpenTalk = false;

            if (currentMissionDone)
                return;

            ShowDialogOrImage(talkSp_start, MissionDialogLines.Work4Open);
        }

        protected override void ApplyCompletedState()
        {
            base.ApplyCompletedState();
            ShowDialogOrImage(talkSp_suc, MissionDialogLines.Work4FirstSubmitSuccess);
        }

        public override void OnClickMapRedDot()
        {
            if (!hasDismissedOpenTalk)
            {
                hasDismissedOpenTalk = true;
                DismissMissionDialog(selfTalk);
            }

            base.OnClickMapRedDot();
        }

        protected override void OnSubmitSuccess()
        {
            TryShowFirstSubmitTalk(
                true,
                MissionDialogLines.Work4FirstSubmitSuccess,
                null,
                selfTalk,
                talkSp_suc);
            base.OnSubmitSuccess();
        }

        void ShowDialogOrImage(Sprite sprite, string line)
        {
            if (PreserveMissionDialog)
                return;

            if (sprite != null)
                ShowMissionTalk(selfTalk, sprite);
            else
                ShowMissionDialog(line);
        }
    }
}
