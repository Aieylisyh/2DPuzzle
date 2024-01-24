using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDropTarget : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    Canvas canvas;

    RectTransform rectTrans;

    [SerializeField]
    RectTransform draggingParent;

    public DragDropContainer[] containers;
    [SerializeField]
    DragDropContainer _startDDC;
    Vector3 _startPos;

    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
    }

    private void Start()
    {
        SetToDragDropContrainer(_startDDC);
    }

    void SetToDragDropContrainer(DragDropContainer ddc)
    {
        _startDDC = ddc;
        rectTrans.SetParent(ddc.transform);
        rectTrans.anchoredPosition = ddc.goodPlaceRef.anchoredPosition;
        GetComponent<Image>().raycastTarget = true;

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPos = rectTrans.anchoredPosition;
        GetComponent<Image>().raycastTarget = false;
        rectTrans.SetParent(draggingParent.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTrans.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragDropContainer endContainer = null;
        foreach (var ddc in containers)
        {
            if (ddc.inside)
            {
                Debug.Log("OnEndDrag " + ddc);
                endContainer = ddc;
                break;
            }
        }

        if (endContainer == null)
        {
            SetToDragDropContrainer(_startDDC);
        }
        else
        {
            SetToDragDropContrainer(endContainer);
            //this.enabled = false;//depends
        }

        foreach (var ddc in containers)
        {
            if (ddc != endContainer)
                ddc.inside = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
