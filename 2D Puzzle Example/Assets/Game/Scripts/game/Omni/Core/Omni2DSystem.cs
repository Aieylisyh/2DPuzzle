using com;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Omni
{

    public partial class Omni2DSystem : MonoBehaviour
    {
        public static Omni2DSystem instance;


        public enum GamePhase
        {
            Default,
            TurnOnPC,
            LoginOmni,
            //插入支线
            MainlineMission_that_employee,
            //插入支线
            MainlineMission_bathroom_girl,
            ThreatMail,
            TurnTo3DScene,


            /// <summary>
            /// 支线
            /// </summary>
            Mission_xxxxx1,
            Mission_xxxxx2,
            Mission_xxxxx3,
            Mission_xxxxx4,
            Mission_xxxxx5,
            Mission_xxxxx6,
            Mission_xxxxx7,


        }

        public GamePhase gameStartPhase;

        void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {

            }
        }

        void ToggleCg(CanvasGroup cg, bool on)
        {
            cg.alpha = on ? 1 : 0;
            cg.interactable = on;
            cg.blocksRaycasts = on;
        }

        private void Start()
        {
            //ToggleCg(sceneCg_girlInBed1, false);
            switch (gameStartPhase)
            {
                case GamePhase.Default:
                    //ToggleCg(sceneCg_girlInBed1, true);
                    //StartSceneGirlInBed();
                    break;
            }
        }

        IEnumerator DelayAction(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }

        public GameObject[] 测试用的所有对象;

        public void 改变测试用的所有对象的可见性(bool 可见与否)
        {
            foreach (var g in 测试用的所有对象)
            {
                g.SetActive(可见与否);
            }
        }
    }
}