using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.EndlessBook.Demos.Demo_02.Scripts
{
    public class DiaryDraggable : MonoBehaviour
    {
        public float dragFactor = 100;
        bool _dragging;
        Vector3 _posStartDrag;
         public UnityEvent EndEvt;

        public DiaryDragResponser[] responsers;
        public void StartDrag()
        {
            _dragging = true;
            _posStartDrag = transform.position;
        }

        public void EndDrag()
        {
            _dragging = false;
            foreach (var r in responsers)
            {
                if (r.CheckDragEnd(this))
                {
                    break;
                }
            }
        }

        public void OnDrag(Vector2 increment)
        {
            if (_dragging)
            {
                _posStartDrag += (Vector3)increment * dragFactor;
                transform.position = _posStartDrag;
            }
        }
    }
}