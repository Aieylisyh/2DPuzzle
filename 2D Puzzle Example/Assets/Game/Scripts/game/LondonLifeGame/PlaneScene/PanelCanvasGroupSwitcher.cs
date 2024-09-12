using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.LondonLifeGame.PlaneScene
{
    public class PanelCanvasGroupSwitcher : MonoBehaviour
    {

        [SerializeField] CanvasGroup _cg;
        [SerializeField] CanvasGroup _cgNext;
        [SerializeField] float duration = 1;


        public void Show(bool current, bool instant)
        {
            CanvasGroup cg = current ? _cg : _cgNext;
            CanvasGroup other = current ? _cgNext : _cg;

            cg.DOKill();
            other.DOKill();

            if (instant)
            {
                cg.interactable = true;
                cg.blocksRaycasts = true;
                cg.alpha = 1;
                other.interactable = false;
                other.blocksRaycasts = false;
                other.alpha = 0;
            }
            else
            {
                cg.interactable = false;
                cg.blocksRaycasts = false;

                other.interactable = false;
                other.blocksRaycasts = false;

                cg.DOFade(duration, 1).OnComplete(() =>
                {
                    cg.interactable = true;
                    cg.blocksRaycasts = true;
                });
                other.DOFade(duration, 0);
            }
        }

    }
}