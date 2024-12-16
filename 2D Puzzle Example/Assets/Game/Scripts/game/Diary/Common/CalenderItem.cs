using com;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.game.Diary.Common
{
    public class CalenderItem : MonoBehaviour
    {
        public GameObject col;
        public Image toReveal;
        public Image toHide;
        public bool revealed;

        private void OnEnable()
        {
            toHide.DOKill();
            toReveal.DOKill();
            if (revealed)
            {
                toReveal.color = Color.white;
            }
            else
            {
                toReveal.color = new Color(1, 1, 1, 0);
            }
        }

        public bool OnClick(GameObject g)
        {
            if (g != col)
                return false;

            if (revealed)
                return true;

            revealed = true;
            SoundSystem.instance.Play(new string[] { "bling", "bo2" });
            toReveal.DOFade(1, 1).SetDelay(0.7f);
            toHide.DOFade(0, 1);
            return true;
        }
    }
}