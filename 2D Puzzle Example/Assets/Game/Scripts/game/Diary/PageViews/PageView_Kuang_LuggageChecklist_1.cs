using Assets.EndlessBook.Demos.Demo_02.Scripts;
using Assets.Game.Scripts.game.Diary;
using echo17.EndlessBook.Demo02;
using System.Collections;
using UnityEngine;


public class PageView_Kuang_LuggageChecklist_1 : PageView
{
    public DiaryDraggable[] diaryDraggables;
    public DiaryBagCheckListItem[] checkListItems;
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
        var allChecked = true;
        foreach (var c in checkListItems)
        {
            if (!c.isChecked)
            {
                allChecked = false;
                break;
            }
        }

        if (allChecked)
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
        //Debug.Log("TouchDown");
    }

    protected override bool HandleHit(RaycastHit hit, BookActionDelegate action)
    {
        foreach (var d in diaryDraggables)
            d.EndDrag();

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

        foreach (var d in diaryDraggables)
            d.EndDrag();

        RaycastHit hit;
        if (Physics.Raycast(pageViewCamera.ViewportPointToRay(hitPointNormalized), out hit, maxRayCastDistance, raycastLayerMask))
        {
            Debug.Log(hit.collider.gameObject);
            foreach (var d in diaryDraggables)
            {
                if (hit.collider.gameObject == d.gameObject)
                {
                    d.StartDrag();
                }
            }
            return true;
        }

        return false;
    }

    public override void Drag(Vector2 increment, bool useInertia)
    {
        foreach (var d in diaryDraggables)
            d.OnDrag(increment);
    }

    public override void HandleTouchUp(Vector2 hitPointNormalized)
    {
        foreach (var d in diaryDraggables)
            d.EndDrag();
    }
}