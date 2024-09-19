using com;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class OpenDoorSceneSystem : MonoBehaviour
{
    public static OpenDoorSceneSystem instance;
    public int needOpenTimes;
    [SerializeField] float _openMinDist;

    int _crtOpenTimes;
    public DoorDraggable doorDraggable;
    public RectTransform focus;
    public RectTransform toFocusRect;
    public float scaleRatio;
    public CanvasGroup innerCg;

    private void Awake()
    {
        instance = this;
    }

    public void Reinit()
    {
        StopAllCoroutines();
        doorDraggable.ToCloseEnd(false);
    }

    public void OnDoorOpen(float openDist)
    {
        Debug.Log("OnDoorOpen " + openDist);
        if (openDist > _openMinDist)
        {
            _crtOpenTimes++;
            if (_crtOpenTimes >= needOpenTimes)
            {
                TriggerInnerScene();
                return;
            }
        }

        CloseDoor();
    }

    void CloseDoor()
    {
        doorDraggable.ToCloseEnd(true);
    }

    void TriggerInnerScene()
    {
        doorDraggable.ToOpenEnd(true);
        StartCoroutine(TriggerInnerSceneCoroutine());
    }

    IEnumerator TriggerInnerSceneCoroutine()
    {
        yield return new WaitForSeconds(doorDraggable.durationOpen);
        doorDraggable.enabled = false;
        innerCg.DOFade(1, 2);
        yield return new WaitForSeconds(2);
        var duration = 3.5f;
        toFocusRect.DOScale(1.4f, duration);
        toFocusRect.DOAnchorPos(-focus.anchoredPosition, duration);
        yield return new WaitForSeconds(3);
        //TODO next scene
    }
}