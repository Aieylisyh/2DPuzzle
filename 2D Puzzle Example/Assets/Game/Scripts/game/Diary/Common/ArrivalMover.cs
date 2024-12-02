using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Game.Scripts.game.Diary.Common
{
    public class ArrivalMover : MonoBehaviour
    {
        public bool controlEnabled;

        public Rigidbody2D rb;
        public float speed;

        public ArrivalFollower myFellow;

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
        void Stop()
        {
            rb.velocity = new Vector2(0, 0);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("arrival mover OnTriggerEnter2D");
            var f = collision.GetComponent<ArrivalFollower>();
            if (f != null && f.followingTarget == null)
            {
                f.followingTarget = GetMyLastFellow();
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log("arrival mover OnCollisionEnter2D");
            var f = collision.transform.GetComponent<ArrivalFollower>();
            if (f != null && f.followingTarget == null)
            {
                f.followingTarget = GetMyLastFellow();
            }
        }

        Transform GetMyLastFellow()
        {
            if (myFellow != null)
            {
                ArrivalFollower af = myFellow;
                while (af.followingTarget != null)
                {
                    af = af.followingTarget.GetComponent<ArrivalFollower>();
                }
                return af.transform;
            }
            return transform;
        }
    }
}