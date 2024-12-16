using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TinyStageOfMimicPanels : MonoBehaviour
{
    public Transform mimicPanelObjectParent;
    public MimicPanelObject[] mimicPanelObjects { get; private set; }
    public float stageDuration;
    Coroutine crtStagePlayCoroutine;
    public bool playing { get; private set; }

    private List<(int, int)> existingSortingLayerAndObjectCount = new List<(int, int)>();
    public void Init()
    {
        if (mimicPanelObjectParent == null)
            mimicPanelObjectParent = this.transform;
        Debug.Log("Init");
        playing = false;

        //用这个复杂的方式，避免同一个sorting layer的对象出现
        mimicPanelObjects = mimicPanelObjectParent.GetComponentsInChildren<MimicPanelObject>();
        foreach (var mpo in mimicPanelObjects)
        {
            mpo.Init();
            var sr = mpo.GetComponentInChildren<SpriteRenderer>();
            var oil = sr.sortingOrder;

            int existedIndex = -1;
            int existedCount = 0;

            for (int i = 0; i < existingSortingLayerAndObjectCount.Count; i++)
            {
                var e = existingSortingLayerAndObjectCount[i];
                if (e.Item1 == oil)
                {
                    existedIndex = i;
                    existedCount = e.Item2;
                    existingSortingLayerAndObjectCount[i] = new(existedIndex, existedCount + 1);
                }
            }
            if (existedIndex < 0)
            {
                existingSortingLayerAndObjectCount.Add(new(oil, 1));
            }

            sr.sortingOrder = oil * 10 + existedCount;
        }
    }

    Action _endCallback;

    public void StartPlay(Action endCallback)
    {
        if (playing)
            return;

        _endCallback = endCallback;
        Debug.Log("StartPlay");
        playing = true;
        foreach (var mpo in mimicPanelObjects)
        {
            mpo.Init();
            mpo.StartAnim();
        }

        if (crtStagePlayCoroutine != null) StopCoroutine(crtStagePlayCoroutine);
        crtStagePlayCoroutine = StartCoroutine(StagePlayCoroutine());
    }

    public void ResetPlay()
    {
        Debug.Log("ResetPlay");
        if (crtStagePlayCoroutine != null) StopCoroutine(crtStagePlayCoroutine);
        playing = false;
        foreach (var mpo in mimicPanelObjects)
        {
            mpo.StartAnim();
        }
    }

    IEnumerator StagePlayCoroutine()
    {
        yield return new WaitForSeconds(stageDuration);
        playing = false;
        _endCallback?.Invoke();
        _endCallback = null;
    }
}