using Omni;
using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.game.Omni.WebCam
{
    public class WebCamDebugger : MonoBehaviour
    {
        public GameObject panel;
        public GameObject infoPanel;
        public GameObject coreBtns;
        public WebcamPreview webcamPreview;
        public static int defaultCamIndex = 0;
        public GameObject modelView;
        public TextMeshProUGUI txt;
        public GameObject[] finishShows;
        public GameObject finishBtn;

        void Start()
        {
            panel.SetActive(false);
            infoPanel.SetActive(false);
            coreBtns.SetActive(false);
            foreach (var item in finishShows)
            {
                item.SetActive(false);
            }

            if (finishBtn != null)
                finishBtn.SetActive(true);

            if (ShouldSkipSetup())
                ApplyFinishedSetup();
        }

        public void OnClickOpenPanel()
        {
            panel.SetActive(true);
            infoPanel.SetActive(true);
            coreBtns.SetActive(true);

            if (finishBtn != null)
                finishBtn.SetActive(true);

            SyncText();
            webcamPreview.StartCapture();
        }

        public void OnClickFinshPanel()
        {
            ApplyFinishedSetup();
            MarkSetupCompleted();
        }

        public void ApplyFinishedSetup()
        {
            if (webcamPreview != null)
                webcamPreview.StopCapture();

            if (panel != null)
                panel.SetActive(false);
            if (infoPanel != null)
                infoPanel.SetActive(false);
            if (coreBtns != null)
                coreBtns.SetActive(false);
            if (modelView != null)
                modelView.SetActive(false);

            if (finishShows != null)
            {
                foreach (var item in finishShows)
                {
                    if (item != null)
                        item.SetActive(true);
                }
            }

            if (finishBtn != null)
                finishBtn.SetActive(false);
        }

        static bool ShouldSkipSetup()
        {
            return Omni2DSystem.instance != null
                && Omni2DSystem.instance.cfg != null
                && Omni2DSystem.instance.cfg.skipWebcamDebugger;
        }

        static void MarkSetupCompleted()
        {
            if (Omni2DSystem.instance == null || Omni2DSystem.instance.cfg == null)
                return;

            Omni2DSystem.instance.cfg.skipWebcamDebugger = true;
        }

        public void OnClickAdd()
        {
            defaultCamIndex++;
            TestWebCam();
            SyncText();
        }

        public void OnClickReduce()
        {
            defaultCamIndex--;
            TestWebCam();
            SyncText();
        }

        void SyncText()
        {
            txt.text = "[" + defaultCamIndex + "]";
        }

        void TestWebCam()
        {
            if (defaultCamIndex > 9)
                defaultCamIndex = 0;
            if (defaultCamIndex < 0)
                defaultCamIndex = 9;
            webcamPreview.StopCapture();
            webcamPreview.StartCapture();
        }

        public void OnClickToggleInfoPanel()
        {
            if (!coreBtns.activeSelf)
                coreBtns.SetActive(true);

            infoPanel.SetActive(!infoPanel.activeSelf);
        }
    }
}
