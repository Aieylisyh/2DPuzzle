using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.Diary
{
    public class DiaryDebugger : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("1"))
                Test1();
            else if (Input.GetKeyDown("2"))
                Test2();
            else if (Input.GetKeyDown("3"))
                Test3();
            else if (Input.GetKeyDown("4"))
                Test4();
            else if (Input.GetKeyDown("5"))
                Test5();
            else if (Input.GetKeyDown("6"))
                Test6();
            else if (Input.GetKeyDown("7"))
                Test7();
            else if (Input.GetKeyDown("8"))
                Test8();
            else if (Input.GetKeyDown("9"))
                Test9();
            else if (Input.GetKeyDown("0"))
                Test0();
        }

        void Test1()
        {
            DiaryGameSystem.instance.cameraController.TurnTo(0, 0, false);
        }

        void Test2()
        {
            DiaryGameSystem.instance.cameraController.TurnTo(1, 1, false);
        }

        void Test3()
        {
            DiaryGameSystem.instance.cameraController.TurnTo(1, 0);
        }

        void Test4()
        {
            DiaryGameSystem.instance.cameraController.TurnTo(1, -1);
        }

        void Test5()
        {
            DiaryGameSystem.instance.cameraController.TurnTo(0, 1);
        }
        void Test6()
        {
            DiaryGameSystem.instance.cameraController.TurnTo(0, -1);
        }

        void Test7()
        {
            DiaryGameSystem.instance.cameraController.TurnTo(-1, 1);
        }

        void Test8()
        {
            DiaryGameSystem.instance.cameraController.TurnTo(-1, 0);
        }

        void Test9()
        {
            //fireworks view
            //DiaryGameSystem.instance.cameraController.TurnTo(-1, -1);
            DiaryGameSystem.instance.cameraController.TurnTo(DiaryGameSystem.instance.cameraController.ref_comedy, true);
        }

        void Test0()
        {
            //focus view
            //DiaryGameSystem.instance.cameraController.TurnTo(0, 0);
            DiaryGameSystem.instance.cameraController.TurnTo(DiaryGameSystem.instance.cameraController.ref_focus, false);
        }
    }
}