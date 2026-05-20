using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Game.Scripts.game.Omni.WebCam
{
    public class WebCamDebugger : MonoBehaviour
    {
        public GameObject content;
        public KeyCode toggleKey = KeyCode.F1;
        // Use this for initialization
        void Start()
        {
            content.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(toggleKey))
            {
                content.SetActive(!content.activeSelf);
            }
        }
    }
}