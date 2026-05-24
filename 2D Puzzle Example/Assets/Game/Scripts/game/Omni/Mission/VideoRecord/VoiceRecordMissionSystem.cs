using Assets.Game.Scripts.game.Omni.Mission;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.game.Omni.Mission.VideoRecord
{
    public class VoiceRecordMissionSystem : QuestionMissionBehaviour
    {
        [System.Serializable]
        public class QuestionAndAnswers
        {
            public GameObject[] checkmarks;
            public GameObject currentCheckmark;
        }

        public QuestionAndAnswers[] questions;

        public Image selfTalk;
        public Sprite talkSp_start;
        public Sprite talkSp_allAnswered;

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

        protected override void Awake()
        {
            base.Awake();
            MissionTalkHelper.Hide(selfTalk);
        }

        protected override void OnResetMission()
        {
            base.OnResetMission();
            ShowDialogOrImage(selfTalk, talkSp_start, MissionDialogLines.Work2Open);
        }

        public void 点击选项(GameObject 选项对应的checkmark)
        {
            base.点击选项(选项对应的checkmark);
        }

        protected override void RefreshFinishBtn()
        {
            base.RefreshFinishBtn();

            if (runtimeQuestions == null)
                return;

            foreach (var q in runtimeQuestions)
            {
                if (!q.IsChecked())
                    return;
            }

            ShowDialogOrImage(selfTalk, talkSp_allAnswered, MissionDialogLines.Work2AllAnswered);
        }

        static void ShowDialogOrImage(Image selfTalk, Sprite sprite, string line)
        {
            if (sprite != null)
                MissionTalkHelper.Show(selfTalk, sprite);
            else
                MissionDialogFrame.ShowLine(line);
        }
    }
}
