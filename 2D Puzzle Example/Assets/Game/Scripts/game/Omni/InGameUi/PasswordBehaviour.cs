using DG.Tweening;
using Omni;
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

        public GameObject profileImage;
        public GameObject passwordArea;
        public GameObject submitLoginButton;
        public CanvasGroup cg_welcome;

        void Start()
        {
            // 初始化输入框为隐藏状态
            inputField_password.contentType = TMP_InputField.ContentType.Password;
            toggleHiddenImg.sprite = toShow;
            isHidden = true;
            cg_welcome.alpha = 0;
        }

        public void Reboot()
        {
            profileImage.SetActive(false);
            passwordArea.SetActive(false);
            submitLoginButton.SetActive(false);
            StartCoroutine(RebootCo());
        }

        public void TurnOffPasswordScreen()
        {
            profileImage.SetActive(false);
            passwordArea.SetActive(false);
            submitLoginButton.SetActive(false);
            cg_welcome.alpha = 0;
            cg_welcome.interactable = false;
            cg_welcome.blocksRaycasts = false;
            StopCoroutine(RebootCo());
        }
        IEnumerator RebootCo()
        {
            cg_welcome.alpha = 0;
            cg_welcome.blocksRaycasts = true;
            yield return new WaitForSeconds(1.0f);
            cg_welcome.DOFade(1, 2);
            yield return new WaitForSeconds(2.5f);
            cg_welcome.DOFade(0, 1);
            yield return new WaitForSeconds(1.0f);
            cg_welcome.alpha = 0;
            cg_welcome.blocksRaycasts = false;

            passwordArea.SetActive(true);
        }

        public void OnUsernameChanged(string v)
        {
            if (v == correctUsername)
            {
                profileImage.SetActive(true);
            }
            else
            {
                profileImage.SetActive(false);
            }

        }

        public void OnPasswordChanged(string v)
        {
            if (v.Length > 5)
            {
                submitLoginButton.SetActive(true);
            }
            else
            {
                submitLoginButton.SetActive(false);
            }
        }

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
                    Omni2DSystem.instance.OnLoginSuc();
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