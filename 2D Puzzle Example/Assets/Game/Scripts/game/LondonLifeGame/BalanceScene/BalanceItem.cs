using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Game.Scripts.game.LondonLifeGame.BalanceScene
{
    public class BalanceItem : 有容器的拖拽
    {
        public float weight;

        Vector3 _fromAnchoredPos;
        Transform _fromParent;

        protected override void Awake()
        {
            _rectTrans = GetComponent<RectTransform>();
            _fromAnchoredPos = _rectTrans.anchoredPosition;
            _fromParent = _rectTrans.parent;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            // _fromAnchoredPos = _rectTrans.anchoredPosition;
            // _fromParent = _rectTrans.parent;

            GetComponent<Image>().raycastTarget = false;
            _rectTrans.SetParent(draggingParent.transform);
        }

        protected override void SetToDragDropContainer(DragDropContainer ddc)
        {
            _startDDC = ddc;
            if (ddc != null)
            {
                _rectTrans.SetParent(ddc.transform);
                _rectTrans.anchoredPosition = ddc.place.anchoredPosition;
                GetComponent<Image>().raycastTarget = false;
                BalanceSceneSystem.instance.OnReceiveItem(weight, true);
            }
            else
            {
                _rectTrans.anchoredPosition = _fromAnchoredPos;
                _rectTrans.SetParent(_fromParent);
                GetComponent<Image>().raycastTarget = true;
            }
        }
    }
}