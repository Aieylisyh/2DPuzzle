using com;
using DG.Tweening;
using Omni;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Game.Scripts.game.Omni.Mission.Work6
{
    public class Work6Email : MonoBehaviour
    {
        //no loop rt
        public RectTransform emailPopup;
        public RectTransform emailPopupFrom;
        public RectTransform emailPopupTo;
        public GameObject emailTalk1;
        public GameObject emailTalk2;
        public GameObject emailRedText;
        public GameObject emailFrame;
        public WebcamPreview webcamPreview;
        public GameObject header;

        public void Hide()
        {
            KillTweens();
            StopAllCoroutines();
            webcamPreview.StopCapture();

            emailPopup.gameObject.SetActive(false);
            emailTalk1.SetActive(false);
            emailTalk2.SetActive(false);
            emailRedText.SetActive(false);
            emailFrame.SetActive(false);
            header.SetActive(false);
        }

        public void OnStartEmailSequence()
        {
            Hide();
            StartCoroutine(StartEmailSequence1_IE());
        }

        public void OnClickPopup()
        {
            emailPopup.DOAnchorPos(emailPopupFrom.anchoredPosition, 0.5f).OnComplete(
                () =>
                {
                    emailPopup.gameObject.SetActive(false);

                }

                );
            //unselect
            emailFrame.SetActive(true);
            StartCoroutine(StartEmailSequence2_IE());
            MissionListPanel.instance.ToggleOffMissions();
        }

        IEnumerator StartEmailSequence1_IE()
        {
            emailPopup.anchoredPosition = emailPopupFrom.anchoredPosition;


            yield return new WaitForSeconds(1.5f);
            SoundSystem.instance.Play("newmsg");
            emailPopup.gameObject.SetActive(true);
            emailPopup.DOAnchorPos(emailPopupTo.anchoredPosition, 0.7f);
        }

        IEnumerator StartEmailSequence2_IE()
        {
            yield return new WaitForSeconds(0.1f);
            emailTalk1.SetActive(true);

            yield return new WaitForSeconds(2f);

            emailTalk2.SetActive(true);
            emailTalk1.SetActive(false);
            yield return new WaitForSeconds(3f);
            Omni2DSystem.instance.StopBgm();
            yield return new WaitForSeconds(1f);

            SoundSystem.instance.Play("strange");
            webcamPreview.gameObject.SetActive(true);
            webcamPreview.StartCapture();

            yield return new WaitForSeconds(3.5f);

            emailRedText.SetActive(true);

            yield return new WaitForSeconds(5f);
            webcamPreview.StopCapture();
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(1);
        }

        void OnDestroy()
        {
            KillTweens();
        }

        void KillTweens()
        {
            if (emailPopup != null)
                emailPopup.DOKill();
        }
    }
}