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
        bool videoTimedLinesStarted;
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
            if (videoDialogRoutine != null)
            {
                StopCoroutine(videoDialogRoutine);
                videoDialogRoutine = null;
            }

            hasShownVideoLine35 = false;
            hasShownVideoLine70 = false;
            videoTimedLinesStarted = false;
            hasShownFirstCorrectTalk = false;
            hasShownFirstWrongTalk = false;
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
                MissionDialogFrame.instance?.Show(MissionDialogLines.Work3FirstCorrect);
                return;
            }

            if (hasShownFirstWrongTalk)
                return;

            hasShownFirstWrongTalk = true;
            MissionDialogFrame.instance?.Show(MissionDialogLines.Work3FirstWrong);
        }

        public override void OnClickVideoPlayButton()
        {
            base.OnClickVideoPlayButton();
            MissionDialogFrame.instance?.Show(MissionDialogLines.Work3Open);

            if (!videoTimedLinesStarted && vp != null)
            {
                videoTimedLinesStarted = true;
                videoDialogRoutine = StartCoroutine(MonitorVideoTimedDialog());
            }

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
                        MissionDialogFrame.instance?.Show(MissionDialogLines.Work3Video35);
                    }

                    if (!hasShownVideoLine70 && progress >= 0.70f)
                    {
                        hasShownVideoLine70 = true;
                        MissionDialogFrame.instance?.Show(MissionDialogLines.Work3Video70);
                    }
                }

                yield return null;
            }

            videoDialogRoutine = null;
        }
    }
}
