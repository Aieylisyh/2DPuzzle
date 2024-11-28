using Assets.Game.Scripts.game.Diary;
using com;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;


public class DiaryGameFlowSystem : MonoBehaviour
{
    public static DiaryGameFlowSystem instance;

    public GameObject gameLogo;
    public GameObject fireworks;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        gameLogo.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        var cc = DiaryGameSystem.instance.cameraController;
        cc.TurnTo(cc.ref_comedy, 3.0f);
        yield return new WaitForSeconds(2.8f);
        gameLogo.SetActive(true);

        float t1 = 1.2f;
        gameLogo.transform.DORotate(Vector3.zero, t1).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(t1 + 2.8f);
        float t2 = 1.2f;
        gameLogo.transform.DORotate(new Vector3(0, 270, 0), t2).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(t2);
        gameLogo.SetActive(false);

        cc.TurnTo(cc.ref_default, 1.5f);
        //yield return new WaitForSeconds(1.2f);
    }

    public void ShowFriendTalk(bool rightOrLeft, string[] soundIds, float extraDuration, float interval, float delay, Action callback)
    {
        StartCoroutine(ShowFriendTalkCoroutine(rightOrLeft, soundIds, extraDuration, interval, delay, callback));
    }

    IEnumerator ShowFriendTalkCoroutine(bool rightOrLeft, string[] soundIds, float extraDuration, float interval, float delay, Action callback)
    {
        yield return new WaitForSeconds(delay);
        var cc = DiaryGameSystem.instance.cameraController;
        cc.TurnTo(0, rightOrLeft ? 1 : -1, true);
        yield return new WaitForSeconds(cc.duration_long);

        foreach (var s in soundIds)
        {
            SoundSystem.instance.Play(s);
            yield return new WaitForSeconds(interval);
        }

        yield return new WaitForSeconds(extraDuration);
        cc.TurnTo(0, 0, false);
        yield return new WaitForSeconds(cc.duration_short);

        callback?.Invoke();
    }
}