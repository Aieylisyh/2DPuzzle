using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Floor5CinematicSystem : MonoBehaviour
{
    public Transform jisaw;
    public Transform jisawStartPos;
    public Transform jisawEndPos;

    public GameObject jisawScene;
    public GameObject[] words;
    public bool canEnterJisawScene;

    // Use this for initialization
    void Start()
    {
        StartPlay();
    }

    // Update is called once per frame
    void StartPlay()
    {
        jisawScene.SetActive(false);

        var sequence = DOTween.Sequence(); //Sequence生成
                                           //Tweenをつなげる
        sequence.AppendCallback(() =>
           {
               jisaw.transform.DOKill();
               jisaw.position = jisawStartPos.position;
               canEnterJisawScene = false;
           })
            .Append(jisaw.DOMoveX(jisawEndPos.position.x, 7).SetEase(Ease.OutQuad))
            .Join(jisaw.DOShakeRotation(7, 1, 18, 70, false))
            .AppendCallback(() =>
            {
                canEnterJisawScene = true;
            })
            .Play();

        /* sequence.Append()
                 .Append(transform.DOMoveX(-7, 1))
                 .Append(transform.DOMoveX(0, 1))
                 //Joinで並列動作するTweenを追加
                 .Join(transform.DORotate(new Vector3(0, 180), 1.5f, RotateMode.WorldAxisAdd)) //0 ~ 1.5
                 .AppendInterval(0.25f)
                 .AppendCallback(() =>
                 {
                     targetRenderer.material.color = Color.cyan;
                 })
                 //前に追加する
                 sequence.PrependInterval(0.5f);
                 sequence.PrependCallback(() => { targetRenderer.material.color = Color.red; });
                 sequence.Prepend(transform.DORotate(new Vector3(0, 0, 180), 1, RotateMode.WorldAxisAdd));

         sequence.Play();
        */
    }

    public void OnClickJiSawScene()
    {
        if (!canEnterJisawScene)
            return;

        jisawScene.SetActive(true);
        canEnterJisawScene = false;
        StartJiSawScene_SpeakWords();
    }

    public void StartJiSawScene_SpeakWords()
    {
        foreach (var w in words)
            w.SetActive(false);
        jisawScene.SetActive(true);

        var sequence = DOTween.Sequence();
        foreach (var w in words)
        {
            sequence.AppendInterval(0.3f);
            sequence.AppendCallback(() =>
            {
                w.SetActive(true);
                w.transform.DOKill();
                w.transform.DOShakePosition(2, 5);
            });
        }

        sequence.Play();
    }
    public void OnClickExitJisawScene()
    {
        canEnterJisawScene = true;
        foreach (var w in words)
            w.SetActive(false);
        jisawScene.SetActive(false);
    }
}