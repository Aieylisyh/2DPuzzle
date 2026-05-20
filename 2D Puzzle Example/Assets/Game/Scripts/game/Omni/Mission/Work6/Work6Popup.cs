using com;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.game.Omni.Mission.Work6
{
    public class Work6Popup : MonoBehaviour
    {
        public Image popupShow;
        public Image popupNo;
        public Image popupYes;
        public GameObject btns;

        private bool _interrupted;

        public void OnHoverPopupNo()
        {
            if (_interrupted)
                return;
            SetPopupState(PopupState.No);
        }

        public void OnHoverPopupYes()
        {
            if (_interrupted)
                return;
            StartCoroutine(HoverYesIE());
        }

        public void OnHoverPopupPanel()
        {
            if (_interrupted)
                return;
            SetPopupState(PopupState.Show);
        }
        IEnumerator HoverYesIE()
        {
            _interrupted = true;
            SetPopupState(PopupState.Yes);
            yield return new WaitForSeconds(0.75f);
            SetPopupState(PopupState.No);
            //yield return new WaitForSeconds(0.25f);
            _interrupted = false;
        }

        public enum PopupState
        {
            None,
            Show,
            Yes,
            No
        }
        public void SetPopupState(PopupState ps)
        {
            switch (ps)
            {
                case PopupState.None:
                    popupShow.gameObject.SetActive(false);
                    popupYes.gameObject.SetActive(false);
                    popupNo.gameObject.SetActive(false);
                    btns.SetActive(false);
                    break;
                case PopupState.Show:
                    popupShow.gameObject.SetActive(true);

                    popupYes.gameObject.SetActive(false);
                    popupNo.gameObject.SetActive(false);
                    btns.SetActive(true);
                    SoundSystem.instance.Play("warning");
                    break;
                case PopupState.Yes:
                    popupShow.gameObject.SetActive(false);
                    popupNo.gameObject.SetActive(false);

                    popupYes.gameObject.SetActive(true);
                    btns.SetActive(true);
                    break;
                case PopupState.No:
                    popupShow.gameObject.SetActive(false);
                    popupYes.gameObject.SetActive(false);

                    popupNo.gameObject.SetActive(true);
                    btns.SetActive(true);
                    break;
            }
        }

    }
}