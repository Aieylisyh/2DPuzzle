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

        public bool revealed;

        private void OnEnable()
        {
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
            toReveal.DOFade(1, 1);
            return true;
        }
    }
}