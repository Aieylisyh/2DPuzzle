using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.Omni.Mission.TextInfo
{
    public class TextInfoMissionSystem : MonoBehaviour
    {
        private void Start()
        {
            RefreshFinishBtn();
            restaurantCheckmark1.SetActive(false);
            restaurantCheckmark2.SetActive(false);
            restaurantCheckmark3.SetActive(false);
            restaurantCheckmark4.SetActive(false);
            restaurantCheckmark5.SetActive(false);
            restaurantCheckmark6.SetActive(false);
        }
        /// <summary>
        /// 首次开始这个任务
        /// </summary>
        public void ResetMission()
        {

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
            Debug.Log(checkmark);
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
    }
}