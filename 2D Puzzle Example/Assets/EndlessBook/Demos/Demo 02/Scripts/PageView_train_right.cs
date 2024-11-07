namespace echo17.EndlessBook.Demo02
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Right map page. We could have just assigned the
    /// PageView_Map to the right, but this makes it more complete with
    /// a left and right
    /// </summary>
    public class PageView_train_right : PageView_Map
    {
        public GameObject[] hittables;

        public GameObject train;
        public float dragFactor = 100;
        bool _draggingTrain;
        Vector3 _posStartDrag;

        protected override bool HandleHit(RaycastHit hit, BookActionDelegate action)
        {
            _draggingTrain = false;//because this is triggered on released
            foreach (var h in hittables)
            {
                if (h == hit.collider.gameObject)
                {
                    Debug.Log("hit " + h.gameObject.name);
                    return true;
                }
            }
            return false;
        }

        public override bool HandleTouchDown(Vector2 hitPointNormalized)
        {
            //Debug.Log("HandleTouchDown");
            if (pageViewCamera == null) return false;

            _draggingTrain = false;
            // cast a ray
            RaycastHit hit;
            if (Physics.Raycast(pageViewCamera.ViewportPointToRay(hitPointNormalized), out hit, maxRayCastDistance, raycastLayerMask))
            {
                Debug.Log(hit.collider.gameObject);
                if (hit.collider.gameObject == train)
                {
                    Debug.Log("start drag");
                    _posStartDrag = train.transform.position;
                    _draggingTrain = true;
                }
                return true;
            }



            return false;
        }

        public override void Drag(Vector2 increment, bool useInertia)
        {
              Debug.Log("Drag " );
            if (_draggingTrain)
            {
                Debug.Log("Drag increment " + increment + " useInertia " + useInertia);
                _posStartDrag += (Vector3)increment * dragFactor;
                train.transform.position = _posStartDrag;
            }
        }
    }
}
