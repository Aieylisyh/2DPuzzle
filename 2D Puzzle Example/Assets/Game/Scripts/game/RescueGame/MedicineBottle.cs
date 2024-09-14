using com;
using DG.Tweening;
using Rescue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Game.Scripts.game.RescueGame
{
    public class MedicineBottle : DragDropTarget
    {
        [SerializeField] DragDropContainer mouthContainer;
        [SerializeField] DragDropContainer fromContainer;
        public override void OnEndDrag(PointerEventData eventData)
        {
            if (freeDrag)
            {
                GetComponent<Image>().raycastTarget = true;
                return;
            }

            DragDropContainer endContainer = null;
            foreach (var ddc in containers)
            {
                if (ddc.inside)
                {
                    //Debug.Log("OnEndDrag " + ddc);
                    endContainer = ddc;
                    break;
                }
            }

            if (endContainer == null)
            {
                SetToDragDropContrainer(_startDDC);
            }
            else
            {
                SetToDragDropContrainer(endContainer);
                if (mouthContainer == endContainer)
                {
                    EatPills();
                }
            }

            foreach (var ddc in containers)
            {
                if (ddc != endContainer)
                    ddc.inside = false;
            }
        }


        void EatPills()
        {
            StartCoroutine(EatPillCoroutine());
        }

        IEnumerator EatPillCoroutine()
        {
            GetComponent<Image>().raycastTarget = false;
            SoundSystem.instance.Play("pill");
            transform.DORotate(new Vector3(0, 0, 90), 2.5f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(2.7f);
            SoundSystem.instance.Play("eat");
            transform.DORotate(new Vector3(0, 0, 0), 2.5f).SetEase(Ease.InOutCubic);
            yield return new WaitForSeconds(2.5f);
            transform.DOMove(fromContainer.goodPlaceRef.position, 1.7f).SetEase(Ease.InOutCubic);
            yield return new WaitForSeconds(1.8f);

            GetComponent<Image>().raycastTarget = true;
            RescueSystem.instance.OnEatPill();
        }
    }
}