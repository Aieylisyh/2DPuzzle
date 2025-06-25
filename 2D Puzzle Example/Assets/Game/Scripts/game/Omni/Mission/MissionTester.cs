using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.Omni.Mission
{
    public class MissionTester : MonoBehaviour
    {
        public MissionPrototype[] tests;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("1"))
            {
                MissionListPanel.instance.notification.Show(tests[0]);
            }
            if (Input.GetKeyDown("2"))
            {
                MissionListPanel.instance.notification.Show(tests[1]);
            }
            if (Input.GetKeyDown("3"))
            {
                MissionListPanel.instance.notification.Show(tests[2]);
            }
            if (Input.GetKeyDown("4"))
            {
                MissionListPanel.instance.notification.Show(tests[3]);
            }
        }
    }
}