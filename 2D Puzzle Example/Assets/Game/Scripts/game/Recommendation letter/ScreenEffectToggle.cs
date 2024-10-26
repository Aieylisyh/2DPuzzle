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
    public CameraFilterPack_Blur_Blurry _blurry;
    public CameraFilterPack_FX_Drunk _drunk;
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

    public void ToggleBlurry(bool b)
    {
        _blurry.enabled = b;
    }

    public void ToggleDrunk(bool b)
    {
        _drunk.enabled = b;
    }
}