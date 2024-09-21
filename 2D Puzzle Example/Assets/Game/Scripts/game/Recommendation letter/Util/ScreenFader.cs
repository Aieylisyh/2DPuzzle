using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] float _inDuration = 1.6f;
    [SerializeField] float _outDuration = 1.2f;
    [SerializeField] Image _blackScreen;
    [SerializeField] Image _whiteScreen;

    private void Start()
    {
        _blackScreen.color = new Color(0, 0, 0, 0);
        _blackScreen.raycastTarget = false;
        _whiteScreen.color = new Color(1, 1, 1, 0);
        _whiteScreen.raycastTarget = false;
    }

    public float FadeInBlack(Action callback)
    {
        _blackScreen.raycastTarget = true;
        _blackScreen.DOKill();
        _blackScreen.DOFade(1, _inDuration).OnComplete(() => { callback?.Invoke(); });
        return _inDuration;
    }

    public float FadeOutBlack(Action callback)
    {
        _blackScreen.DOKill();
        _blackScreen.DOFade(0, _outDuration).OnComplete(() =>
        {
            callback?.Invoke();
            _blackScreen.raycastTarget = false;
        });
        return _outDuration;
    }

    public float FadeInWhite(Action callback)
    {
        _whiteScreen.raycastTarget = true;
        _whiteScreen.DOKill();
        _whiteScreen.DOFade(1, _inDuration).OnComplete(() => { callback?.Invoke(); });
        return _inDuration;
    }

    public float FadeOutWhite(Action callback)
    {
        _whiteScreen.DOKill();
        _whiteScreen.DOFade(0, _outDuration).OnComplete(() =>
        {
            callback?.Invoke();
            _whiteScreen.raycastTarget = false;
        });
        return _outDuration;
    }
}