using Assets.EndlessBook.Demos.Demo_02.Scripts;
using Assets.Game.Scripts.game.Diary;
using com;
using echo17.EndlessBook.Demo02;
using System.Collections;
using UnityEngine;

public class PageView_Kuang_Trains1 : PageView
{
    public TinyStageOfMimicPanels[] tinyStages;

    private int _tinyStageIndex;
    public override void Activate()
    {
        base.Activate();
        _tinyStageIndex = -1;
        foreach (var ts in tinyStages)
        {
            ts.Init();
            ts.gameObject.SetActive(false);
        }

        CheckLock();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public void CheckLock()
    {
        var allPassed = _tinyStageIndex >= tinyStages.Length;

        if (allPassed)
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
        Debug.Log("TouchDown");//earlier than handleHit

        OnTap();
    }

    public void OnTap()
    {
        SoundSystem.instance.Play("done");
        if (_tinyStageIndex < 0)
            _tinyStageIndex = 0;

        if (_tinyStageIndex >= tinyStages.Length)
        {
            CheckLock();
            return;
        }

        foreach (var ts in tinyStages)
        {
            if (ts.playing)
                return;
        }

        var crtStage = tinyStages[_tinyStageIndex];
        foreach (var ts in tinyStages)
        {
            ts.gameObject.SetActive(ts == crtStage);
        }

        crtStage.StartPlay();
        _tinyStageIndex++;
    }

    protected override bool HandleHit(RaycastHit hit, BookActionDelegate action)
    {
        Debug.Log("HandleHit");
        var blockClickOnClickOnItems = false;
        if (blockClickOnClickOnItems)
        {
            return true;
        }
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
}