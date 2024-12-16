using Assets.Game.Scripts.game.Diary;
using com;
using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class DiaryGameFlowSystem : MonoBehaviour
{
    public static DiaryGameFlowSystem instance;

    public GameObject gameLogo;
    public GameObject fireworks;

    public TextMeshProUGUI subtitle;

    public bool isTalking { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        subtitle.gameObject.SetActive(false);
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        gameLogo.SetActive(false);
        yield return new WaitForSeconds(0.5f);


        if (!DiaryGameSystem.instance.skipLogo)
        {
            DiaryGameSystem.instance.ToggleLockTurnPage(true);
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
            yield return new WaitForSeconds(1.2f);
        }

        DiaryGameSystem.instance.ToggleLockTurnPage(false);
    }

    public void ShowFriendTalk(bool rightOrLeft, DialogData[] dialogs, float extraDuration, float delay, Action callback)
    {
        if (DiaryGameSystem.instance.skipDialogs)
        {
            callback?.Invoke();
            return;
        }

        if (DiaryGameSystem.instance.fastDialogs)
            StartCoroutine(ShowFriendTalkCoroutine(rightOrLeft, dialogs, 0, 0, callback));
        else
            StartCoroutine(ShowFriendTalkCoroutine(rightOrLeft, dialogs, extraDuration, delay, callback));
    }

    IEnumerator ShowFriendTalkCoroutine(bool rightOrLeft, DialogData[] dialogs, float extraDuration, float delay, Action callback)
    {
        isTalking = true;
        yield return new WaitForSeconds(delay);
        var cc = DiaryGameSystem.instance.cameraController;
        cc.TurnTo(0, rightOrLeft ? 1 : -1, true);

        if (DiaryGameSystem.instance.fastDialogs)
            yield return new WaitForSeconds(0.1f);
        else
            yield return new WaitForSeconds(cc.duration_long);

        foreach (var d in dialogs)
        {
            yield return new WaitForSeconds(0.1f);
            SoundSystem.instance.Play(d.soundId);
            subtitle.gameObject.SetActive(true);
            subtitle.text = d.text;

            if (DiaryGameSystem.instance.fastDialogs)
                yield return new WaitForSeconds(0.1f);
            else
                yield return new WaitForSeconds(d.time);

            subtitle.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(extraDuration);
        cc.TurnTo(0, 0, false);
        yield return new WaitForSeconds(cc.duration_short);

        isTalking = false;
        callback?.Invoke();
    }

    [System.Serializable]
    public class DialogData
    {
        public string soundId;
        public string text;
        public float time;
    }
}