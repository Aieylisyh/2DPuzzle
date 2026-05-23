using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.Omni._3d
{
    public class Door : MonoBehaviour
    {

        public Transform openTrans;
        public Transform closeTrans;
        public Transform targetTrans;
        public float duration;

        private void OnTriggerEnter(Collider other)
        {
            var playerInteractor = other.GetComponent<PlayerInteractor>();
            if (playerInteractor != null)
            {
                playerInteractor.crtDoor = this;
                UiBehaviour.instance.ToggleDoorTip(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var playerInteractor = other.GetComponent<PlayerInteractor>();
            if (playerInteractor != null && playerInteractor.crtDoor == this)
            {
                playerInteractor.crtDoor = null;
                UiBehaviour.instance.ToggleDoorTip(false);
            }
        }

        public void Open()
        {
            targetTrans.DOKill();
            targetTrans.DOMove(openTrans.position, duration).SetEase(Ease.OutBounce);
            targetTrans.DORotate(openTrans.eulerAngles, duration).SetEase(Ease.OutBounce);
        }

        public void Close()
        {
            targetTrans.DOKill();
            targetTrans.DOMove(closeTrans.position, duration).SetEase(Ease.OutBounce);
            targetTrans.DORotate(closeTrans.eulerAngles, duration).SetEase(Ease.OutBounce);
        }

        void OnDestroy()
        {
            if (targetTrans != null)
                targetTrans.DOKill();
        }
    }
}