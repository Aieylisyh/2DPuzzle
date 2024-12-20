
using UnityEngine;
using Assets.Game.Scripts.game.Diary;
using echo17.EndlessBook.Demo02;
using System.Collections;
using com;
using DG.Tweening;

public class PageView_Kuang_Beach_1 : PageView
{
    [System.Serializable]
    public class QteArray
    {
        public Transform pos;
        public SpriteRenderer showSr;
        public SpriteRenderer hideSr;
        public int[] stageQte;//d 1 i 2 g 3
    }

    public QteArray[] qteArrays;

    public Camera cam1;
    public Camera cam2;
    public Transform cam1TransTarget;
    public Transform cam2TransTarget;
    public Transform shovelView;

    public string sfxShovelSmall;
    public string sfxShovelBig;

    public MimicPanelObject shovelAnim;

    public bool qteFinished;
    private int qteArrayIndex;
    private int qteArraySubIndex;

    public Transform[] viewQtes;

    public override void Activate()
    {
        base.Activate();
        CheckLock();
        qteArrayIndex = 0;
        qteArraySubIndex = 0;
        foreach (var a in qteArrays)
        {
            a.showSr.color = new Color(1, 1, 1, 0);
        }
        shovelView.gameObject.SetActive(false);
        ShowPendingQte();
    }

    public override void Deactivate()
    {
        //Debug.Log("Deactivate");
        base.Deactivate();
    }

    public void CheckLock()
    {
        if (false)
        {
            DiaryGameSystem.instance.ToggleLockTurnPage(false);
        }
        else
        {
            DiaryGameSystem.instance.ToggleLockTurnPage(true);
        }
    }

    private void Update()
    {
        if (qteFinished)
            return;
        if (_blockInput)
            return;

        if (Input.GetKeyDown("d"))
        {
            CheckQte(1);
        }
        else if (Input.GetKeyDown("i"))
        {
            CheckQte(2);
        }
        else if (Input.GetKeyDown("g"))
        {
            CheckQte(3);
        }
    }

    void ShowPendingQte()
    {
        var v = ValidateQteIndex();
        if (!v)
            return;

        var a = qteArrays[qteArrayIndex];
        var nextPending = a.stageQte[qteArraySubIndex];
        for (int i = 0; i < viewQtes.Length; i++)
        {
            if (i == nextPending - 1)
            {
                viewQtes[i].gameObject.SetActive(true);
                viewQtes[i].position = a.pos.position;
            }
            else
            {
                viewQtes[i].gameObject.SetActive(false);
            }
        }

        shovelView.gameObject.SetActive(true);
    }

    void CheckQte(int qte)
    {
        var v = ValidateQteIndex();
        if (!v)
            return;


        var nextPending = qteArrays[qteArrayIndex].stageQte[qteArraySubIndex];
        if (qte == nextPending)
        {
            ProcessQteCoroutine();
        }
    }

    bool ValidateQteIndex()
    {
        var len = qteArrays.Length;
        var a = qteArrays[qteArrayIndex];
        var lenSub = a.stageQte.Length;
        if (qteArraySubIndex >= lenSub)
        {
            qteArraySubIndex = 0;
            qteArrayIndex++;
            if (qteArrayIndex >= len)
            {
                qteFinished = true;
                OnFinishQte();
                return false;
            }
            return false;
        }

        return true;
    }


    bool _blockInput = false;

    IEnumerator ProcessQteCoroutine()
    {
        Debug.Log("ProcessQteCoroutine " + qteArrayIndex + "/" + qteArraySubIndex);
        _blockInput = true;

        var crtA = qteArrays[qteArrayIndex];
        qteArraySubIndex++;
        bool finishCurrentA = qteArraySubIndex == crtA.stageQte.Length;

        shovelView.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        shovelAnim.gameObject.SetActive(true);
        shovelAnim.Init();
        shovelAnim.StartAnim();
        shovelAnim.transform.parent.position = crtA.pos.position;

        if (finishCurrentA)
        {
            SoundSystem.instance.Play(sfxShovelBig);
            crtA.hideSr.DOFade(0, 0.7f);
            crtA.showSr.DOFade(1, 0.6f).SetDelay(0.3f);
        }
        else
        {
            SoundSystem.instance.Play(sfxShovelSmall);
        }

        yield return new WaitForSeconds(0.5f);
        var v = ValidateQteIndex();
        if (v)
        {
            yield return new WaitForSeconds(1);
            ShowPendingQte();
        }
        _blockInput = false;
    }

    void OnFinishQte()
    {
        Debug.Log("OnFinishQte");
        DiaryGameFlowSystem.instance.StartFireworks();
    }
}