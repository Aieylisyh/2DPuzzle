using Assets.EndlessBook.Demos.Demo_02.Scripts;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DiaryDragResponser : MonoBehaviour
{
    public float range;
    public bool blocked;
    public Transform center;

    public UnityEvent OnReceiveDragEvt;
    public enum ReceiveHandleMethod
    {
        None,
        Disactive,
        Other,
    }
    public ReceiveHandleMethod receiveHandleMethod;

    private void Start()
    {
        blocked = false;
    }

    public bool CheckDragEnd(DiaryDraggable d)
    {
        var distance = GetDistance(d.transform);
        if (distance < range)
        {
            switch (receiveHandleMethod)
            {
                case ReceiveHandleMethod.None:
                    break;
                case ReceiveHandleMethod.Disactive:
                    d.gameObject.SetActive(false);
                    break;
                case ReceiveHandleMethod.Other:
                    break;
            }
            d.EndEvt?.Invoke();
            return true;
        }

        return false;
    }

    float GetDistance(Transform other)
    {
        var p1 = other.position;
        var p2 = (center == null) ? transform.position : center.position;
        var d = p1 - p2;
        d.z = 0;
        return d.magnitude;
    }
}