using Assets.Game.Scripts.game.Omni.Mission;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.game.Omni.Mission.TextInfo
{
    public class TextInfoMissionSystem : MissionBehaviour
    {
        public Image selfTalk;
        public Sprite talkSp_open;
        public Sprite talkSp_afterKeyword;
        public Sprite talkSp_chooseRestaurant;
        public Sprite talkSp_readySubmit;

        bool hasShownKeywordTalk;
        bool hasShownAllKeywordsTalk;
        bool hasShownReadySubmitTalk;

        public GameObject[] correctRestaurantCheckmarks;
        public GameObject[] restaurantCheckmarks;

        public GameObject keyword_burger;
        public GameObject keyword_burgers;
        public GameObject keyword_20min;
        public GameObject keyword_15min;
        public GameObject keyword_0fee;
        public GameObject keyword_199fee;
        public GameObject keyword_15_25_perperson;
        public GameObject keyword_20_25_perperson;

        public GameObject clueLv3_burger;
        public GameObject clueLv3_min;
        public GameObject clueLv3_fee;
        public GameObject clueLv3_perperson;

        public TextMeshProUGUI txt_clueLv3;
        public TextMeshProUGUI txt_clueLv4;

        protected override void OnResetMission()
        {
            ResetFirstSubmitTalkFlag();
            hasShownKeywordTalk = false;
            hasShownAllKeywordsTalk = false;
            hasShownReadySubmitTalk = false;
            HideMissionTalk(selfTalk);

            if (restaurantCheckmarks != null)
            {
                foreach (var cm in restaurantCheckmarks)
                    SetActiveIfNotNull(cm, false);
            }

            SetActiveIfNotNull(keyword_burger, false);
            SetActiveIfNotNull(keyword_burgers, false);
            SetActiveIfNotNull(keyword_20min, false);
            SetActiveIfNotNull(keyword_15min, false);
            SetActiveIfNotNull(keyword_0fee, false);
            SetActiveIfNotNull(keyword_199fee, false);
            SetActiveIfNotNull(keyword_15_25_perperson, false);
            SetActiveIfNotNull(keyword_20_25_perperson, false);

            CheckKeywordsTriggerClueLv3();

            if (txt_clueLv4 != null)
                txt_clueLv4.text = "0/3";

            RefreshFinishBtn();

            if (PreserveMissionDialog)
                return;

            ShowDialogOrImage(talkSp_open, MissionDialogLines.Work1Open);
        }

        public override void SubmitMission()
        {
            DismissMissionDialog(selfTalk);
            base.SubmitMission();
        }

        void ShowDialogOrImage(Sprite sprite, string line)
        {
            if (PreserveMissionDialog)
                return;

            if (sprite != null)
                MissionTalkHelper.Show(selfTalk, sprite);
            else
                MissionDialogFrame.ShowLine(line);
        }

        protected override bool ValidateSubmission()
        {
            if (restaurantCheckmarks == null || correctRestaurantCheckmarks == null)
                return false;

            int checkedCount = 0;
            bool result = true;

            foreach (var cm in restaurantCheckmarks)
            {
                if (cm == null || !cm.activeSelf)
                    continue;

                checkedCount++;
                if (!ContainsCheckmark(correctRestaurantCheckmarks, cm))
                    result = false;
            }

            if (checkedCount != correctRestaurantCheckmarks.Length)
                result = false;

            return result;
        }

        public void OnRestaurantChecked(GameObject checkmark)
        {
            if (checkmark == null)
                return;

            int checkedCount = CountActiveGameObjects(restaurantCheckmarks);

            if (checkmark.activeSelf)
            {
                checkmark.SetActive(false);
            }
            else
            {
                if (checkedCount >= 3)
                    return;
                checkmark.SetActive(true);
            }

            TryShowReadySubmitTalk();
            RefreshFinishBtn();
        }

        protected override void RefreshFinishBtn()
        {
            int checkedCount = CountActiveGameObjects(restaurantCheckmarks);

            if (txt_clueLv4 != null)
                txt_clueLv4.text = checkedCount + "/3";

            if (checkedCount >= 3)
                TryShowReadySubmitTalk();

            ToggleFinishedButton(checkedCount >= 3);
        }

        public void OnKeywordChecked(GameObject kw)
        {
            if (kw == null || kw.activeSelf)
                return;

            kw.SetActive(true);

            if (!hasShownKeywordTalk)
            {
                hasShownKeywordTalk = true;
                ShowDialogOrImage(talkSp_afterKeyword, MissionDialogLines.Work1FirstKeyword);
            }

            CheckKeywordsTriggerClueLv3();
            TryShowAllKeywordsTalk();
        }

        void TryShowAllKeywordsTalk()
        {
            if (hasShownAllKeywordsTalk || !AllKeywordsChecked())
                return;

            hasShownAllKeywordsTalk = true;
            ShowDialogOrImage(talkSp_chooseRestaurant, MissionDialogLines.Work1AllKeywords);
        }

        void TryShowReadySubmitTalk()
        {
            if (hasShownReadySubmitTalk || !AllKeywordsChecked())
                return;

            int checkedCount = CountActiveGameObjects(restaurantCheckmarks);
            if (checkedCount < 3 || !HasAnyCorrectRestaurantSelected())
                return;

            hasShownReadySubmitTalk = true;
            ShowDialogOrImage(talkSp_readySubmit, MissionDialogLines.Work1ReadySubmit);
        }

        bool AllKeywordsChecked()
        {
            return IsActive(keyword_burger)
                && IsActive(keyword_burgers)
                && IsActive(keyword_20min)
                && IsActive(keyword_15min)
                && IsActive(keyword_0fee)
                && IsActive(keyword_199fee)
                && IsActive(keyword_15_25_perperson)
                && IsActive(keyword_20_25_perperson);
        }

        bool HasAnyCorrectRestaurantSelected()
        {
            if (correctRestaurantCheckmarks == null)
                return false;

            foreach (var cm in correctRestaurantCheckmarks)
            {
                if (IsActive(cm))
                    return true;
            }

            return false;
        }

        void CheckKeywordsTriggerClueLv3()
        {
            int triggered = 0;

            SetActiveIfNotNull(clueLv3_burger, IsActive(keyword_burger) && IsActive(keyword_burgers));
            SetActiveIfNotNull(clueLv3_min, IsActive(keyword_20min) && IsActive(keyword_15min));
            SetActiveIfNotNull(clueLv3_fee, IsActive(keyword_0fee) && IsActive(keyword_199fee));
            SetActiveIfNotNull(clueLv3_perperson, IsActive(keyword_15_25_perperson) && IsActive(keyword_20_25_perperson));

            if (IsActive(clueLv3_burger)) triggered++;
            if (IsActive(clueLv3_min)) triggered++;
            if (IsActive(clueLv3_fee)) triggered++;
            if (IsActive(clueLv3_perperson)) triggered++;

            if (txt_clueLv3 != null)
                txt_clueLv3.text = triggered + "/4";
        }

        static bool IsActive(GameObject go) => go != null && go.activeSelf;

        static bool ContainsCheckmark(GameObject[] checkmarks, GameObject target)
        {
            if (checkmarks == null || target == null)
                return false;

            foreach (var cm in checkmarks)
            {
                if (cm == target)
                    return true;
            }

            return false;
        }
    }
}
