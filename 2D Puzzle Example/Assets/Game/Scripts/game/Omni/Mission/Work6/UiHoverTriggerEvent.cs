using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Game.Scripts.game.Omni.Mission.Work6
{
    public class UiHoverTriggerEvent : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        public UnityEvent evtEnter;
        public UnityEvent evtDown;
        public UnityEvent evtExit;

        public void OnPointerDown(PointerEventData eventData)
        {
            evtDown?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            evtEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            evtExit?.Invoke();
        }
    }
}