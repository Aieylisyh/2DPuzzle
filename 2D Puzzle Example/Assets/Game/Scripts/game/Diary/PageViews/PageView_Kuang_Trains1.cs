using Assets.EndlessBook.Demos.Demo_02.Scripts;
using Assets.Game.Scripts.game.Diary;
using com;
using echo17.EndlessBook.Demo02;
using System.Collections;
using UnityEngine;

public class PageView_Kuang_Trains1 : PageView
{
    public Transform mimicPanelObjectParent;
    public MimicPanelObject[] mimicPanelObjects { get; private set; }

    public override void Activate()
    {
        base.Activate();
        CheckLock();
        mimicPanelObjects = mimicPanelObjectParent.GetComponentsInChildren<MimicPanelObject>();
        foreach (var mpo in mimicPanelObjects)
            mpo.ResetAnim();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public void CheckLock()
    {
        var allPassed = false;

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
        foreach (var mpo in mimicPanelObjects)
        {
            mpo.ResetAnim();
            mpo.StartAnim();
        }
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