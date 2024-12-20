
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
    public Camera cam1TransTarget;
    public Camera cam2TransTarget;
    public Transform shovelView;

    public string sfxShovelSmall;
    public string sfxShovelBig;

    public Transform shovelAnim;
    public Transform shovelAnimTrans1;
    public Transform shovelAnimTrans2;

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
        shovelAnim.gameObject.SetActive(false);

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
        else if (Input.anyKeyDown)
        {
            SoundSystem.instance.Play("bo2");
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
            var qteView = viewQtes[i];
            if (i == nextPending - 1)
            {
                Debug.Log("qteView " + i);
                qteView.gameObject.SetActive(true);
                qteView.position = a.pos.position;
            }
            else
            {
                qteView.gameObject.SetActive(false);
            }
        }

        shovelView.gameObject.SetActive(true);
    }

    void CheckQte(int qte)
    {
        Debug.Log("CheckQte " + qte);
        var v = ValidateQteIndex();
        if (!v)
            return;


        var nextPending = qteArrays[qteArrayIndex].stageQte[qteArraySubIndex];
        Debug.Log(qte + " nextPending " + nextPending);
        if (qte == nextPending)
        {
            StartCoroutine(ProcessQteCoroutine());
        }
        else
        {
            SoundSystem.instance.Play("bo2");
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
                OnFinishQte();
                return false;
            }
            //return true;
        }

        return true;
    }


    bool _blockInput = false;

    IEnumerator ProcessQteCoroutine()
    {
        Debug.Log("ProcessQteCoroutine crt " + qteArrayIndex + "/" + qteArraySubIndex);
        _blockInput = true;

        var crtA = qteArrays[qteArrayIndex];
        qteArraySubIndex++;
        bool finishCurrentA = qteArraySubIndex == crtA.stageQte.Length;

        shovelView.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);

        shovelAnim.gameObject.SetActive(true);
        shovelAnim.DOKill();
        var t = 0.7f;
        shovelAnim.transform.parent.position = crtA.pos.position;
        shovelAnim.localScale = shovelAnimTrans1.localScale;
        shovelAnim.position = shovelAnimTrans1.position;
        shovelAnim.rotation = shovelAnimTrans1.rotation;
        shovelAnim.DOScale(shovelAnimTrans2.localScale, t);
        shovelAnim.DOMove(shovelAnimTrans2.position, t);
        shovelAnim.DORotate(shovelAnimTrans2.eulerAngles, t).OnComplete(
            () => { shovelAnim.gameObject.SetActive(false); }
            );

        if (finishCurrentA)
        {
            SoundSystem.instance.Play(sfxShovelBig);
            if (crtA.hideSr != null)
                crtA.hideSr.DOFade(0, 0.7f);
            if (crtA.showSr != null)
                crtA.showSr.DOFade(1, 1f).SetDelay(0.0f);
        }
        else
        {
            SoundSystem.instance.Play(sfxShovelSmall);
        }

        yield return new WaitForSeconds(0.5f);
        var v = ValidateQteIndex();
        if (v)
        {
            yield return new WaitForSeconds(0.35f);
            ShowPendingQte();
        }
        _blockInput = false;
    }

    void OnFinishQte()
    {
        qteFinished = true;
        Debug.Log("OnFinishQte");
        StartCoroutine(FinishQteCo()); ;
    }

    IEnumerator FinishQteCo()
    {
        SoundSystem.instance.Play("bling");
        var t = 2f;
        cam1.transform.DOMove(cam1TransTarget.transform.position, t);
        cam1.DOOrthoSize(cam1TransTarget.orthographicSize, t);
        cam2.transform.DOMove(cam2TransTarget.transform.position, t);
        cam2.DOOrthoSize(cam2TransTarget.orthographicSize, t);
        yield return new WaitForSeconds(t + 1.5f);
        DiaryGameFlowSystem.instance.StartFireworks();
    }
}