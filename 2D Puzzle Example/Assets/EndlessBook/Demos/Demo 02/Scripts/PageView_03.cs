namespace echo17.EndlessBook.Demo02
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using echo17.EndlessBook;
    using Assets.Game.Scripts.game.Diary;
    using Unity.VisualScripting;

    public class PageView_03 : PageView
    {
        public GameObject btn1;
        public GameObject btn2;

        int btn1HitCount;
        int btn2HitCount;
        public float dragFactor=100;

        public override void Activate()
        {
            base.Activate();
            CheckLock();
        }
        public override void Deactivate()
        {
            base.Deactivate();
            DiaryGameSystem.instance.ToggleLockTurnNextPage(false);
        }

        void CheckLock()
        {
            if (btn1HitCount >= 1 && btn2HitCount >= 1)
            {
                DiaryGameSystem.instance.ToggleLockTurnNextPage(false);
            }
            else
            {
                DiaryGameSystem.instance.ToggleLockTurnNextPage(true);
            }
        }

        public override void TouchDown()
        {
            base.TouchDown();
            //Debug.Log("TouchDown");
        }

        protected override bool HandleHit(RaycastHit hit, BookActionDelegate action)
        {
            _draggingBtn1 = false;
            //Debug.Log("HandleHit");
            // no action, just return
            if (btn1 == hit.collider.gameObject)
            {
                Debug.Log("hit btn1");
                btn1HitCount++;
                CheckLock();
                return true;
            }
            if (btn2 == hit.collider.gameObject)
            {
                Debug.Log("hit btn2");
                btn2HitCount++;
                CheckLock();
                return true;
            }
            return false;
        }

        bool _draggingBtn1;
        Vector3 _posStartDrag;
        public override bool HandleTouchDown(Vector2 hitPointNormalized)
        {
            //Debug.Log("HandleTouchDown");
            if (pageViewCamera == null) return false;

            _draggingBtn1 = false;
            // cast a ray
            RaycastHit hit;
            if (Physics.Raycast(pageViewCamera.ViewportPointToRay(hitPointNormalized), out hit, maxRayCastDistance, raycastLayerMask))
            {
                Debug.Log(hit.collider.gameObject);
                if (hit.collider.gameObject == btn1)
                {
                    Debug.Log("start drag");
                    _posStartDrag = btn1.transform.position;
                    _draggingBtn1 = true;
                }
                return true;
            }



            return false;
        }

        public override void Drag(Vector2 increment, bool useInertia)
        {
            if (_draggingBtn1)
            {
                //Debug.Log("Drag increment " + increment + " useInertia " + useInertia);
                _posStartDrag += (Vector3)increment * dragFactor;
                btn1.transform.position = _posStartDrag;
            }
        }
    }
}