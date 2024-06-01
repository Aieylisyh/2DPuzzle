using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class 简单的方向键控制 : MonoBehaviour
    {
        public float speed;

        public Rigidbody rb;

        void Update()
        {
            Vector2 movement = Vector2.zero;

            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
            Debug.Log(movement);
            rb.velocity = new Vector3(movement.x, 0, movement.y) * speed;
            //transform.position += new Vector3(movement.x, 0, movement.y) * speed * Time.deltaTime;

            var mao1 = new 猫();

            var mao2 = new 银渐层();

            var mao3 = mao2.获得自己();

            var mao4 = 银渐层.获得一个标准的银渐层Static();
        }
    }

    public class 猫
    {

    }

    public class 银渐层 : 猫
    {
        public 银渐层 获得自己()
        {
            return this;
        }

        public static 银渐层 获得一个标准的银渐层Static()
        {
            return new 银渐层();
        }
    }
}