using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Game.Scripts.game.Diary.Common
{
    public class ArrivalMover : MonoBehaviour
    {
        public bool controlEnabled;

        public Rigidbody2D rb;
        public float speed;

        public List<ArrivalFollower> myFellows = new List<ArrivalFollower>();
        public PageView_Kuang_Arrival_1 pageView_Kuang_Arrival_1;

        private Vector3 localScale;

        private void Start()
        {
            localScale = transform.localScale;
        }
        private void Update()
        {
            ReadInput();
        }

        void ReadInput()
        {
            if (!controlEnabled)
                return;

            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");
            if (Mathf.Abs(h) + Mathf.Abs(v) > 0.1f)
            {
                rb.velocity = (new Vector2(h, v)).normalized * speed;
                return;
            }
            if (h > 0.5f)
                MoveRight();
            else if (h < -0.5f)
                MoveLeft();
            else if (v > 0.5f)
                MoveUp();
            else if (v < -0.5f)
                MoveDown();
            else
                Stop();
        }

        void MoveUp()
        {
            rb.velocity = new Vector2(0, speed);
            transform.localScale = localScale;
        }

        void MoveDown()
        {
            rb.velocity = new Vector2(0, -speed);
            transform.localScale = localScale;
        }

        void MoveRight()
        {
            rb.velocity = new Vector2(speed, 0);
            transform.localScale = localScale;
        }

        void MoveLeft()
        {
            rb.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }
        void Stop()
        {
            rb.velocity = new Vector2(0, 0);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var ab = collision.GetComponent<ArrivalBus>();
            if (ab != null)
            {
                if (myFellows.Count >= pageView_Kuang_Arrival_1.afs.Length)
                {
                    Debug.Log("all af reached!");
                    controlEnabled = false;
                    var delay = 0.2f;
                    var offset = 0f;
                    foreach (var f in myFellows)
                    {
                        f.transform.SetParent(ab.transform);
                        f.myIndex = -1;
                        offset += 1f;
                        f.transform.DOMove(ab.door.position - Vector3.right * offset, 1).SetDelay(delay);
                        delay += 0.35f;
                    }
                    GetComponent<Collider2D>().enabled = false;
                    transform.SetParent(ab.transform);
                    transform.DOMove(ab.door.position, 1).SetDelay(delay).OnComplete(
                   () =>
                   {
                       pageView_Kuang_Arrival_1.puzzleDone = true;
                       pageView_Kuang_Arrival_1.CheckLock();
                   }
                        );
                    ab.StartJourney(delay + 0.5f);
                }
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            //Debug.Log("arrival mover OnCollisionEnter2D");
            var f = collision.transform.GetComponent<ArrivalFollower>();
            if (f != null && myFellows.IndexOf(f) < 0)
            {
                f.FollowMePlease(this);
            }
        }
    }
}