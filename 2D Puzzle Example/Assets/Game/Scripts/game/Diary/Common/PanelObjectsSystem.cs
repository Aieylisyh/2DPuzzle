using System.Collections;
using UnityEngine;


[System.Serializable]
public class MimicPanelObject
{
    public enum Method
    {
        Up,
        Down,
        Drag,
    }
    public Method method;
    public float delay;
    public float duration;
    public Vector3 offset;//drag from or rotation offset
}

public class PanelObjectsSystem : MonoBehaviour
{
    public static PanelObjectsSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public void InitObject(MimicPanelObject mpo )
    {

    }
}