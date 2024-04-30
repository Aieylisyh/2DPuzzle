using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TintDemo : MonoBehaviour
{
    Color pickedColor;
    public UnityEvent evt;
    public UnityEvent<int> evt_int;
    public UnityEvent<bool> evt_bool;
    public UnityEvent<Color> evt_Color;

    public void Tint(GameObject g)
    {
        Image img = g.GetComponent<Image>();
        img.color = pickedColor;
    }

    public void PickColor(GameObject g)
    {
        Image img = g.GetComponent<Image>();
        pickedColor = img.color;
    }

    public void TestColor(Color c)
    {
        List<GameObject> list;
    }

    public void TestBool(bool b)
    {

    }
}