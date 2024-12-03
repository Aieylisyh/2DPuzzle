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

        public void StartJourney(float delay)
        {
            msb.enabled = true;

            var x = transform.position.x;
            transform.DOMoveX(x + distance, 5).SetDelay(delay).SetEase(Ease.InCubic);
        }
    }
}