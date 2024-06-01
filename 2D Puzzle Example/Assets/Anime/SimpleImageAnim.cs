using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Anime
{
    public class SimpleImageAnim : MonoBehaviour
    {
        public Sprite[] sps;

        public float duration;
        public float delay;

        private int _index;
        public bool invertOrder;
        public Image img;

        void Start()
        {
            _index = 0;
            if (invertOrder)
                _index = sps.Length - 1;
            StartCoroutine(PlayAnimationCoroutine());
        }

        IEnumerator PlayAnimationCoroutine()
        {
            SetImage();

            yield return new WaitForSeconds(delay);

            while (true)
            {
                yield return null;
                yield return null;
                _index++;
                if (invertOrder)
                {
                    if (_index >= sps.Length)
                        _index = 0;
                }
                else
                {
                    if (_index < 0)
                        _index = sps.Length - 1;
                }

                SetImage();
                yield return new WaitForSeconds(duration);
            }
        }

        void SetImage()
        {
            img.sprite = sps[_index];
        }
    }
}