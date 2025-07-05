using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.game.Omni.Mission
{
    public class MissionNotification : MonoBehaviour
    {
        public MissionData crtMissionData;
        public GameObject view;

        public TextMeshProUGUI title;
        public TextMeshProUGUI agentNameTxt;

        private bool _isShowing;

        private void Awake()
        {
            _isShowing = true;
            Hide();
        }

        public void OnClickChecked()
        {
            Debug.Log("OnClickChecked");
            //点击勾 添加这个新任务
            MissionListPanel.instance.AddMission(crtMissionData);
            Hide();
        }

        public void Show(MissionData md)
        {
            if (_isShowing)
            {
                return;
            }

            crtMissionData = md;
            title.text = "\"" + md.proto.title + "\"";
            agentNameTxt.text = md.proto.agentName;
            _isShowing = true;
            view.SetActive(true);
        }

        public void Hide()
        {
            if (!_isShowing)
            {
                return;
            }

            _isShowing = false;
            view.SetActive(false);
        }
    }
}