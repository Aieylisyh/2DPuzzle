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
            webcamPreview.StopCapture();
            panel.SetActive(false);
            modelView.SetActive(false);

            foreach (var item in finishShows)
            {
                item.SetActive(true);
            }
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
