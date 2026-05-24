using Assets.Game.Scripts.game.Omni.WebCam;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Linyifei_Game.Script.Onmi_sys
{
    public class EndingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Image image;
        public Sprite hoverSp;
        public Sprite normalSp;

        public void OnPointerEnter(PointerEventData eventData)
        {
            image.sprite = hoverSp;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            image.sprite = normalSp;
        }

        // Use this for initialization
        void Start()
        {
            image = GetComponent<Image>();
            image.sprite = normalSp;
        }

        public void OnClick()
        {
            WebcamDebuggerSession.MarkSkipOnNextSceneLoad();
            SceneManager.LoadScene(0);
        }
    }
}