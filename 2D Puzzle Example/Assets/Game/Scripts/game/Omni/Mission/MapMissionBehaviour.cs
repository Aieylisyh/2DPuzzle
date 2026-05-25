using UnityEngine;
using UnityEngine.Video;

namespace Assets.Game.Scripts.game.Omni.Mission
{
    public abstract class MapMissionBehaviour : VideoMissionBehaviour
    {
        public GameObject redDotButton;
        protected MapDataRuntime[] runtimeMapDatas;

        protected void BuildRuntimeMapDatas(float[] startTimes, float[] endTimes, GameObject[] hints)
        {
            if (hints == null)
            {
                runtimeMapDatas = null;
                return;
            }

            runtimeMapDatas = new MapDataRuntime[hints.Length];
            for (int i = 0; i < hints.Length; i++)
            {
                runtimeMapDatas[i] = new MapDataRuntime
                {
                    startTime = startTimes != null && i < startTimes.Length ? startTimes[i] : 0f,
                    endTime = endTimes != null && i < endTimes.Length ? endTimes[i] : 0f,
                    hint = hints[i]
                };
            }
        }

        protected override void Awake()
        {
            BindSerializedMapDatas();
            base.Awake();

            if (redDotButton != null)
                redDotButton.SetActive(false);
        }

        protected abstract void BindSerializedMapDatas();

        protected override void OnVideoFinished(VideoPlayer source)
        {
            base.OnVideoFinished(source);

            if (redDotButton != null)
                redDotButton.SetActive(false);
        }

        public override void ResetMission()
        {
            base.ResetMission();

            if (currentMissionDone)
                ApplyCompletedState();
        }

        protected virtual void ApplyCompletedState()
        {
            if (videoPlayButton != null)
                videoPlayButton.SetActive(false);
            if (redDotButton != null)
                redDotButton.SetActive(false);
        }

        public virtual void OnClickMapRedDot()
        {
            if (vp == null || runtimeMapDatas == null)
                return;

            var t = vp.time;
            Debug.Log(GetType().Name + " OnClickMapRedDot t " + t);

            foreach (var d in runtimeMapDatas)
            {
                if (d.hint == null)
                    continue;

                if (d.startTime < t && d.endTime > t)
                    d.hint.SetActive(true);
            }

            RefreshFinishBtn();
        }

        protected override void RefreshFinishBtn()
        {
            bool allChecked = runtimeMapDatas != null && runtimeMapDatas.Length > 0;

            if (runtimeMapDatas != null)
            {
                foreach (var d in runtimeMapDatas)
                {
                    if (d.hint == null || !d.hint.activeSelf)
                        allChecked = false;
                }
            }
            else
            {
                allChecked = false;
            }

            ToggleFinishedButton(allChecked);
        }

        public override void OnClickVideoPlayButton()
        {
            base.OnClickVideoPlayButton();

            if (redDotButton != null)
                redDotButton.SetActive(true);
        }
    }
}
