using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.game.LondonLifeGame.PlaneScene
{
    public class PlaneSceneGameSystem : MonoBehaviour
    {
        public static PlaneSceneGameSystem instance;

        GoToPlaneBehaviour _goToPlane;

        public DreamBubble.气泡参数[] 气泡们;
        List<DreamBubble.气泡参数> _待生成的气泡们 = new List<DreamBubble.气泡参数>();


        public float bubbleInterval;
        public DreamBubble bubblePrefab;
        public Transform bubbleParent;

        private void Awake()
        {
            instance = this;
            _goToPlane = GetComponent<GoToPlaneBehaviour>();

            foreach (var p in 气泡们)
                _待生成的气泡们.Add(p);
            foreach (var p in 气泡们)
                _待生成的气泡们.Add(p);
            foreach (var p in 气泡们)
                _待生成的气泡们.Add(p);
        }

        public void StartGenerateBubbles()
        {
            StartCoroutine(生成气泡循环());
        }

        public void AddTo待生成的气泡们(DreamBubble.气泡参数 p)
        {
            _待生成的气泡们.Add(p);
        }

        IEnumerator 生成气泡循环()
        {
            while (true)
            {
                yield return new WaitForSeconds(bubbleInterval);
                if (_待生成的气泡们.Count > 0)
                {
                    CreateBubble(_待生成的气泡们[0]);
                    _待生成的气泡们.RemoveAt(0);
                }
            }
        }

        IEnumerator DelayAction(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }

        void CreateBubble(DreamBubble.气泡参数 p)
        {
            var b = Instantiate(bubblePrefab, bubbleParent);
            b.Init(p);
        }

        public void OnClickBubble()
        {
            _goToPlane.OnClickBubble();
        }
    }
}