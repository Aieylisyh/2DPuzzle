using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TintDemo : MonoBehaviour
{
    Color pickedColor;

    public void Tint(GameObject g)
    {
        Image img = g.GetComponent<Image>();
        //img.color = pickedColor;
        img.CrossFadeColor(pickedColor, 0.5f, false, false);
    }

    public void PickColor(GameObject g)
    {
        Image img = g.GetComponent<Image>();
        pickedColor = img.color;
    }
}