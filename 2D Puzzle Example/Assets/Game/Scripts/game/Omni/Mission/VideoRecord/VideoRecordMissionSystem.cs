using Assets.Game.Scripts.game.Omni.Mission;
using Omni;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.Omni.Mission.VideoRecord
{
    public class VideoRecordMissionSystem : QuestionMissionBehaviour
    {
        [System.Serializable]
        public class QuestionAndAnswers
        {
            public GameObject[] checkmarks;
            public GameObject currentCheckmark;
        }

        public QuestionAndAnswers[] questions;

        bool hasShownPlayClickTalk;
        bool hasShownVideoLine35;
        bool hasShownVideoLine70;
        bool hasShownFirstFinishTalk;
        bool hasStartedVideoTimedLines;
        Coroutine videoDialogRoutine;

        const float MinVideoLength = 0.1f;
        const float WaitForVideoLengthTimeout = 5f;

        protected override void BindSerializedQuestions()
        {
            if (questions == null)
            {
                runtimeQuestions = null;
                return;
            }

            runtimeQuestions = new QuestionRuntime[questions.Length];
            for (int i = 0; i < questions.Length; i++)
            {
                var q = questions[i];
                runtimeQuestions[i] = new QuestionRuntime
                {
                    checkmarks = q.checkmarks,
                    currentCheckmark = q.currentCheckmark
                };
            }
        }

        protected override void OnResetMission()
        {
            base.OnResetMission();
            ResetVideoDialogState();
        }

        void ResetVideoDialogState()
        {
            StopVideoDialogRoutine();
            hasShownPlayClickTalk = false;
            hasShownVideoLine35 = false;
            hasShownVideoLine70 = false;
            hasShownFirstFinishTalk = false;
            hasStartedVideoTimedLines = false;
        }

        void StopVideoDialogRoutine()
        {
            if (videoDialogRoutine == null)
                return;

            StopCoroutine(videoDialogRoutine);
            videoDialogRoutine = null;
        }

        public void 点击选项(GameObject 选项对应的checkmark)
        {
            base.点击选项(选项对应的checkmark);
        }

        protected override void OnSubmitSuccess()
        {
            TryShowFirstFinishTalk(true);
            base.OnSubmitSuccess();
        }

        protected override void OnSubmitFail()
        {
            TryShowFirstFinishTalk(false);
            base.OnSubmitFail();
        }

        void TryShowFirstFinishTalk(bool allCorrect)
        {
            if (hasShownFirstFinishTalk)
                return;

            hasShownFirstFinishTalk = true;
            MissionDialogFrame.ShowLine(allCorrect
                ? MissionDialogLines.Work3FirstCorrect
                : MissionDialogLines.Work3FirstWrong);
        }

        public override void OnClickVideoPlayButton()
        {
            PrepareVideoForPlayback();
            base.OnClickVideoPlayButton();

            if (!hasShownPlayClickTalk)
            {
                hasShownPlayClickTalk = true;
                MissionDialogFrame.ShowLine(MissionDialogLines.Work3Open);
            }

            if (!hasStartedVideoTimedLines && vp != null)
            {
                hasStartedVideoTimedLines = true;
                StopVideoDialogRoutine();
                videoDialogRoutine = StartCoroutine(MonitorVideoTimedDialog());
            }

            if (Omni2DSystem.instance != null)
                Omni2DSystem.instance.SwitchBgm();
        }

        void PrepareVideoForPlayback()
        {
            if (vp == null)
                return;

            vp.Stop();
            vp.time = 0;
        }

        IEnumerator MonitorVideoTimedDialog()
        {
            float waitElapsed = 0f;
            while (vp != null && vp.length < MinVideoLength && waitElapsed < WaitForVideoLengthTimeout)
            {
                waitElapsed += Time.deltaTime;
                yield return null;
            }

            if (vp == null || vp.length < MinVideoLength)
                yield break;

            double videoLength = vp.length;

            while (vp != null && !vp.isPlaying)
                yield return null;

            while (vp != null && vp.isPlaying && vp.time < 0.01f)
                yield return null;

            while (vp != null && vp.isPlaying)
            {
                float progress = Mathf.Clamp01((float)(vp.time / videoLength));

                if (!hasShownVideoLine35 && progress >= 0.35f)
                {
                    hasShownVideoLine35 = true;
                    MissionDialogFrame.ShowLine(MissionDialogLines.Work3Video35);
                }

                if (!hasShownVideoLine70 && progress >= 0.70f)
                {
                    hasShownVideoLine70 = true;
                    MissionDialogFrame.ShowLine(MissionDialogLines.Work3Video70);
                }

                if (hasShownVideoLine35 && hasShownVideoLine70)
                    break;

                yield return null;
            }

            videoDialogRoutine = null;
        }
    }
}
