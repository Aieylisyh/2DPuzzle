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

            if (usernameText != null)
                usernameText.text = Username;
        }

        void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }

        public static MissionDialogFrame Resolve()
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<MissionDialogFrame>(true);
            return instance;
        }

        public static void ShowLine(string text)
        {
            Resolve()?.Show(text);
        }

        public static void HideLine()
        {
            var frame = Resolve();
            if (frame != null)
                frame.Hide();
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
