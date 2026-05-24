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
        Coroutine videoDialogRoutine;

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
            hasShownVideoLine35 = false;
            hasShownVideoLine70 = false;
            hasShownFirstCorrectTalk = false;
            hasShownFirstWrongTalk = false;
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
            base.OnClickVideoPlayButton();

            StopVideoDialogRoutine();
            hasShownVideoLine35 = false;
            hasShownVideoLine70 = false;

            MissionDialogFrame.ShowLine(MissionDialogLines.Work3Open);

            if (vp != null)
                videoDialogRoutine = StartCoroutine(MonitorVideoTimedDialog());

            if (Omni2DSystem.instance != null)
                Omni2DSystem.instance.SwitchBgm();
        }

        IEnumerator MonitorVideoTimedDialog()
        {
            while (vp != null && vp.isPlaying)
            {
                if (vp.length > 0f)
                {
                    float progress = (float)(vp.time / vp.length);

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
                }

                yield return null;
            }

            videoDialogRoutine = null;
        }
    }
}
