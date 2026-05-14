using System.Collections;
using UnityEngine;

namespace Assets.Linyifei_Game.Script.Onmi_sys
{
    public class GlobalInputState : MonoBehaviour
    {
        public static bool isMouse0ConsumedThisFrame = false;

        // Update is called once per frame
        void LateUpdate()
        {
            isMouse0ConsumedThisFrame = false;
        }
    }
}