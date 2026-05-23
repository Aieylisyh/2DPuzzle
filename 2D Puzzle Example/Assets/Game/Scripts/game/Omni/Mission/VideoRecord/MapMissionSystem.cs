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

        public MapData[] mapDatas;

        public Image selfTalk;
        public Sprite talkSp_start;
        public Sprite talkSp_suc;

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

        protected override void ApplyCompletedState()
        {
            base.ApplyCompletedState();

            if (selfTalk != null)
            {
                selfTalk.enabled = true;
                selfTalk.sprite = talkSp_suc;
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
        }
    }
}
