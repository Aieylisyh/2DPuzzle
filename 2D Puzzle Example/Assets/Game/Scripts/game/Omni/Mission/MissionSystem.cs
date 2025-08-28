using Assets.Game.Scripts.game.Omni.Mission.TextInfo;
using System.Collections;
using System.Collections.Generic;
using Assets.Game.Scripts.game.Omni.Mission.VideoRecord;
using UnityEngine;

namespace Assets.Game.Scripts.game.Omni.Mission
{
    public class MissionSystem : MonoBehaviour
    {
        public static MissionSystem instance;

        public MissionPackagePrototype[] missionPackages;

        public List<MissionData> missions = new List<MissionData>();

        public TextInfoMissionSystem textInfoMs;
        public VoiceRecordMissionSystem voiceRms;
        public VideoRecordMissionSystem videoRms;
        public bool cheatMode_alwaysCorrect;

        private void Awake()
        {
            instance = this;
        }

        public void Add(int i)
        {
            Add(missionPackages[i]);
        }
        public void Add(MissionPackagePrototype mpp)
        {
            MissionListPanel.instance.notification.Show(mpp);
        }

        public void AddMissionByPackage(MissionPackagePrototype mpp)
        {
            foreach (var p in mpp.protos)
            {
                var newMission = new MissionData();
                newMission.proto = p;
                newMission.state = MissionData.State.Unread;
                newMission.missionPackageId = mpp.id;
                missions.Add(newMission);
                MissionListPanel.instance.AddMission(newMission);
            }
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
            // if (Input.GetKeyDown("1"))
            // {
            //     Add(missionPackages[0]);
            // }
            //
            // if (Input.GetKeyDown("2"))
            // {
            //     Add(missionPackages[1]);
            // }
            //
            // if (Input.GetKeyDown("0"))
            // {
            //     Complete(missions[0]);
            // }
        }

        public void ShowMission(MissionData md)
        {
            if (md.proto.type == MissionPrototype.Type.TextInfo)
            {
                textInfoMs.ResetMission();
                voiceRms.Hide();
                videoRms.Hide();
            }
            else if (md.proto.type == MissionPrototype.Type.VoiceRecord)
            {
                voiceRms.ResetMission();
                textInfoMs.Hide();
                videoRms.Hide();
            }
            else if (md.proto.type == MissionPrototype.Type.VideoRecord)
            {
                videoRms.ResetMission();
                textInfoMs.Hide();
                voiceRms.Hide();
            }
            else
            {
                textInfoMs.Hide();
                voiceRms.Hide();
            }
        }
    }
}