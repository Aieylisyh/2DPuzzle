using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace com
{
    public class UiImageScreenFader : MonoBehaviour
    {
        public Image blackScreen;
        public Image whiteScreen;
        public static UiImageScreenFader instance;

        private void Awake()
        {
            instance = this;
        }

        public void FadeBlack(Action cb, float inTime = 0.5f, float outTime = 0.5f, float delay = 0.2f)
        {
            Fade(blackScreen, cb, inTime, outTime, delay);
        }

        public void FadeWhite(Action cb, float inTime = 0.5f, float outTime = 0.5f, float delay = 0.2f)
        {
            Fade(whiteScreen, cb, inTime, outTime, delay);
        }

        public void Fade(Image img, Action cb, float inTime = 0.5f, float outTime = 0.5f, float delay = 0.2f)
        {
            img.DOKill();
            img.DOFade(1, inTime).OnComplete(() =>
            {
                cb?.Invoke();
                img.DOFade(0, outTime).SetDelay(delay);
            });
        }
    }
}