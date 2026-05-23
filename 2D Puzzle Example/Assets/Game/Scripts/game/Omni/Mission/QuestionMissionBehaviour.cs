using UnityEngine;

namespace Assets.Game.Scripts.game.Omni.Mission
{
    public abstract class QuestionMissionBehaviour : VideoMissionBehaviour
    {
        protected QuestionRuntime[] runtimeQuestions;

        protected override void Awake()
        {
            BindSerializedQuestions();
            base.Awake();
            ResetAllQuestions();
        }

        protected abstract void BindSerializedQuestions();

        public void 点击选项(GameObject 选项对应的checkmark)
        {
            if (runtimeQuestions == null || 选项对应的checkmark == null)
                return;

            foreach (var q in runtimeQuestions)
            {
                if (!q.ContainsCheckmark(选项对应的checkmark))
                    continue;

                q.Check(选项对应的checkmark);
                OnQuestionAnswered(q);
                break;
            }

            RefreshFinishBtn();
        }

        protected virtual void OnQuestionAnswered(QuestionRuntime question) { }

        protected void ResetAllQuestions()
        {
            if (runtimeQuestions == null)
                return;

            foreach (var q in runtimeQuestions)
                q.Reset();
        }

        protected override void OnResetMission()
        {
            base.OnResetMission();
            ResetAllQuestions();
        }

        protected override bool ValidateSubmission()
        {
            if (runtimeQuestions == null)
                return true;

            foreach (var q in runtimeQuestions)
            {
                if (!q.IsCorrect())
                    return false;
            }

            return true;
        }

        protected override void RefreshFinishBtn()
        {
            bool allChecked = runtimeQuestions != null && runtimeQuestions.Length > 0;

            if (runtimeQuestions != null)
            {
                foreach (var q in runtimeQuestions)
                {
                    if (!q.IsChecked())
                        allChecked = false;
                }
            }
            else
            {
                allChecked = false;
            }

            ToggleFinishedButton(allChecked);
        }
    }
}
