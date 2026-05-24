using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.game.Omni.Mission
{
    public static class MissionTalkHelper
    {
        public static void Show(Image selfTalk, Sprite sprite)
        {
            if (selfTalk == null || sprite == null)
                return;

            selfTalk.gameObject.SetActive(true);
            selfTalk.sprite = sprite;
            selfTalk.enabled = true;
        }

        public static void Hide(Image selfTalk)
        {
            if (selfTalk == null)
                return;

            selfTalk.enabled = false;
            selfTalk.gameObject.SetActive(false);
        }
    }
}
