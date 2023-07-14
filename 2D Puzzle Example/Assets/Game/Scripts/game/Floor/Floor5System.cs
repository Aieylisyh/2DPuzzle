using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using com;

public class Floor5System : MonoBehaviour
{
    public static Floor5System instance;
    public Transform jisaw;
    public Transform jisawStartPos;
    public Transform jisawEndPos;

    public GameObject jisawScene;
    public GameObject[] words;

    bool _canEnterJisawScene;
    bool _canRevealLift;
    bool _hasRevealedLift;

    public Image liftImg;
    public Image doorRightImg;
    public Image doorLeftImg;
    public Image bgImg;
    public GameObject liftControlPanelButton;
    public GameObject liftButtons;
    public GameObject liftBlinker;
    public GameObject exitJisawViewBtn;
    public GameObject bigJisawView;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartPlay();
    }

    // Update is called once per frame
    void StartPlay()
    {
        liftImg.color = Color.black;
        liftControlPanelButton.SetActive(false);
        liftBlinker.SetActive(false);
        jisawScene.SetActive(false);
        liftButtons.SetActive(false);
        exitJisawViewBtn.SetActive(false);

        _canEnterJisawScene = false;
        _canRevealLift = false;
        _hasRevealedLift = false;

        var sequence = DOTween.Sequence(); //Sequence生成
        //Tweenをつなげる
        sequence.AppendCallback(() =>
           {
               jisaw.transform.DOKill();
               jisaw.position = jisawStartPos.position;
           })
            .AppendInterval(1.0f)
            .Append(bgImg.DOColor(Color.white, 1.5f))
            .Append(jisaw.DOMoveX(jisawEndPos.position.x, 7))
            .Join(jisaw.DOShakeRotation(7, 1, 18, 70, false))
            .AppendCallback(() =>
            {
                SoundSystem.instance.Play("trigger");
                _canEnterJisawScene = true;
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
        if (!_canEnterJisawScene)
            return;

        jisawScene.SetActive(true);
        _canEnterJisawScene = false;
        StartJiSawScene_SpeakWords();
    }

    public void StartJiSawScene_SpeakWords()
    {
        exitJisawViewBtn.SetActive(false);

        foreach (var w in words)
        {
            w.transform.DOKill();
            w.SetActive(false);
        }

        jisawScene.SetActive(true);
        bigJisawView.transform.DOKill();
        bigJisawView.transform.DOShakePosition(12, 4);

        var sequence = DOTween.Sequence();
        foreach (var w in words)
        {
            sequence.AppendInterval(0.25f);
            sequence.AppendCallback(() =>
            {
                w.SetActive(true);
                w.transform.DOKill();
                w.transform.DOShakePosition(2, 7);
                SoundSystem.instance.Play("show word");
            });
        }
        sequence.AppendCallback(() =>
        {
            _canRevealLift = true;
            //Debug.Log("canRevealLift");
            exitJisawViewBtn.SetActive(true);
        });

        sequence.Play();
    }
    public void OnClickExitJisawScene()
    {
        _canEnterJisawScene = true;
        foreach (var w in words)
            w.SetActive(false);
        SoundSystem.instance.Play("btn ok");
        jisawScene.SetActive(false);
        if (_canRevealLift && !_hasRevealedLift)
        {
            RevealList();
        }
    }

    public void RevealList()
    {
        doorRightImg.DOColor(Color.white, 4.0f);
        doorLeftImg.DOColor(Color.white, 4.0f);

        liftImg.DOColor(Color.white, 3.6f).OnComplete(() =>
        {
            //SoundSystem.instance.Play("ding");
            liftBlinker.SetActive(true);
            liftButtons.SetActive(true);
            liftControlPanelButton.SetActive(true);
        });
    }
}