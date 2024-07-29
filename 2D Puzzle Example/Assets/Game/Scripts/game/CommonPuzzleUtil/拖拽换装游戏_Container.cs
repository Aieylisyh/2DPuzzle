using UnityEngine;
using UnityEngine.EventSystems;

public class 拖拽换装游戏_Container : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, ICancelHandler
{
    [HideInInspector]
    public bool inside;
    [HideInInspector]
    public RectTransform rectTrans;

    [System.Serializable]
    public class 拖拽换装游戏_Ref
    {
        public 拖拽换装游戏_Target.拖拽换装游戏_SubType type;
        public RectTransform rt;
        public 拖拽换装游戏_Target currentTarget;
    }


    public 拖拽换装游戏_Ref[] refs;

    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();

    }

    public 拖拽换装游戏_Ref GetRef(拖拽换装游戏_Target tb)
    {
        foreach (var r in refs)
            if (r.type == tb.subType) return r;
        return null;
    }

    public void ReceiveTarget(拖拽换装游戏_Target tb, bool swap)
    {
        if (refs == null)
            return;

        //  Debug.Log(gameObject.name + " Container ReceiveTarget " + tb.gameObject.name);

        tb.rectTrans.SetParent(transform);
        var reference = GetRef(tb);
        var lastDDC = tb.lastDDC;
        // Debug.Log(lastDDC.gameObject.name);
        tb.lastDDC = this;
        var lastTarget = reference.currentTarget;
        reference.currentTarget = tb;
        tb.rectTrans.anchoredPosition = reference.rt.anchoredPosition;

        if (swap && reference.currentTarget != null)
        {
            //    Debug.Log("swap");
            lastTarget.SetToDragDropContainer(lastDDC, false);
        }
    }

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
