using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class LiftDoor
{
    public RectTransform door;

    public float openX;
    public float closeX;
    public float duration;

    public void Set(bool toOpen, bool instant)
    {
        var targetX = toOpen ? openX : closeX;
        if (instant)
        {
            door.DOKill();
            var pos = door.anchoredPosition;
            pos.x = targetX;
            door.anchoredPosition = pos;
        }
        else
        {
            door.DOKill();
            door.DOAnchorPosX(targetX, duration).SetEase(Ease.InOutCubic);
        }
    }
}