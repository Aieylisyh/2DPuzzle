using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.MoissanColorGame.Script
{
    public class CombatStateController : MonoBehaviour
    {
        public static CombatStateController instance;

        public Camera cam;

        public float inCombatCamSize;
        public float outCombatCamSize;
        public float duration;

        public Transform inCombatCamParent;//00
        public Vector3 inCombatCamLocationPositionOffset;
        public Transform outCombatCamParent;//00

        public bool inCombat;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            inCombat = false;
        }


        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                ToggleInCombat(!inCombat);
            }
        }

        void ToggleInCombat(bool b)
        {
            if (inCombat == b)
                return;
            inCombat = b;

            cam.DOKill();
            if (b)
            {
                cam.transform.SetParent(inCombatCamParent);
                cam.transform.DOLocalMove(new Vector3(0, 0, -10) + inCombatCamLocationPositionOffset, duration);
                cam.DOOrthoSize(inCombatCamSize, duration);
            }
            else
            {
                cam.transform.SetParent(outCombatCamParent);
                cam.transform.DOLocalMove(new Vector3(0, 0, -10), duration);
                cam.DOOrthoSize(outCombatCamSize, duration);
            }

        }
    }
}