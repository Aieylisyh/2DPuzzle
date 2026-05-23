using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.game.Omni._3d
{
    public class UiBehaviour : MonoBehaviour
    {
        public static UiBehaviour instance;
        public CanvasGroup cg_door;

        private void Awake()
        {
            instance = this;
        }
        // Use this for initialization
        void Start()
        {
            cg_door.alpha = 0;
        }

        public void ToggleDoorTip(bool b)
        {
            cg_door.DOKill();
            if (b)
            {
                cg_door.DOFade(1, 1);
            }
            else
            {
                cg_door.DOFade(0, 1);
            }
        }

        void OnDestroy()
        {
            if (cg_door != null)
                cg_door.DOKill();
        }
    }
}