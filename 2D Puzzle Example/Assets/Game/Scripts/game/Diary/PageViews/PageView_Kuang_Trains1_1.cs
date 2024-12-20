using Assets.EndlessBook.Demos.Demo_02.Scripts;
using Assets.Game.Scripts.game.Diary;
using com;
using echo17.EndlessBook.Demo02;
using UnityEngine;
public class PageView_Kuang_Trains1_1 : PageView
{
    public TinyStageOfMimicPanels tinyStage;

    bool friendTalksDone = false;

    public DiaryGameFlowSystem.DialogData[] dialogDatas_Left;
    public DiaryGameFlowSystem.DialogData[] dialogDatas_Right;
    bool _activated = false;

    public override void Activate()
    {
        Debug.Log(gameObject.name + " Activate1");
        base.Activate();
        if (_activated)
            return;

        Debug.Log(gameObject.name + " Activate2");
        _activated = true;
        tinyStage.Init();
        tinyStage.gameObject.SetActive(true);
        CheckLock();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public void CheckLock()
    {
        if (friendTalksDone)
        {
            DiaryGameSystem.instance.ToggleLockTurnPage(false);
        }
        else
        {
            DiaryGameSystem.instance.ToggleLockTurnPage(true);
        }
    }


    public override void TouchDown()
    {
        base.TouchDown();
        //Debug.Log("TouchDown");//earlier than handleHit

        OnTap();
    }

    public void OnTap()
    {
        if (tinyStage.playing)
            return;
        if (DiaryGameFlowSystem.instance.isTalking)
            return;
        if (friendTalksDone)
            return;
        SoundSystem.instance.Play("s1");
        tinyStage.StartPlay(ShowFriendTalkLeft);
    }

    void ShowFriendTalkLeft()
    {
        var datas = dialogDatas_Left;
        if (datas != null && datas.Length > 0)
        {
            DiaryGameFlowSystem.instance.ShowFriendTalk(false, datas, 1.2f, 0.4f, ShowFriendTalkRight);
            return;
        }

        ShowFriendTalkRight();
    }

    void ShowFriendTalkRight()
    {
        var datas = dialogDatas_Right;
        if (datas != null && datas.Length > 0)
        {
            DiaryGameFlowSystem.instance.ShowFriendTalk(true, datas, 1.5f, 0.3f, TalkEnd);
            return;
        }

        TalkEnd();
    }

    void TalkEnd()
    {
        friendTalksDone = true;
        CheckLock();
    }

    protected override bool HandleHit(RaycastHit hit, BookActionDelegate action)
    {
        //Debug.Log("HandleHit");
        var blockClickOnClickOnItems = false;
        if (blockClickOnClickOnItems)
            return true;

        return false;
    }

    public override bool HandleTouchDown(Vector2 hitPointNormalized)
    {
        if (pageViewCamera == null) return false;

        RaycastHit hit;
        if (Physics.Raycast(pageViewCamera.ViewportPointToRay(hitPointNormalized), out hit, maxRayCastDistance, raycastLayerMask))
        {
            //Debug.Log(hit.collider.gameObject);
            return true;
        }

        return false;
    }

    public void PlayTrainStationSfx()
    {
        SoundSystem.instance.Play("train station");
    }
}