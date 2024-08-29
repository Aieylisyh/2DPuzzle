using System;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.LondonLifeGame.PlaneScene
{
    public class PlaneSceneGameSystem : MonoBehaviour
    {
        public DreamBubble.气泡参数[] 气泡们;

        // Use this for initialization
        void Start()
        {
            float t = 0;
            foreach (var p in 气泡们)
            {
                StartCoroutine(DelayAction(t, ));
            }
        }

        IEnumerator DelayAction(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }

        void CreateBubble()
        {

        }
    }
}