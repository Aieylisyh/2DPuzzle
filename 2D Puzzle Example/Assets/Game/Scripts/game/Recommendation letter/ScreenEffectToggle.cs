using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ScreenEffectToggle : MonoBehaviour
{
    public static ScreenEffectToggle instance;
    public GameObject[] realityVfxs;
    public GameObject[] dreamVfxs;

    private void Awake()
    {
        instance = this;
    }

    public void ToggleDreamReality(bool b)
    {
        foreach (var v in realityVfxs)
        {
            v.SetActive(!b);
        }
        foreach (var v in dreamVfxs)
        {
            v.SetActive(b);
        }
    }
}