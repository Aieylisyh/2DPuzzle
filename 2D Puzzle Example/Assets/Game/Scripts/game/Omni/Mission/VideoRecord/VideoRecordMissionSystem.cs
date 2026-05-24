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

        bool hasShownVideoLine35;
        bool hasShownVideoLine70;
        bool hasShownFirstCorrectTalk;
        bool hasShownFirstWrongTalk;
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

            if (currentMissionDone)
                return;

            ShowWork3OpenTalk();
        }

        void ResetVideoDialogState()
        {
            StopVideoDialogRoutine();
            hasShownVideoLine35 = false;
            hasShownVideoLine70 = false;
            hasShownFirstCorrectTalk = false;
            hasShownFirstWrongTalk = false;
            hasStartedVideoTimedLines = false;
        }

        void StopVideoDialogRoutine()
        {
            if (videoDialogRoutine == null)
                return;

            StopCoroutine(videoDialogRoutine);
            videoDialogRoutine = null;
        }

        void ShowWork3OpenTalk()
        {
            MissionDialogFrame.ShowLine(MissionDialogLines.Work3Open);
        }

        public void 点击选项(GameObject 选项对应的checkmark)
        {
            base.点击选项(选项对应的checkmark);
        }

        protected override void OnQuestionAnswered(QuestionRuntime question)
        {
            if (question.IsCorrect())
            {
                if (hasShownFirstCorrectTalk)
                    return;

                hasShownFirstCorrectTalk = true;
                MissionDialogFrame.ShowLine(MissionDialogLines.Work3FirstCorrect);
                return;
            }

            if (hasShownFirstWrongTalk)
                return;

            hasShownFirstWrongTalk = true;
            MissionDialogFrame.ShowLine(MissionDialogLines.Work3FirstWrong);
        }

        public override void OnClickVideoPlayButton()
        {
            PrepareVideoForPlayback();
            base.OnClickVideoPlayButton();

            ShowWork3OpenTalk();

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
