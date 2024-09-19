using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoorDraggable : 固定方向拖拽
{
    public float durationOpen = 1;
    public float durationClose = 1;

    public override void OnEndDrag(PointerEventData eventData)
    {
        _img.raycastTarget = true;
        var dist = Vector2.Distance(end2.anchoredPosition, _rectTrans.anchoredPosition);
        OpenDoorSceneSystem.instance.OnDoorOpen(dist);
    }

    public void ToOpenEnd(bool withTween)
    {
        _rectTrans.DOKill();
        if (withTween)
        {
            _img.raycastTarget = false;
            _rectTrans.DOAnchorPos(end1.anchoredPosition, durationOpen).SetEase(Ease.OutBounce).OnComplete(
                () => { _img.raycastTarget = true; }
                );
        }
        else
        {
            _rectTrans.anchoredPosition = end1.anchoredPosition;
        }
    }

    public void ToCloseEnd(bool withTween)
    {
        _rectTrans.DOKill();
        if (withTween)
        {
            _img.raycastTarget = false;
            _rectTrans.DOAnchorPos(end2.anchoredPosition, durationClose).SetEase(Ease.OutBounce).OnComplete(
                () => { _img.raycastTarget = true; }
                );
        }
        else
        {
            _rectTrans.anchoredPosition = end2.anchoredPosition;
        }
    }
}