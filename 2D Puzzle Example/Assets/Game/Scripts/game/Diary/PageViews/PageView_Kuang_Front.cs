using echo17.EndlessBook.Demo02;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.Diary.PageViews
{
    public class PageView_Kuang_Front : PageView
    {
        bool friendTalksDone = false;
        public override void Activate()
        {
            base.Activate();
            CheckLock();
            if (!friendTalksDone)
            {
                ShowFriendTalkLeft();
            }
        }

        void ShowFriendTalkLeft()
        {
            DiaryGameFlowSystem.instance.ShowFriendTalk(
                false, new string[2] { "animalese 1", "animalese 2" },
                0.9f, 1.2f, 1.2f, ShowFriendTalkRight);
        }

        void ShowFriendTalkRight()
        {
            DiaryGameFlowSystem.instance.ShowFriendTalk(
               true, new string[2] { "animalese 3", "animalese 4" },
               0.9f, 1.6f, 0.2f, TalkEnd);
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