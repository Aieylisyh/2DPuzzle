using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.game.Omni.Mission
{
    public class MissionNotification : MonoBehaviour
    {
        public MissionPrototype proto;
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
            //点击勾 添加这个新任务
            MissionListPanel.instance.AddMission(proto);
            Hide();
        }

        public void Show(MissionPrototype p)
        {
            if (_isShowing)
            {
                return;
            }

            proto = p;
            title.text = proto.title;
            agentNameTxt.text = proto.agentName;
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