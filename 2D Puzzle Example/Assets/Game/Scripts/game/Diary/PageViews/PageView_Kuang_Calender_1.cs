
using UnityEngine;
using Assets.Game.Scripts.game.Diary;
using Assets.Game.Scripts.game.Diary.Common;
using echo17.EndlessBook.Demo02;

public class PageView_Kuang_Calender_1 : PageView
{
    public CalenderItem[] calenderItems;
    public PageView_Kuang_Calender_2 view_Kuang_Calender_2;

    public override void Activate()
    {
        base.Activate();
        CheckLock();
    }
    public override void Deactivate()
    {
        Debug.Log("Deactivate");
        base.Deactivate();
    }

    public void CheckLock()
    {
        var allRevealed = true;
        foreach (var ci in calenderItems)
        {
            if (!ci.revealed)
            {
                allRevealed = false;
                break;
            }
        }
        foreach (var ci in view_Kuang_Calender_2.calenderItems)
        {
            if (!ci.revealed)
            {
                allRevealed = false;
                break;
            }
        }

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
        var clickedOnSomething = false;
        //Debug.Log("HandleHit " + hit.collider.gameObject);
        foreach (var ci in calenderItems)
        {
            if (ci.OnClick(hit.collider.gameObject))
            {
                clickedOnSomething = true;
                CheckLock();
                break;
            }
        }

        return clickedOnSomething;
    }
}