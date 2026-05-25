using Assets.Game.Scripts.game.Omni.Mission;

using Omni;

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



        bool isHidden = true;



        public string correctUsername;

        public string correctPassword;



        public Image profileImage;

        public GameObject passwordArea;

        public Image submitLoginButtonImage;

        public CanvasGroup cg_welcome;



        public Sprite spUser;

        public Sprite spDefault;

        public Sprite spLoginOk;

        public Sprite spLoginNotOk;



        void Start()

        {

            TurnOffPasswordScreen();

        }



        void Update()

        {

            if (Input.GetKeyDown(KeyCode.F2) || Input.GetKeyDown("l"))

            {

                if (Omni2DSystem.instance != null)

                    Omni2DSystem.instance.SkipBootAndLogin();

            }

        }



        public void Boot()

        {

            passwordArea.SetActive(true);

            RefreshLoginButtonState();

        }



        public void TurnOffPasswordScreen()

        {

            inputField_password.contentType = TMP_InputField.ContentType.Password;

            isHidden = true;

            passwordArea.SetActive(false);



            if (submitLoginButtonImage != null)

                submitLoginButtonImage.gameObject.SetActive(false);

        }



        public void OnUsernameChanged(string v)

        {

            if (v == correctUsername)

            {

                profileImage.sprite = spUser;

                if (Omni2DSystem.instance != null && Omni2DSystem.instance.sticker != null)

                    Omni2DSystem.instance.sticker.canRemove = true;

            }

            else

            {

                profileImage.sprite = spDefault;

            }



            RefreshLoginButtonState();

        }



        public void OnPasswordChanged(string v)

        {

            RefreshLoginButtonState();

        }



        void RefreshLoginButtonState()

        {

            if (submitLoginButtonImage == null || passwordArea == null || !passwordArea.activeSelf)

                return;



            submitLoginButtonImage.gameObject.SetActive(true);



            bool usernameReady = inputField_username != null

                && inputField_username.text == correctUsername;

            bool passwordReady = inputField_password != null

                && inputField_password.text.Length > 5;



            submitLoginButtonImage.sprite = usernameReady && passwordReady

                ? spLoginOk

                : spLoginNotOk;

        }



        public void ToggleHidden()

        {

            if (isHidden)

            {

                inputField_password.contentType = TMP_InputField.ContentType.Standard;

                toggleHiddenImg.sprite = toHide;

            }

            else

            {

                inputField_password.contentType = TMP_InputField.ContentType.Password;

                toggleHiddenImg.sprite = toShow;

            }



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


