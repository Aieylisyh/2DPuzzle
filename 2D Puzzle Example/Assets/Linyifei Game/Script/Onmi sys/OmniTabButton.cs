using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Linyifei_Game.Script.Onmi_sys
{
    public class OmniTabButton : MonoBehaviour
    {
        public Sprite sp;
        public Sprite sp_active;
        public Image img;
        public GameObject content;
        public bool isDarkTheme;

        [HideInInspector]
        public OmniMainInterface mainInterface;

        public void OnClick()
        {
            mainInterface.OnClickButton(this);
            //播放音效
        }
    }
}