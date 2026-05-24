using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.game.Omni.Mission
{
    public class MissionDialogFrame : MonoBehaviour
    {
        public const string Username = "Sam 1211";

        public static MissionDialogFrame instance;

        public TextMeshProUGUI usernameText;
        public TextMeshProUGUI contentText;

        void Awake()
        {
            instance = this;
            ResolveReferences();
            Hide();

            if (usernameText != null)
                usernameText.text = Username;
        }

        void ResolveReferences()
        {
            if (usernameText != null && contentText != null)
                return;

            foreach (var text in GetComponentsInChildren<TextMeshProUGUI>(true))
            {
                if (text.gameObject.name.Contains("username"))
                    usernameText = text;
                else if (text.gameObject.name.Contains("content"))
                    contentText = text;
            }
        }

        public void Show(string text)
        {
            ResolveReferences();

            if (contentText != null)
                contentText.text = text;

            if (usernameText != null)
                usernameText.text = Username;

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
