using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Game.Scripts.game.LondonLifeGame.Cook
{
    public class DragToCookBehaviour : 简单自由拖拽
    {
        [SerializeField] Vector2 _centerPos;
        Vector2 _startPos;
        [SerializeField] float _maxRange;

        float _progress;
        [SerializeField] float _totalProgress;

        [SerializeField] Image _bar;

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);

            _startPos = eventData.position;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            var deltaVector = eventData.position - _centerPos;
            var dist = deltaVector.magnitude;
            if (dist > _maxRange)
            {
                //do nothing
                _progress = 0;
            }
            else
            {
                base.OnDrag(eventData);
            }

            base.OnDrag(eventData);
            SyncProgress();
        }

        void SyncProgress()
        {
            var r = _progress / _totalProgress;
            _bar.fillAmount = r;
        }
    }
}