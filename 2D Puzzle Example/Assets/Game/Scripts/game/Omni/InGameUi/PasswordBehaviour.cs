using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.game.Omni.InGameUi
{
    public class PasswordBehaviour : MonoBehaviour
    {
        public TMP_InputField inputField_password;
        public TMP_InputField inputField_username;

        public Image toggleHiddenImg;
        public Sprite toHide;
        public Sprite toShow;

        private bool isHidden = true;

        public string correctUsername;
        public string correctPassword;
        // Use this for initialization
        void Start()
        {
            // 初始化输入框为隐藏状态
            inputField_password.contentType = TMP_InputField.ContentType.Password;
            toggleHiddenImg.sprite = toShow;
            isHidden = true;
        }

        // Update is called once per frame
        public void ToggleHidden()
        {
            if (isHidden)
            {
                // 显示输入内容
                inputField_password.contentType = TMP_InputField.ContentType.Standard;
                toggleHiddenImg.sprite = toHide;
            }
            else
            {
                // 隐藏输入内容
                inputField_password.contentType = TMP_InputField.ContentType.Password;
                toggleHiddenImg.sprite = toShow;
            }

            // 刷新输入框，确保显示状态更新
            inputField_password.ForceLabelUpdate();
            isHidden = !isHidden;
        }

        public void Submit()
        {
            if (inputField_username.text == correctUsername)
            {
                if (inputField_password.text == correctPassword)
                {
                    Debug.Log("用户名密码正确");
                }
                else
                {
                    Debug.Log("密码错误");
                }
            }
            else
            {
                Debug.Log("用户不存在");
            }
        }
    }
}