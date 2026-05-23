using com;
using Omni;
using UnityEngine;

namespace Assets.Game.Scripts.game.Omni.Mission
{
    public abstract class MissionBehaviour : MonoBehaviour
    {
        public GameObject view;
        public MissionData missionData;

        public GameObject[] submitSucToShows;
        public GameObject[] submitSucToHides;
        public GameObject[] submitFailToShows;
        public GameObject[] submitFailToHides;
        public GameObject finishBtn_ok;
        public GameObject finishBtn_notOk;

        protected bool currentMissionDone;

        protected virtual void Awake()
        {
            if (view != null)
                view.SetActive(false);
        }

        public virtual void ResetMission()
        {
            if (view != null)
                view.SetActive(true);

            ResetSubmitUI();
            ToggleFinishedButton(false);
            OnResetMission();

            if (currentMissionDone)
                ShowSuccessSubmitUI();
        }

        protected virtual void OnResetMission() { }

        public virtual void Hide()
        {
            if (view != null)
                view.SetActive(false);
        }

        public virtual void RetryMission()
        {
            SetGameObjectsActive(submitFailToShows, false);
        }

        public virtual void SubmitMission()
        {
            bool result = ValidateSubmission();
            if (MissionSystem.instance != null && MissionSystem.instance.cheatMode_alwaysCorrect)
                result = true;

            Debug.Log(GetType().Name + " SubmitMission " + result);

            if (result)
                OnSubmitSuccess();
            else
                OnSubmitFail();
        }

        protected virtual bool ValidateSubmission() => true;

        protected virtual void OnSubmitSuccess()
        {
            CompleteMission();
            ShowSuccessSubmitUI();
        }

        protected virtual void OnSubmitFail()
        {
            if (SoundSystem.instance != null)
                SoundSystem.instance.Play("warning");
            ShowFailSubmitUI();
        }

        protected void CompleteMission()
        {
            if (SoundSystem.instance != null)
                SoundSystem.instance.Play("newmsg");
            if (Omni2DSystem.instance != null)
                Omni2DSystem.instance.RefreshMissionDoneNum(1);
            if (MissionSystem.instance != null && missionData != null)
                MissionSystem.instance.Complete(missionData);
            currentMissionDone = true;
        }

        protected void ResetSubmitUI()
        {
            SetGameObjectsActive(submitSucToShows, false);
            SetGameObjectsActive(submitFailToShows, false);
        }

        protected void ShowSuccessSubmitUI()
        {
            SetGameObjectsActive(submitSucToShows, true);
            SetGameObjectsActive(submitSucToHides, false);
        }

        protected void ShowFailSubmitUI()
        {
            SetGameObjectsActive(submitFailToShows, true);
            SetGameObjectsActive(submitFailToHides, false);
        }

        protected void ToggleFinishedButton(bool ok)
        {
            if (finishBtn_ok != null)
                finishBtn_ok.SetActive(ok);
            if (finishBtn_notOk != null)
                finishBtn_notOk.SetActive(!ok);
        }

        protected virtual void RefreshFinishBtn() { }

        protected static void SetGameObjectsActive(GameObject[] objects, bool active)
        {
            if (objects == null)
                return;

            foreach (var g in objects)
            {
                if (g != null)
                    g.SetActive(active);
            }
        }

        protected static void SetActiveIfNotNull(GameObject target, bool active)
        {
            if (target != null)
                target.SetActive(active);
        }

        protected static int CountActiveGameObjects(GameObject[] objects)
        {
            if (objects == null)
                return 0;

            int count = 0;
            foreach (var g in objects)
            {
                if (g != null && g.activeSelf)
                    count++;
            }

            return count;
        }
    }
}
