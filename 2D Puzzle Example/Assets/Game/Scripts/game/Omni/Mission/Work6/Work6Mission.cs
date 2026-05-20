using com;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Assets.Game.Scripts.game.Omni.Mission.Work6
{
    public class Work6Mission : MonoBehaviour
    {
        public Work6Popup popup;
        public GameObject talk1;
        public GameObject talk2;
        public GameObject talk3;
        public GameObject talk4;
        public GameObject talk5;
        public GameObject talk6;

        public GameObject videoButton;
        public VideoPlayer vp;

        public GameObject missionObj;
        public Work6Email email;
        public GameObject header;

        public MissionPrototype work6proto;

        private void Start()
        {
            Hide();
            vp.loopPointReached += OnVideoFinished;
        }
        private void OnVideoFinished(VideoPlayer source)
        {
            vp.gameObject.SetActive(false);
            SoundSystem.instance.Play("click");
        }

        public void StartWork6ByPlot()
        {
            var newMission = new MissionData();
            newMission.proto = work6proto;
            newMission.state = MissionData.State.Unread;
            newMission.missionPackageId = "work6";
            MissionSystem.instance.missions.Add(newMission);
            MissionListPanel.instance.AddMission(newMission);
            SoundSystem.instance.Play("strange");
        }

        public void Hide()
        {
            popup.SetPopupState(Work6Popup.PopupState.None);
            talk1.gameObject.SetActive(false);
            talk2.gameObject.SetActive(false);
            talk3.gameObject.SetActive(false);
            talk4.gameObject.SetActive(false);
            talk5.gameObject.SetActive(false);
            talk6.gameObject.SetActive(false);
            videoButton.SetActive(true);
            vp.Stop();
            missionObj.SetActive(false);
            header.SetActive(false);
        }

        public void ResetMission()
        {
            Hide();

            missionObj.SetActive(true);
            talk1.gameObject.SetActive(true);
            email.Hide();
            header.SetActive(true);
        }

        public void OnClickVideoButton()
        {
            videoButton.SetActive(false);
            vp.Play();
            talk1.gameObject.SetActive(false);
            talk2.gameObject.SetActive(true);
            StartCoroutine(OnVideoPlayedIE());

        }

        public void OnClickNo()
        {
            popup.SetPopupState(Work6Popup.PopupState.None);
            talk5.gameObject.SetActive(false);
            talk6.gameObject.SetActive(true);

            header.SetActive(false);
            email.header.SetActive(true);
            email.OnStartEmailSequence();
        }

        IEnumerator OnVideoPlayedIE()
        {
            yield return new WaitForSeconds(0.1f);
            yield return new WaitForSeconds(3f);
            talk2.gameObject.SetActive(false);
            talk3.gameObject.SetActive(true);
            yield return new WaitForSeconds(7.0f);
            talk3.gameObject.SetActive(false);
            talk4.gameObject.SetActive(true);
            yield return new WaitForSeconds(5.4f);
            talk4.gameObject.SetActive(false);
            talk5.gameObject.SetActive(true);
            yield return new WaitForSeconds(3.4f);
            popup.SetPopupState(Work6Popup.PopupState.Show);
        }
        //接到任务看视频前-台词1
        //视频0秒（开始看）-台词2
        //视频3秒-台词3
        //视频11秒-台词4
        //视频结束-台词5
        //选完no弹窗消失后-台词6
    }
}