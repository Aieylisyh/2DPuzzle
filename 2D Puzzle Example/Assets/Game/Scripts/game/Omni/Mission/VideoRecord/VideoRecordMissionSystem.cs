using Assets.Game.Scripts.game.Omni.Mission;
using Omni;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.game.Omni.Mission.VideoRecord
{
    public class VideoRecordMissionSystem : QuestionMissionBehaviour
    {
        [System.Serializable]
        public class QuestionAndAnswers
        {
            public GameObject[] checkmarks;
            public GameObject currentCheckmark;
            public Sprite talkSp;
        }

        public Image selfTalk;
        public Sprite talkSp_start;
        public Sprite talkSp_suc;
        public Sprite talkSp_fail;

        public QuestionAndAnswers[] questions;

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
                    currentCheckmark = q.currentCheckmark,
                    talkSp = q.talkSp
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
            MissionTalkHelper.Hide(selfTalk);
        }

        public void 点击选项(GameObject 选项对应的checkmark)
        {
            base.点击选项(选项对应的checkmark);
        }

        protected override void OnQuestionAnswered(QuestionRuntime question)
        {
            if (selfTalk == null || question.talkSp == null)
                return;

            if (question.IsCorrect())
                MissionTalkHelper.Show(selfTalk, question.talkSp);
            else
                MissionTalkHelper.Show(selfTalk, talkSp_fail != null ? talkSp_fail : question.talkSp);
        }

        protected override void OnSubmitSuccess()
        {
            MissionTalkHelper.Show(selfTalk, talkSp_suc);
            base.OnSubmitSuccess();
        }

        protected override void OnSubmitFail()
        {
            MissionTalkHelper.Show(selfTalk, talkSp_fail);
            base.OnSubmitFail();
        }

        public override void OnClickVideoPlayButton()
        {
            base.OnClickVideoPlayButton();
            MissionTalkHelper.Show(selfTalk, talkSp_start);

            if (Omni2DSystem.instance != null)
                Omni2DSystem.instance.SwitchBgm();
        }
    }
}
