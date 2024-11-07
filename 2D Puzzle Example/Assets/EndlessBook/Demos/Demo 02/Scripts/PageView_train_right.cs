namespace echo17.EndlessBook.Demo02
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using echo17.EndlessBook;

    /// <summary>
    /// Right map page. We could have just assigned the
    /// PageView_Map to the right, but this makes it more complete with
    /// a left and right
    /// </summary>
    public class PageView_train_right : PageView_Map
    {
        public GameObject[] hittables;

        protected override bool HandleHit(RaycastHit hit, BookActionDelegate action)
        {
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
    }
}
