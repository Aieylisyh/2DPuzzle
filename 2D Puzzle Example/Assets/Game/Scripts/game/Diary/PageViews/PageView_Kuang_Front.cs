using echo17.EndlessBook.Demo02;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.Diary.PageViews
{
    public class PageView_Kuang_Front : PageView
    {
        public DiaryGameFlowSystem.DialogData[] dialogDataLeft;
        public DiaryGameFlowSystem.DialogData[] dialogDataRight;

        bool friendTalksDone = false;

        public override void Activate()
        {
            base.Activate();

            if (!friendTalksDone)
            {
                ShowFriendTalkLeft();
                return;
            }

            CheckLock();
        }

        void ShowFriendTalkLeft()
        {
            DiaryGameFlowSystem.instance.ShowFriendTalk(true, dialogDataLeft, 1.2f, 1.2f, ShowFriendTalkRight);
        }

        void ShowFriendTalkRight()
        {
            DiaryGameFlowSystem.instance.ShowFriendTalk(false, dialogDataRight, 1.6f, 0.4f, TalkEnd);
        }

        void TalkEnd()
        {
            friendTalksDone = true;
            CheckLock();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }

        void CheckLock()
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
    }
}