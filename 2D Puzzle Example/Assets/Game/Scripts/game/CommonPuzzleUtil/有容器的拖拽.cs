﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class 有容器的拖拽 : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    protected Canvas canvas;

    protected RectTransform _rectTrans;

    [SerializeField]
    protected RectTransform draggingParent;

    public DragDropContainer[] containers;
    [SerializeField]
    protected DragDropContainer _startDDC;

    private void Awake()
    {
        _rectTrans = GetComponent<RectTransform>();
        SetToDragDropContainer(_startDDC);
    }

    protected virtual void SetToDragDropContainer(DragDropContainer ddc)
    {
        _startDDC = ddc;
        if (ddc != null)
        {
            _rectTrans.SetParent(ddc.transform);
            _rectTrans.anchoredPosition = ddc.place.anchoredPosition;
        }

        GetComponent<Image>().raycastTarget = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = false;
        _rectTrans.SetParent(draggingParent.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTrans.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragDropContainer endContainer = null;
        foreach (var ddc in containers)
        {
            if (ddc.inside)
            {
                endContainer = ddc;
                break;
            }
        }

        SetToDragDropContainer(endContainer == null ? _startDDC : endContainer);

        foreach (var ddc in containers)
        {
            if (ddc != endContainer)
                ddc.inside = false;
        }
    }
}