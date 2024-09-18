using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropContainer : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, ICancelHandler
{
    [HideInInspector]
    public bool inside;

    public RectTransform place
    {
        get
        {
            if (_goodPlaceRefs != null && _goodPlaceRefs.Length > 0)
            {
                while (_goodPlaceRefsIndex >= _goodPlaceRefs.Length)
                {
                    _goodPlaceRefsIndex -= _goodPlaceRefs.Length;
                }

                return _goodPlaceRefs[_goodPlaceRefsIndex++];
            }
            return _goodPlaceRef;
        }
    }
    [SerializeField] RectTransform _goodPlaceRef;
    [SerializeField] RectTransform[] _goodPlaceRefs;
    int _goodPlaceRefsIndex;
    public void OnCancel(BaseEventData eventData)
    {
        inside = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter " + this);
        inside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inside = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
