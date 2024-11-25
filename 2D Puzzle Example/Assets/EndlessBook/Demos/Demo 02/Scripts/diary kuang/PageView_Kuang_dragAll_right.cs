namespace echo17.EndlessBook.Demo02
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Assets.Game.Scripts.game.Diary;

    public class PageView_Kuang_dragAll_right : PageView
    {
        public DiaryViewItems diaryViewItems;

        public override void Activate()
        {
            base.Activate();

            if (diaryViewItems != null && diaryViewItems.items.Length > 0)
            {
                diaryViewItems.Init();
                diaryViewItems.Play();
            }
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }
    }
}