using System.Collections;
using UnityEngine;

namespace Assets.EndlessBook.Demos.Demo_02.Scripts
{
    public class DiaryDraggable : MonoBehaviour
    {
        public float dragFactor = 100;
        bool _dragging;
        Vector3 _posStartDrag;

        public void StartDrag()
        {
            _dragging = true;
            _posStartDrag = transform.position;
        }

        public void EndDrag()
        {
            _dragging = false;
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