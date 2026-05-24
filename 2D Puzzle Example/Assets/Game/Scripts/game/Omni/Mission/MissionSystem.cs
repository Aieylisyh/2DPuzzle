using Assets.Game.Scripts.game.Omni.Mission.TextInfo;
using Assets.Game.Scripts.game.Omni.Mission.VideoRecord;
using Assets.Game.Scripts.game.Omni.Mission.Work6;
using com;
using System.Collections;
using System.Collections.Generic;
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
        public MapMissionSystem mapMs1;
        public MapMission2System mapMs2;
        public Work6Mission work6Mission;
        public bool cheatMode_alwaysCorrect;
        /// <summary>
        /// F3 切换：为 true 时跳过「须先完成前置任务」限制。
        /// </summary>
        public bool cheatMode_skipOrderLock;

        static bool hasShownFirstMissionOpenLine;

        private void Awake()
        {
            instance = this;
        }

        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.F3))
                return;

            cheatMode_skipOrderLock = !cheatMode_skipOrderLock;
            Debug.Log("Mission order lock " + (cheatMode_skipOrderLock ? "disabled" : "enabled") + " (F3)");

            if (MissionListPanel.instance != null)
                MissionListPanel.instance.RefreshAllMissionItems();
        }

        public bool IsMissionUnlocked(MissionData md)
        {
            if (md == null || md.proto == null)
                return false;

            if (cheatMode_skipOrderLock)
                return true;

            int targetOrder = md.proto.order;

            foreach (var m in missions)
            {
                if (m == null || m.proto == null || m == md)
                    continue;

                if (m.proto.order < targetOrder && m.state != MissionData.State.Done)
                    return false;
            }

            return true;
        }

        public void Add(int i)
        {
            Add(missionPackages[i]);
        }
        public void Add(MissionPackagePrototype mpp)
        {
            MissionListPanel.instance.notification.Show(mpp);
            SoundSystem.instance.Play("newmsg");
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
                    break;
                }
            }
            // MissionListPanel.instance.RefreshCompletedMissionsCount();
        }

        public void ShowMission(MissionData md)
        {
            if (!IsMissionUnlocked(md))
            {
                Debug.Log("Mission locked: complete previous missions first. " + md.proto.title);
                if (SoundSystem.instance != null)
                    SoundSystem.instance.Play("warning");
                return;
            }

            if (MissionDialogFrame.instance != null)
                MissionDialogFrame.instance.Hide();

            if (md.proto.type == MissionPrototype.Type.TextInfo)
            {
                textInfoMs.ResetMission();
                voiceRms.Hide();
                videoRms.Hide();
                mapMs1.Hide();
                mapMs2.Hide();
                work6Mission.Hide();

                textInfoMs.missionData = md;
            }
            else if (md.proto.type == MissionPrototype.Type.VoiceRecord)
            {
                voiceRms.ResetMission();
                textInfoMs.Hide();
                videoRms.Hide();
                mapMs1.Hide();
                mapMs2.Hide();
                work6Mission.Hide();

                voiceRms.missionData = md;
            }
            else if (md.proto.type == MissionPrototype.Type.VideoRecord)
            {
                videoRms.ResetMission();
                textInfoMs.Hide();
                voiceRms.Hide();
                mapMs1.Hide();
                mapMs2.Hide();
                work6Mission.Hide();

                videoRms.missionData = md;
            }
            else if (md.proto.type == MissionPrototype.Type.Map)
            {
                videoRms.Hide();
                textInfoMs.Hide();
                voiceRms.Hide();
                mapMs1.ResetMission();
                mapMs2.Hide();
                work6Mission.Hide();

                mapMs1.missionData = md;
            }
            else if (md.proto.type == MissionPrototype.Type.Map2)
            {
                videoRms.Hide();
                textInfoMs.Hide();
                voiceRms.Hide();
                mapMs1.Hide();
                mapMs2.ResetMission();
                work6Mission.Hide();

                mapMs2.missionData = md;
            }
            else if (md.proto.type == MissionPrototype.Type.Work6)
            {
                videoRms.Hide();
                textInfoMs.Hide();
                voiceRms.Hide();
                mapMs1.Hide();
                mapMs2.Hide();
                work6Mission.ResetMission();

                work6Mission.missionData = md;
            }
            else
            {
                textInfoMs.Hide();
                voiceRms.Hide();
                videoRms.Hide();
                mapMs1.Hide();
                mapMs2.Hide();
            }

            if (!hasShownFirstMissionOpenLine)
            {
                hasShownFirstMissionOpenLine = true;
                if (MissionDialogFrame.instance != null)
                    MissionDialogFrame.instance.Show(MissionDialogLines.FirstMissionOpen);
            }
        }
    }
}