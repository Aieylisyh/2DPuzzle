using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.game.Omni.Mission.TextInfo
{
    public class TextInfoMissionSystem : MonoBehaviour
    {
        public GameObject view;

        /// <summary>
        /// 首次开始这个任务
        /// </summary>
        public void ResetMission()
        {
            view.SetActive(true);

            RefreshFinishBtn();
            restaurantCheckmark1.SetActive(false);
            restaurantCheckmark2.SetActive(false);
            restaurantCheckmark3.SetActive(false);
            restaurantCheckmark4.SetActive(false);
            restaurantCheckmark5.SetActive(false);
            restaurantCheckmark6.SetActive(false);

            keyword_burger.SetActive(false);
            keyword_burgers.SetActive(false);
            keyword_20min.SetActive(false);
            keyword_15min.SetActive(false);
            keyword_0fee.SetActive(false);
            keyword_199fee.SetActive(false);
            keyword_15_25_perperson.SetActive(false);
            keyword_20_25_perperson.SetActive(false);

            CheckKeywordsTriggerClueLv3();

            txt_clueLv4.text = "0/3";
            // txt_clueLv4.text = "need 3 checked";
        }

        public void Hide()
        {
            view.SetActive(false);
        }

        /// <summary>
        /// 失败了重新开始 去掉餐厅的勾勾
        /// </summary>
        public void RetryMission()
        {

        }

        /// <summary>
        /// 点击finish触发（至少勾选了3个餐厅出现可以点击的finish）
        /// </summary>
        public void SubmitMission()
        {

        }

        public GameObject restaurantCheckmark1;
        public GameObject restaurantCheckmark2;
        public GameObject restaurantCheckmark3;
        public GameObject restaurantCheckmark4;
        public GameObject restaurantCheckmark5;
        public GameObject restaurantCheckmark6;

        public GameObject finishBtn_ok;
        public GameObject finishBtn_notOk;

        int GetRestaurantCheckedCount()
        {
            int checkedCount = 0;
            if (restaurantCheckmark1.activeSelf) checkedCount++;
            if (restaurantCheckmark2.activeSelf) checkedCount++;
            if (restaurantCheckmark3.activeSelf) checkedCount++;
            if (restaurantCheckmark4.activeSelf) checkedCount++;
            if (restaurantCheckmark5.activeSelf) checkedCount++;
            if (restaurantCheckmark6.activeSelf) checkedCount++;
            return checkedCount;
        }

        void RefreshFinishBtn()
        {
            int checkedCount = GetRestaurantCheckedCount();
            txt_clueLv4.text = checkedCount + "/3";
            if (checkedCount >= 3)
            {
                finishBtn_ok.SetActive(true);
                finishBtn_notOk.SetActive(false);
            }
            else
            {
                finishBtn_ok.SetActive(false);
                finishBtn_notOk.SetActive(true);
            }
        }

        public void OnRestaurantChecked(GameObject checkmark)
        {
            //Debug.Log(checkmark);
            int checkedCount = GetRestaurantCheckedCount();

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

            RefreshFinishBtn();
        }

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

        public void OnKeywordChecked(GameObject kw)
        {
            if (!kw.activeSelf)
            {
                kw.SetActive(true);
            }
            CheckKeywordsTriggerClueLv3();
        }

        void CheckKeywordsTriggerClueLv3()
        {
            int triggered = 0;
            clueLv3_burger.SetActive(keyword_burger.activeSelf && keyword_burgers.activeSelf);
            clueLv3_min.SetActive(keyword_20min.activeSelf && keyword_15min.activeSelf);
            clueLv3_fee.SetActive(keyword_0fee.activeSelf && keyword_199fee.activeSelf);
            clueLv3_perperson.SetActive(keyword_15_25_perperson.activeSelf && keyword_20_25_perperson.activeSelf);

            if (clueLv3_burger.activeSelf) triggered++;
            if (clueLv3_min.activeSelf) triggered++;
            if (clueLv3_fee.activeSelf) triggered++;
            if (clueLv3_perperson.activeSelf) triggered++;


            txt_clueLv3.text = triggered + "/4";
        }
    }
}