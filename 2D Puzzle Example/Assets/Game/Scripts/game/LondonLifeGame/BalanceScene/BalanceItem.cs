using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.game.LondonLifeGame.BalanceScene
{
    public class BalanceItem : 有容器的拖拽
    {
        public float weight;

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
        }
    }
}