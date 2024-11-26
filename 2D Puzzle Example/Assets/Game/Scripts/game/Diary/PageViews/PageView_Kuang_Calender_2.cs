
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using echo17.EndlessBook;
using Assets.Game.Scripts.game.Diary;
using Assets.Game.Scripts.game.Diary.Common;
using echo17.EndlessBook.Demo02;

public class PageView_Kuang_Calender_2 : PageView
{
    public CalenderItem[] calenderItems;
    public PageView_Kuang_Calender_1 view_Kuang_Calender_1;
    public override void Activate()
    {
        base.Activate();
    }
    public override void Deactivate()
    {
        base.Deactivate();
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
                view_Kuang_Calender_1.CheckLock();
                break;
            }
        }

        return clickedOnSomething;
    }
}