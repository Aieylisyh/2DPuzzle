using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Linyifei_Game.Script.Onmi_sys
{
    public class OmniMainInterface : MonoBehaviour
    {
        public GameObject sp_bright;
        public GameObject sp_dark;

        public OmniTabButton[] omniTabs;

        private void Start()
        {
            foreach (var b in omniTabs)
                b.mainInterface = this;

            OnClickButton(null);
        }

        public void OnClickButton(OmniTabButton btn)
        {
            foreach (var b in omniTabs)
            {
                if (b != btn)
                {
                    b.img.sprite = b.sp;
                    b.content.SetActive(false);
                }
                else
                {
                    b.img.sprite = b.sp_active;
                    b.content.SetActive(true);

                    if (b.isDarkTheme)
                    {
                        sp_bright.SetActive(false);
                        sp_dark.SetActive(true);
                    }
                    else
                    {
                        sp_bright.SetActive(true);
                        sp_dark.SetActive(false);
                    }
                }
            }
        }
    }
}