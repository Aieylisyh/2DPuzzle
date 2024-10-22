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

    [SerializeField] PanelCanvasGroupSwitcher _pcgs;
    private void Awake()
    {
        instance = this;
    }

    public void Reinit()
    {
        _pcgs.Show(true, true);
        StopAllCoroutines();
        doorDraggable.ToCloseEnd(false);
        SceneTextSystem.instance.SetText(2, false);
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
        yield return new WaitForSeconds(1);
        var duration = 3.5f;
        toFocusRect.DOScale(scaleRatio, duration);
        toFocusRect.DOAnchorPos(-focus.anchoredPosition, duration);
        yield return new WaitForSeconds(2.5f);
        _pcgs.Show(false, false);
        yield return new WaitForSeconds(2);
        InRoomClockScene.instance.Reinit();
    }
}