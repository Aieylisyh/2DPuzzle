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
        cc.TurnTo(cc.ref_comedy, 3.5f);
        yield return new WaitForSeconds(3.5f);
        gameLogo.SetActive(true);

        gameLogo.transform.DORotate(Vector3.zero, 3.3f).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(3.6f);
        gameLogo.transform.DORotate(new Vector3(0, 270, 0), 2.5f).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(2.5f + 0.1f);
        gameLogo.SetActive(false);

        cc.TurnTo(cc.ref_default, 1.5f);
        yield return new WaitForSeconds(1.5f);
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