using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts
{
    public class AppleManager : MonoBehaviour
    {
        public static AppleManager instance;

        public AppleMove prefab;

        public int num;
        public float minRadius;
        public float deltaRadius;
        public float minAngularSpeed;
        public float deltaAngularSpeed;
        public float minAlpha;
        public float deltaAlpha;
        public Transform appleParent;

        private List<AppleMove> apples = new List<AppleMove>();

        void Start()
        {
            instance = this;
        }

        public void CreateApples()
        {
            for (int i = 0; i < num; i++)
            {
                var newApple = Instantiate(prefab, appleParent);
                newApple.gameObject.SetActive(true);
                newApple.radius = minRadius + deltaRadius * i;
                newApple.GetComponent<Image>().color = new Color(1, 1, 1, minAlpha + deltaAlpha * i);
                newApple.angularSpeed = minAngularSpeed + deltaAngularSpeed * i;

                apples.Add(newApple);
            }
        }

        public void DeleteApples()
        {
            foreach (var a in apples)
            {
                Destroy(a.gameObject);
            }
            apples = new List<AppleMove>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(2))
                TogglePause();
        }

        void TogglePause()
        {
            if (Time.timeScale != 0)
                Pause();
            else
                Resume();
        }

        void Pause()
        {
            Time.timeScale = 0;
        }

        void Resume()
        {
            Time.timeScale = 1;
        }
    }
}