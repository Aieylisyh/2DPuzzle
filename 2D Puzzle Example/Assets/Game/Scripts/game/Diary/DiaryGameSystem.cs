using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.Diary
{
    public class DiaryGameSystem : MonoBehaviour
    {
        public static DiaryGameSystem instance;
        public DiaryCameraController cameraController;

        public Transform lockNextTransEnd;
        public Transform lockNextTransStart;
        public Transform lockNextGo;

        public bool canTurnToNextPage;
        public bool canTurnToLastPage;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            lockNextGo.transform.position = lockNextTransStart.position;
        }

        public void ToggleLockTurnNextPage(bool b)
        {
            lockNextGo.transform.DOKill();
            canTurnToNextPage = !b;
            lockNextGo.transform.DOMove(b ? lockNextTransEnd.position : lockNextTransStart.position, 1);
        }

        public bool testLockNext;
        public bool testUnlockNext;

        private void Update()
        {
            if (testLockNext)
            {
                testLockNext = false;
                ToggleLockTurnNextPage(true);
            }

            if (testUnlockNext)
            {
                testUnlockNext = false;
                ToggleLockTurnNextPage(false);
            }
        }
    }
}