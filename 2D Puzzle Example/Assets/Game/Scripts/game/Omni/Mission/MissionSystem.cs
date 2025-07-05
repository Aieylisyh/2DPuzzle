using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Assets.Game.Scripts.game.Omni.Mission
{
    public class MissionSystem : MonoBehaviour
    {
        public static MissionSystem instance;

        public MissionPrototype[] tests;

        public List<MissionData> missions = new List<MissionData>();

        private void Awake()
        {
            instance = this;
        }

        void Add(MissionPrototype p)
        {
            var newMission = new MissionData();
            newMission.proto = p;
            newMission.state = MissionData.State.Unread;
            missions.Add(newMission);

            MissionListPanel.instance.notification.Show(newMission);
        }

        public int GetCompletedMissionsCount()
        {
            int result = 0;
            foreach (var m in missions)
            {
                if (m.state == MissionData.State.Done)
                {
                    result += 1;
                }
            }
            return result;
        }

        public void Complete(MissionData missionToComplete)
        {
            foreach (var m in missions)
            {
                if (m == missionToComplete)
                {
                    m.state = MissionData.State.Done;
                    MissionListPanel.instance.RefreshAllMissionItems();
                }
            }
            MissionListPanel.instance.RefreshCompletedMissionsCount();
        }

        void Update()
        {
            if (Input.GetKeyDown("1"))
            {
                Add(tests[0]);
            }

            if (Input.GetKeyDown("2"))
            {
                Add(tests[1]);
            }
            if (Input.GetKeyDown("3"))
            {
                Add(tests[2]);
            }
            if (Input.GetKeyDown("4"))
            {
                Add(tests[3]);
            }

            if (Input.GetKeyDown("0"))
            {
                Complete(missions[0]);
            }
        }
    }
}