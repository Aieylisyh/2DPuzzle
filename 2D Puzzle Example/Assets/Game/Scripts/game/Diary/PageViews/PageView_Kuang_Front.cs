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
            var datas = new DiaryGameFlowSystem.DialogData[2];
            datas[0] = new DiaryGameFlowSystem.DialogData();
            datas[0].soundId = "animalese 1";
            datas[0].text = "Alice: I love my house I like my socks I hate eggs.";
            datas[0].time = 1.0f;
            datas[1] = new DiaryGameFlowSystem.DialogData();
            datas[1].soundId = "animalese 2";
            datas[1].text = "Jack: I made my biggest mistake by creating my company.";
            datas[1].time = 1.0f;
            DiaryGameFlowSystem.instance.ShowFriendTalk(false, datas, 1.2f, 1.2f, ShowFriendTalkRight);
        }

        void ShowFriendTalkRight()
        {
            var datas = new DiaryGameFlowSystem.DialogData[2];
            datas[0] = new DiaryGameFlowSystem.DialogData();
            datas[0].soundId = "animalese 3";
            datas[0].text = "Kaka: I love my house I like my socks I hate eggs.";
            datas[0].time = 1.0f;
            datas[1] = new DiaryGameFlowSystem.DialogData();
            datas[1].soundId = "animalese 4";
            datas[1].text = "Kaku: I made my biggest mistake by creating my company.";
            datas[1].time = 1.0f;

            DiaryGameFlowSystem.instance.ShowFriendTalk(true, datas, 1.6f, 0.2f, TalkEnd);
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