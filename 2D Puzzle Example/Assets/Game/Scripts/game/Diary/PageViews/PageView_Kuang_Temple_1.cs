
using UnityEngine;
using Assets.Game.Scripts.game.Diary;
using echo17.EndlessBook.Demo02;
using System.Collections;
using com;
using DG.Tweening;

public class PageView_Kuang_Temple_1 : PageView
{
    public Collider muyuCol;

    public Transform[] toReveals;
    private int nextIndex;

    public string sfxMuyu;
    public string sfxShow;
    public MimicPanelObject muyuAnim;
    public MimicPanelObject monkAnim;
    public GameObject prefabDong;
    public float dongSpawnOffset;

    public DiaryGameFlowSystem.DialogData[] dialogDatas_Left_开场;
    public DiaryGameFlowSystem.DialogData[] dialogDatas_Right_开场;

    public DiaryGameFlowSystem.DialogData[] dialogDatas_Left_乌龟;
    public DiaryGameFlowSystem.DialogData[] dialogDatas_Right_乌龟;

    public DiaryGameFlowSystem.DialogData[] dialogDatas_Left_收尾;
    public DiaryGameFlowSystem.DialogData[] dialogDatas_Right_收尾;

    public override void Activate()
    {
        base.Activate();
        CheckLock();
    }
    public override void Deactivate()
    {
        //Debug.Log("Deactivate");
        base.Deactivate();
    }

    public void CheckLock()
    {
        var allRevealed = nextIndex >= toReveals.Length;

        if (allRevealed)
        {
            DiaryGameSystem.instance.ToggleLockTurnPage(false);
        }
        else
        {
            DiaryGameSystem.instance.ToggleLockTurnPage(true);
        }
    }


    protected override bool HandleHit(RaycastHit hit, BookActionDelegate action)
    {
        var res = hit.collider == muyuCol;
        if (res)
            OnClickMuyu();
        return res;
    }

    void OnClickMuyu()
    {
        var dong = Instantiate(prefabDong, prefabDong.transform.position, prefabDong.transform.rotation);
        dong.transform.position += new Vector3(Random.Range(-dongSpawnOffset, dongSpawnOffset),
            Random.Range(-dongSpawnOffset, dongSpawnOffset) * 0.5f, 0);
        dong.SetActive(true);
        Destroy(dong.gameObject, 1);
        SoundSystem.instance.Play(sfxMuyu);

        if (_isMuyuBusy)
        {
            return;
        }

        SoundSystem.instance.Play(sfxShow,0.8f);

        StartCoroutine(MuyuCoroutine());
    }

    bool _isMuyuBusy;

    IEnumerator MuyuCoroutine()
    {
        _isMuyuBusy = true;
        var extraDelay = ShowNextItem();
        monkAnim.gameObject.SetActive(true);
        monkAnim.Init();
        monkAnim.StartAnim();

        muyuAnim.Init();
        muyuAnim.StartAnim();
        yield return new WaitForSeconds(1.1f);
        monkAnim.gameObject.SetActive(false);
        yield return new WaitForSeconds(extraDelay);
        _isMuyuBusy = false;
    }

    float ShowNextItem()
    {
        float extraDelay = 0;

        var allRevealed = nextIndex >= toReveals.Length;

        if (allRevealed)
        {
            CheckLock();
            return extraDelay;
        }

        var item = toReveals[nextIndex];
        item.gameObject.SetActive(true);
        var sr = item.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = new Color(1, 1, 1, 0);
            sr.DOFade(1, 0.4f);
        }
        var mpo = item.GetComponent<MimicPanelObject>();
        if (mpo != null)
        {
            mpo.Init();
            mpo.StartAnim();
            extraDelay = mpo.duration;
        }

        nextIndex++;
        return extraDelay;
    }

    public void ShowFriendTalkLeft_开场()
    {
        var datas = dialogDatas_Left_开场;
        if (datas != null && datas.Length > 0)
        {
            DiaryGameFlowSystem.instance.ShowFriendTalk(false, datas, 1.2f, 0.4f, ShowFriendTalkRight_开场);
            return;
        }

        ShowFriendTalkRight_开场();
    }

    void ShowFriendTalkRight_开场()
    {
        var datas = dialogDatas_Right_开场;
        if (datas != null && datas.Length > 0)
        {
            DiaryGameFlowSystem.instance.ShowFriendTalk(true, datas, 1.5f, 0.3f, null);
            return;
        }
    }

    public void ShowFriendTalkLeft_乌龟()
    {
        var datas = dialogDatas_Left_乌龟;
        if (datas != null && datas.Length > 0)
        {
            DiaryGameFlowSystem.instance.ShowFriendTalk(false, datas, 1.2f, 0.4f, ShowFriendTalkRight_乌龟);
            return;
        }

        ShowFriendTalkRight_乌龟();
    }

    void ShowFriendTalkRight_乌龟()
    {
        var datas = dialogDatas_Right_乌龟;
        if (datas != null && datas.Length > 0)
        {
            DiaryGameFlowSystem.instance.ShowFriendTalk(true, datas, 1.5f, 0.3f, null);
            return;
        }
    }

    public void ShowFriendTalkLeft_收尾()
    {
        var datas = dialogDatas_Left_收尾;
        if (datas != null && datas.Length > 0)
        {
            DiaryGameFlowSystem.instance.ShowFriendTalk(false, datas, 1.2f, 0.4f, ShowFriendTalkRight_收尾);
            return;
        }

        ShowFriendTalkRight_收尾();
    }

    void ShowFriendTalkRight_收尾()
    {
        var datas = dialogDatas_Right_收尾;
        if (datas != null && datas.Length > 0)
        {
            DiaryGameFlowSystem.instance.ShowFriendTalk(true, datas, 1.5f, 0.3f, null);
            return;
        }
    }
}