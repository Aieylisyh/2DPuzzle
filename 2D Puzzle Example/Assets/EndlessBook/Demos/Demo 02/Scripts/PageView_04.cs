namespace echo17.EndlessBook.Demo02
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Assets.Game.Scripts.game.Diary;

    public class PageView_04 : PageView
    {
        public DiaryViewItems diaryViewItems;

        public override void Activate()
        {
            base.Activate();
            CheckLock();
            diaryViewItems.Init();
            diaryViewItems.Play();
        }

        public override void Deactivate()
        {
            base.Deactivate();
            DiaryGameSystem.instance.ToggleLockTurnNextPage(false);
        }

        void CheckLock()
        {
            if (!diaryViewItems.completed)
            {
                DiaryGameSystem.instance.ToggleLockTurnNextPage(false);
            }
            else
            {
                DiaryGameSystem.instance.ToggleLockTurnNextPage(true);
            }
        }
    }
}