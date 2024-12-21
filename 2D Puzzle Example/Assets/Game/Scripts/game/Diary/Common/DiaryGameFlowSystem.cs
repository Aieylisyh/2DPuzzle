using Assets.Game.Scripts.game.Diary;
using com;
using DG.Tweening;
using echo17.EndlessBook.Demo02;
using System;
using System.Collections;
using TMPro;
using UnityEngine;


public class DiaryGameFlowSystem : MonoBehaviour
{
    public static DiaryGameFlowSystem instance;

    public GameObject gameLogo;

    public Light[] lights;

    public TextMeshProUGUI subtitle;

    public bool isTalking { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        subtitle.gameObject.SetActive(false);
        //DiaryGameFlowSystem.instance.StartFireworks();
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        foreach (var l in lights)
        {
            l.intensity *= 0.2f;
        }
        gameLogo.SetActive(false);
        yield return new WaitForSeconds(0.7f);


        if (!DiaryGameSystem.instance.skipLogo)
        {
            var cc = DiaryGameSystem.instance.cameraController;
            float showLogoCamMoveTime = 3.2f;
            cc.TurnTo(cc.ref_comedy, showLogoCamMoveTime);
            yield return new WaitForSeconds(showLogoCamMoveTime * 0.88f);
            gameLogo.SetActive(true);

            float t1 = 1.0f;
            gameLogo.transform.DORotate(Vector3.zero, t1).SetEase(Ease.InOutCubic);
            yield return new WaitForSeconds(t1 + 1.5f);
            foreach (var l in lights)
            {
                l.DOIntensity(l.intensity * 5, 2f);
            }
            yield return new WaitForSeconds(3 - 1.5f);


            float t2 = 0.7f;
            gameLogo.transform.DORotate(new Vector3(0, 270, 0), t2).SetEase(Ease.InOutCubic);
            yield return new WaitForSeconds(t2);
            gameLogo.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            float backTime = 2.8f;
            cc.TurnTo(cc.ref_default, backTime);
            yield return new WaitForSeconds(backTime);
        }

        DiaryGameSystem.instance.ToggleLockTurnPage(false);
    }

    public void ShowFriendTalk(bool rightOrLeft, DialogData[] dialogs, float endExtraDuration, float startDelay, Action callback)
    {
        if (DiaryGameSystem.instance.skipDialogs)
        {
            callback?.Invoke();
            return;
        }

        if (DiaryGameSystem.instance.fastDialogs)
            StartCoroutine(ShowFriendTalkCoroutine(rightOrLeft, dialogs, 0, 0, callback));
        else
            StartCoroutine(ShowFriendTalkCoroutine(rightOrLeft, dialogs, endExtraDuration, startDelay, callback));
    }

    IEnumerator ShowFriendTalkCoroutine(bool rightOrLeft, DialogData[] dialogs, float endExtraDuration, float startDelay, Action callback)
    {
        isTalking = true;
        yield return new WaitForSeconds(startDelay);
        var cc = DiaryGameSystem.instance.cameraController;
        cc.TurnTo(0, rightOrLeft ? 1 : -1, false);

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
                yield return new WaitForSeconds(d.time + 0.4f);

            subtitle.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(endExtraDuration);
        cc.TurnTo(0, 0, true);
        yield return new WaitForSeconds(cc.duration_short);

        isTalking = false;
        callback?.Invoke();
    }

    public void SimpleSubtitle(string s, float d)
    {
        StartCoroutine(SimpleSubtitleCo(s, d));
    }

    IEnumerator SimpleSubtitleCo(string s, float d)
    {
        subtitle.gameObject.SetActive(true);
        subtitle.text = s;
        yield return new WaitForSeconds(d);
        subtitle.gameObject.SetActive(false);
    }
    public void StartFireworks()
    {
        StartCoroutine(FireworksCoroutine());
    }

    public GameObject fireworksFirst;
    public ParticleSystem[] fireworks;
    public CameraFilterPack_Glow_Glow_Color filter;

    IEnumerator FireworksCoroutine()
    {
        gameLogo.SetActive(false);
        yield return new WaitForSeconds(1f);
        DiaryGameFlowSystem.instance.ShowFriendTalk(false, endDialogDatas, 1.5f, 0.4f,
            () => { StartCoroutine(PostFireworksCo()); });
    }

    IEnumerator PostFireworksCo()
    {
        DiaryGameSystem.instance.ToggleLockTurnPage(false);
        yield return new WaitForSeconds(0.5f);
        demo02.Ex_OpenBack();
        yield return new WaitForSeconds(2f);
        demo02.Ex_CloseBack();
        yield return new WaitForSeconds(0.35f);
        var cc = DiaryGameSystem.instance.cameraController;
        float showLogoCamMoveTime = 3.2f;
        cc.TurnTo(cc.ref_comedy, showLogoCamMoveTime);

        var tt = 0.5f;
        yield return new WaitForSeconds(showLogoCamMoveTime - tt);

        PlayFireworkSound();
        fireworksFirst.SetActive(true);

        yield return new WaitForSeconds(2f + tt);
        foreach (var f in fireworks)
        {
            StartCoroutine(FireworksCo(f));
        }
        foreach (var l in lights)
        {
            l.DOIntensity(0.1f, 0.2f);
        }
        yield return new WaitForSeconds(0.2f);

        filter.enabled = true;
        RenderSettings.fogColor = Color.black;
        RenderSettings.fogStartDistance = 2.5f;
        RenderSettings.fogEndDistance = 8.5f;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fog = true;
    }

    public DialogData[] endDialogDatas;
    public echo17.EndlessBook.Demo02.Demo02 demo02;

    IEnumerator FireworksCo(ParticleSystem ps)
    {
        while (true)
        {
            ps.Play();
            PlayFireworkSound();
            yield return new WaitForSeconds(UnityEngine.Random.Range(1, 3));
        }
    }

    public void PlayFireworkSound()
    {
        SoundSystem.instance.Play("firework");
    }

    [System.Serializable]
    public class DialogData
    {
        public string soundId;
        public string text;
        public float time;
    }
}