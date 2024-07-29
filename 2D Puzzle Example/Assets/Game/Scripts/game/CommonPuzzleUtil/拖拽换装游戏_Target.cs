using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class 拖拽换装游戏_Target : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public enum 拖拽换装游戏_SubType
    {
        Cloth,
        Shoe,
        Hair,
        Pant,
    }

    public 拖拽换装游戏_SubType subType;

    private Canvas _canvas;

    [HideInInspector]
    public RectTransform rectTrans;

    [SerializeField]
    RectTransform draggingParent;

    public 拖拽换装游戏_Container[] containers;
    [SerializeField]
    拖拽换装游戏_Container _startDDC;

    [HideInInspector]
    public 拖拽换装游戏_Container lastDDC;

    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();

        lastDDC = _startDDC;
        SetToDragDropContainer(_startDDC, false);
    }

    public void SetToDragDropContainer(拖拽换装游戏_Container ddc, bool swap = true)
    {
        if (ddc == null)
            return;

        ddc.ReceiveTarget(this, swap);
        GetComponent<Image>().raycastTarget = true;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = false;
        rectTrans.SetParent(draggingParent.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTrans.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        拖拽换装游戏_Container endContainer = null;
        foreach (var ddc in containers)
        {
            if (ddc.inside)
            {
                endContainer = ddc;
                break;
            }
        }

        SetToDragDropContainer(endContainer == null ? lastDDC : endContainer);

        foreach (var ddc in containers)
        {
            if (ddc != endContainer)
                ddc.inside = false;
        }
    }
}