using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace com
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
            foreach (var c in LondonLifeGameSystem.instance.allScenes)
            {
                if (c != null && c != other && c != cg)
                {
                    c.interactable = false;
                    c.blocksRaycasts = false;
                    c.alpha = 0;
                }
            }

            if (instant)
            {
                if (cg != null)
                {
                    cg.interactable = true;
                    cg.blocksRaycasts = true;
                    cg.alpha = 1;
                }
                if (other != null)
                {
                    other.interactable = false;
                    other.blocksRaycasts = false;
                    other.alpha = 0;
                }
            }
            else
            {
                if (cg != null)
                {
                    cg.interactable = false;
                    cg.blocksRaycasts = false;
                    cg.DOFade(1, duration).OnComplete(() =>
                                   {
                                       cg.interactable = true;
                                       cg.blocksRaycasts = true;
                                   });
                }
                if (other != null)
                {
                    other.interactable = false;
                    other.blocksRaycasts = false;

                    other.DOFade(0, duration);
                }
            }
        }
    }
}