using UnityEngine;

namespace Assets.Game.Scripts.game.Omni.Mission
{
    public class QuestionRuntime
    {
        public GameObject[] checkmarks;
        public GameObject currentCheckmark;
        public Sprite talkSp;

        public void Check(GameObject checkmarkClicked)
        {
            Reset();
            checkmarkClicked.SetActive(true);
        }

        public bool IsCorrect()
        {
            int checkedCount = 0;
            if (checkmarks == null)
                return false;

            foreach (var cm in checkmarks)
            {
                if (!cm.activeSelf)
                    continue;

                checkedCount++;
                if (cm != currentCheckmark)
                    return false;
            }

            return checkedCount == 1;
        }

        public bool IsChecked()
        {
            if (checkmarks == null)
                return false;

            foreach (var cm in checkmarks)
            {
                if (cm.activeSelf)
                    return true;
            }

            return false;
        }

        public void Reset()
        {
            if (checkmarks == null)
                return;

            foreach (var cm in checkmarks)
                cm.SetActive(false);
        }

        public bool ContainsCheckmark(GameObject checkmark)
        {
            if (checkmarks == null)
                return false;

            foreach (var cm in checkmarks)
            {
                if (cm == checkmark)
                    return true;
            }

            return false;
        }
    }
}
