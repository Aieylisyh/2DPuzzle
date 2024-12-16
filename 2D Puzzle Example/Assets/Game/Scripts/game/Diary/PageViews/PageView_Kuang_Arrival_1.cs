
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Game.Scripts.game.Diary;
using Assets.Game.Scripts.game.Diary.Common;
using echo17.EndlessBook.Demo02;
using com;

public class PageView_Kuang_Arrival_1 : PageView
{
    public bool puzzleDone;

    public ArrivalMover arrivalMover;
    public ArrivalFollower[] afs;

    public GameObject sign1;
    public GameObject sign2;
    public GameObject sign3;
    public override void Activate()
    {
        base.Activate();
        puzzleDone = false;

        int i = 0;
        foreach (var af in afs)
        {
            var sp = af.GetComponent<SpriteRenderer>();
            sp.sortingOrder = 100 + (i++);
        }
        SetSign(true, false, false);
        CheckLock();
    }

    public void SetSign(bool b1, bool b2, bool b3)
    {
        sign1.SetActive(b1);
        sign2.SetActive(b2);
        sign3.SetActive(b3);
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public void CheckLock()
    {
        if (puzzleDone)
        {
            DiaryGameSystem.instance.ToggleLockTurnPage(false);
        }
        else
        {
            DiaryGameSystem.instance.ToggleLockTurnPage(true);
        }
    }

  
}