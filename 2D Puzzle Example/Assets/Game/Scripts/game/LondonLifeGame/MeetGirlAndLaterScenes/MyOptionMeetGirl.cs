using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyOptionMeetGirl : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    Canvas canvas;

    RectTransform _rectTrans;

    [SerializeField]
    RectTransform target;
    [SerializeField]
    float distanceThreshold = 50;

    private Vector2 _startPos;

    void Start()
    {
        _startPos = _rectTrans.anchoredPosition;
    }

    private void Awake()
    {
        _rectTrans = GetComponent<RectTransform>();
        GetComponent<Image>().raycastTarget = true;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = false;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        _rectTrans.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
        CheckDragResult();
    }

    void CheckDragResult()
    {
        _rectTrans.DOKill();
        var dir = target.anchoredPosition - _rectTrans.anchoredPosition;
        Debug.Log(dir.magnitude);
        if (dir.magnitude < distanceThreshold)
        {
            //拖到了
            GetComponent<Image>().DOFade(0, 1);
            this.enabled = false;
            MeetGirlSystem.instance.EndGirlBegScene();
        }
        else
        {
            _rectTrans.DOAnchorPos(_startPos, 0.5f);
        }
    }


}