using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.Diary.Common
{
    public class ArrivalFollower : MonoBehaviour
    {
        public Transform followingTarget;
        public float goodDistance;
        public Rigidbody2D rb;
        public float speed;

        void Update()
        {
            if (followingTarget != null) Follow();
        }

        void Follow()
        {
            var dist = followingTarget.position - transform.position;
            dist.z = 0;
            var d = dist.magnitude;
            if (d < goodDistance)
            {

            }
            else
            {
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