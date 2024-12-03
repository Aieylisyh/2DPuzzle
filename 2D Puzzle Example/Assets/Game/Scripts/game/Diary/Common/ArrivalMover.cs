using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Game.Scripts.game.Diary.Common
{
    public class ArrivalMover : MonoBehaviour
    {
        public bool controlEnabled;

        public Rigidbody2D rb;
        public float speed;

        public List<ArrivalFollower> myFellows = new List<ArrivalFollower>();

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