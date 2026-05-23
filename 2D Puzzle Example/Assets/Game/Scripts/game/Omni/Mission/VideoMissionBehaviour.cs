using UnityEngine;
using UnityEngine.Video;

namespace Assets.Game.Scripts.game.Omni.Mission
{
    public abstract class VideoMissionBehaviour : MissionBehaviour
    {
        public GameObject videoPlayButton;
        public VideoPlayer vp;

        protected override void Awake()
        {
            base.Awake();

            if (vp == null)
                return;

            vp.Stop();
            vp.loopPointReached += OnVideoFinished;
        }

        protected virtual void OnVideoFinished(VideoPlayer source)
        {
            if (videoPlayButton != null)
                videoPlayButton.SetActive(true);
        }

        protected void StopVideoAndShowPlayButton()
        {
            if (vp != null)
            {
                vp.Stop();
                ApplyVideoIdleColor();
            }

            if (videoPlayButton != null)
                videoPlayButton.SetActive(true);
        }

        protected void ApplyVideoIdleColor()
        {
            if (vp == null)
                return;

            var clearRt = vp.GetComponent<ClearRenderTexture>();
            if (clearRt != null)
            {
                clearRt.ApplyIdleColor();
                return;
            }

            ClearRenderTexture.FillRenderTexture(vp.targetTexture, new Color(0.12f, 0.12f, 0.14f, 1f));
        }

        public virtual void OnClickVideoPlayButton()
        {
            if (vp != null)
                vp.Play();
            if (videoPlayButton != null)
                videoPlayButton.SetActive(false);
        }

        protected override void OnResetMission()
        {
            StopVideoAndShowPlayButton();
        }
    }
}
