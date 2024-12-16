using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.Diary.Common
{
    public class ArrivalFollower : MonoBehaviour
    {
        public ArrivalMover host;
        public int myIndex = -1;

        public float goodDistance;
        public Rigidbody2D rb;
        public float speed;
        public GameObject meetPrefeb;

        void Update()
        {
            if (myIndex >= 0) Follow();
        }

        public void FollowMePlease(ArrivalMover am)
        {
            myIndex = am.myFellows.Count;
            host = am;
            am.myFellows.Add(this);

            //Debug.Log("FollowMePlease " + gameObject.name + " myIndex:" + myIndex);
            var m = Instantiate(meetPrefeb, transform.position + new Vector3(1.3f, 1.5f, 0), Quaternion.identity, this.transform);
            m.transform.localScale = Vector3.zero;
            m.transform.DOScale(1f, 1).SetEase(Ease.OutBounce);
            Destroy(m, 2);
        }

        void Follow()
        {
            Transform t = host.transform;
            if (myIndex >= 1)
            {
                t = host.myFellows[myIndex - 1].transform;
            }

            var dist = t.position - transform.position;
            dist.z = 0;
            var d = dist.magnitude;
            if (d < goodDistance)
            {

            }
            else
            {

                rb.velocity = dist.normalized * speed;
                return;
                var ax = Mathf.Abs(dist.x);
                var ay = Mathf.Abs(dist.y);
                if (ay >= ax)
                {
                    if (dist.y > 0)
                    {
                        MoveUp();
                    }
                    else
                    {
                        MoveDown();
                    }
                }
                else
                {
                    if (dist.x > 0)
                    {
                        MoveRight();
                    }
                    else
                    {
                        MoveLeft();
                    }
                }
            }
        }

        void MoveUp()
        {
            rb.velocity = new Vector2(0, speed);
        }

        void MoveDown()
        {
            rb.velocity = new Vector2(0, -speed);
        }

        void MoveRight()
        {
            rb.velocity = new Vector2(speed, 0);
        }

        void MoveLeft()
        {
            rb.velocity = new Vector2(-speed, 0);
        }
    }
}