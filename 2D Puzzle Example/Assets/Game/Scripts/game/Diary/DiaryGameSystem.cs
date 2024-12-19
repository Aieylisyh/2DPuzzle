using com;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.Diary
{
    public class DiaryGameSystem : MonoBehaviour
    {
        public static DiaryGameSystem instance;
        public DiaryCameraController cameraController;

        //public Transform lockNextTransEnd;
        // public Transform lockNextTransStart;
        //public Transform lockNextGo;
        public Transform lock_enterStart;
        public Transform lock_end;
        public Transform lock_leaveEnd;
        public Transform locker;

        public bool canTurnPage;

        public MimicPanelObject[] startingMpos;


        private void Awake()
        {
            canTurnPage = true;
            instance = this;
        }

        private void Start()
        {
            locker.position = lock_enterStart.position;
            locker.rotation = lock_enterStart.rotation;
            ToggleLockTurnPage(true);
            foreach(var mpo in startingMpos)
            {
                mpo.Init();
                mpo.StartAnim();
            }
        }

        public void ToggleLockTurnPage(bool b)
        {
            if (neverLock)
            {
                canTurnPage = true;
                locker.position = lock_enterStart.position;
                locker.rotation = lock_enterStart.rotation;
                return;
            }


            Debug.Log("ToggleLockTurnPage " + b);
            if (canTurnPage == !b)
                return;

            locker.DOKill();
            if (canTurnPage)
            {
                //lock
                locker.position = lock_enterStart.position;
                locker.rotation = lock_enterStart.rotation;
                locker.DOMove(lock_end.position, 1.5f).SetEase(Ease.OutCubic);
                locker.DORotate(lock_end.eulerAngles, 1.5f).SetEase(Ease.InOutCubic);
            }
            else
            {
                //unlock
                SoundSystem.instance.Play("bling",0.5f);
                locker.DOMove(lock_leaveEnd.position, 1.5f).SetEase(Ease.InCubic);
                locker.DORotate(lock_leaveEnd.eulerAngles, 1.5f).SetEase(Ease.Linear);
            }

            canTurnPage = !b;
        }

        public bool testLockNext;
        public bool testUnlockNext;
        public bool neverLock;
        public bool skipDialogs;
        public bool skipLogo;
        public bool fastDialogs;
        private void Update()
        {
            if (testLockNext)
            {
                testLockNext = false;
                ToggleLockTurnPage(true);
            }

            if (testUnlockNext)
            {
                testUnlockNext = false;
                ToggleLockTurnPage(false);
            }

            if (Input.GetKeyDown("p"))
            {
                ToggleLockTurnPage(false);
            }

            if (Input.GetKeyDown("o"))
            {
                ToggleLockTurnPage(true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                SoundSystem.instance.Play("click",0.4f);
            }
        }
    }
}