using Assets.EndlessBook.Demos.Demo_02.Scripts;
using Assets.Game.Scripts.game.Diary;
using echo17.EndlessBook.Demo02;
using System.Collections;
using UnityEngine;

public class PageView_Kuang_Trains1 : PageView
{
 public override void Activate()
    {
        base.Activate();
        CheckLock();
    }
    public override void Deactivate()
    {
        base.Deactivate();
        DiaryGameSystem.instance.ToggleLockTurnNextPage(false);
    }

    public void CheckLock()
    {
        var allPassed = false;

        if (allPassed)
        {
            DiaryGameSystem.instance.ToggleLockTurnNextPage(false);
        }
        else
        {
            DiaryGameSystem.instance.ToggleLockTurnNextPage(true);
        }
    }


    public override void TouchDown()
    {
        base.TouchDown();
        Debug.Log("TouchDown");
    }

    protected override bool HandleHit(RaycastHit hit, BookActionDelegate action)
    {
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