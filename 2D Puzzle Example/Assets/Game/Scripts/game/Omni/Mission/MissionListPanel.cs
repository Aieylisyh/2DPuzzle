using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.game.Omni.Mission
{
    public class MissionListPanel : MonoBehaviour
    {
        public static MissionListPanel instance;

        public List<MissionListItem> missions = new List<MissionListItem>();

        public MissionListItem prefab;//预制体

        public MissionNotification notification;

        private void Awake()
        {
            instance = this;
        }

        public void ToggleOnMission(int index)
        {
            var m = GetCurrentMission(index);
            if (m != null)
            {
                m.ToggleOn();
            }
        }

        public void ToggleOffMissions()
        {
            foreach (var m in missions)
            {
                m.ToggleOff();
            }
        }

        public void ToggleOffMission(int index)
        {
            var m = GetCurrentMission(index);
            if (m != null)
            {
                m.ToggleOff();
            }
        }

        public void RemoveMission(int index)
        {
            var m = GetCurrentMission(index);
            if (m != null)
            {
                missions.Remove(m);
                Destroy(m.gameObject);
            }
        }

        public void AddMission(MissionPrototype proto)
        {
            var newMission = Instantiate(prefab, prefab.transform.parent);
            newMission.proto = proto;
            newMission.data = new MissionData();
            newMission.data.state = MissionData.State.Unread;
            newMission.gameObject.SetActive(true);
            newMission.SyncView();
            newMission.ToggleOff();
        }

        MissionListItem GetCurrentMission(int index)
        {
            if (index < 0 || index >= missions.Count)
            {
                return null;
            }
            return missions[index];
        }
    }
}