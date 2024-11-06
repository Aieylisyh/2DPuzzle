using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

[System.Serializable]
public class DiaryViewItem
{
    public enum ItemType
    {
        Image,
        Text,
        GameObject
    }

    public enum ToggleMethod
    {
        Active,
        Alpha,
    }

    public Transform item;
    public float delay = 0;
    public float duration = 1;
    public ItemType itemType;
    public ToggleMethod toggleMethod;

    public void Init()
    {
        if (toggleMethod == DiaryViewItem.ToggleMethod.Active)
        {
            item.gameObject.SetActive(false);
        }
        else if (toggleMethod == DiaryViewItem.ToggleMethod.Alpha)
        {
            if (itemType == DiaryViewItem.ItemType.Image)
            {
                var img = item.GetComponent<SpriteRenderer>();
                img.color = new Color(1, 1, 1, 0);
            }
            else if (itemType == DiaryViewItem.ItemType.Text)
            {
                var txt = item.GetComponent<TextMeshPro>();
                txt.color = new Color(1, 1, 1, 0);
            }
        }
    }

    public void Perform()
    {
        if (toggleMethod == DiaryViewItem.ToggleMethod.Active)
        {
            item.gameObject.SetActive(true);
        }
        else if (toggleMethod == DiaryViewItem.ToggleMethod.Alpha)
        {
            if (itemType == DiaryViewItem.ItemType.Image)
            {
                var img = item.GetComponent<SpriteRenderer>();
                img.DOFade(1, duration);
            }
            else if (itemType == DiaryViewItem.ItemType.Text)
            {
                var txt = item.GetComponent<TextMeshPro>();
                txt.DOFade(1, duration);
            }
        }
    }
}

[System.Serializable]
public class DiaryViewItems
{
    public DiaryViewItem[] items;
    public bool completed
    {
        get
        {
            return _index >= items.Length;
        }
    }

    private int _index;
    public Action endCallback;

    public void Play()
    {
        _index = 0;
        PlayCurrent();
    }

    public void Init()
    {
        foreach (var i in items)
        {
            i.Init();
        }
    }

    void CheckNext()
    {
        if (_index < items.Length - 1)
        {
            _index++;
            PlayCurrent();
        }
        else
        {
            endCallback?.Invoke();
        }
    }

    void PlayCurrent()
    {
        Sequence s = DOTween.Sequence();

        var crt = items[_index];
        s.AppendInterval(crt.delay);
        s.AppendCallback(crt.Perform);
        s.AppendInterval(crt.duration);
        s.AppendCallback(CheckNext);
        s.Play();
    }
}