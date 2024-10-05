using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace com
{
    public class UiImageScreenFader : MonoBehaviour
    {
        [SerializeField] Image _blackScreen;
        [SerializeField] Image _whiteScreen;
        public static UiImageScreenFader instance;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            _blackScreen.color = new Color(0, 0, 0, 0);
            _blackScreen.raycastTarget = false;
            _whiteScreen.color = new Color(1, 1, 1, 0);
            _whiteScreen.raycastTarget = false;
        }
        public void FadeBlack(Action cb, float inTime = 0.5f, float outTime = 0.5f, float delay = 0.2f)
        {
            Fade(_blackScreen, cb, inTime, outTime, delay);
        }

        public void FadeWhite(Action cb, float inTime = 0.5f, float outTime = 0.5f, float delay = 0.2f)
        {
            Fade(_whiteScreen, cb, inTime, outTime, delay);
        }

        public void FadeInBlack(Action cb, float inTime = 0.5f)
        {
            FadeIn(_blackScreen, cb, inTime);
        }

        public void FadeInWhite(Action cb, float inTime = 0.5f)
        {
            FadeIn(_whiteScreen, cb, inTime);
        }
        public void FadeOutBlack(Action cb, float outTime = 0.5f)
        {
            FadeOut(_blackScreen, cb, outTime);
        }

        public void FadeOutWhite(Action cb, float outTime = 0.5f)
        {
            FadeOut(_whiteScreen, cb, outTime);
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

        public void FadeIn(Image img, Action cb, float inTime = 0.5f)
        {
            img.DOKill();
            img.DOFade(1, inTime).OnComplete(() =>
            {
                cb?.Invoke();
            });
        }

        public void FadeOut(Image img, Action cb, float outTime = 0.5f)
        {
            img.DOKill();
            img.DOFade(0, outTime).OnComplete(() =>
            {
                cb?.Invoke();
            });
        }
    }
}