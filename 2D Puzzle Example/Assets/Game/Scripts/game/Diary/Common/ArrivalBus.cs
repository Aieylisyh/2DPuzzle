using com;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.Diary.Common
{
    public class ArrivalBus : MonoBehaviour
    {
        public MoveSinBehaviour msb;
        public Transform door;
        public float distance;
        public float distanceBack;
        public PageView_Kuang_Arrival_1 pageView_Kuang_Arrival_1;
        public void StartJourney(float delay)
        {
            msb.enabled = true;

            var x = transform.position.x;
            transform.DOMoveX(x - distanceBack, 2.4f).SetDelay(delay).SetEase(Ease.InOutCubic).OnComplete(Go); ;
        }

        void Go()
        {
            var x = transform.position.x;
            transform.DOMoveX(x + distance + distanceBack, 4.8f).SetEase(Ease.InQuad).OnComplete(
               () =>
               {
                   pageView_Kuang_Arrival_1.puzzleDone = true;
                   pageView_Kuang_Arrival_1.CheckLock();
                   pageView_Kuang_Arrival_1.SetSign(false, false, true);
               });
        }
    }
}