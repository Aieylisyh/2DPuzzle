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

        public QuestionAndAnswers[] questions;

        public Image selfTalk;
        public Sprite talkSp_start;
        public Sprite talkSp_suc;
        public Sprite talkSp_fail;

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

            if (selfTalk != null)
                selfTalk.enabled = false;
        }

        protected override void OnQuestionAnswered(QuestionRuntime question)
        {
            if (selfTalk == null)
                return;

            if (question.IsCorrect() && question.talkSp != null)
            {
                selfTalk.sprite = question.talkSp;
                selfTalk.enabled = true;
            }
            else
            {
                selfTalk.enabled = false;
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

        protected override void OnSubmitFail()
        {
            if (selfTalk != null)
            {
                selfTalk.enabled = true;
                selfTalk.sprite = talkSp_fail;
            }

            base.OnSubmitFail();
        }

        public override void OnClickVideoPlayButton()
        {
            base.OnClickVideoPlayButton();

            if (selfTalk != null)
            {
                selfTalk.enabled = true;
                selfTalk.sprite = talkSp_start;
            }

            if (Omni2DSystem.instance != null)
                Omni2DSystem.instance.SwitchBgm();
        }
    }
}
