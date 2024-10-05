using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogBehaviour : MonoBehaviour
{
    public static DialogBehaviour instance;

    public TextMeshProUGUI txt;
    public CanvasGroup cg;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Hide(true);
    }

    void Hide(bool instant)
    {
        if (instant)
        {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
            cg.interactable = false;
        }
        else
        {
            cg.blocksRaycasts = true;
            cg.interactable = true;
            cg.DOKill();
            cg.DOFade(0, 0.7f).OnComplete(
                () =>
                {
                    cg.blocksRaycasts = false;
                    cg.interactable = false;
                }
                );
        }

        if (_cb != null)
        {
            _cb?.Invoke();
            _cb = null;
        }
    }

    public void Show()
    {
        txt.text = "";
        cg.alpha = 1;
        cg.blocksRaycasts = true;
        cg.interactable = true;
    }

    public void OnClick()
    {
        _index++;
        if (_index >= strings.Count)
        {
            Hide(false);
            return;
        }

        var s = strings[_index];
        txt.text = s;
    }

    int _index;
    List<string> strings;
    Action _cb;
    public void SetCallback(Action cb)
    {
        _cb = cb;
    }
    public void SetDialog(List<string> s)
    {
        strings = s;
        _index = -1;
    }
}