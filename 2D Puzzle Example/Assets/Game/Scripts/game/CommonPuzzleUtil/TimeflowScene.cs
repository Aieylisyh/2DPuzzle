using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeflowScene : MonoBehaviour
{
    [System.Serializable]
    public struct TimeflowObject
    {
        public float startTime;
        public float endTime;
        public GameObject obj;
        public bool isToShow;
    }
    [System.Serializable]
    public struct TimeflowOpatiqueImage
    {
        public float startTime;
        public float endTime;
        public Image img;
        public bool isToShow;
    }

    [SerializeField]
    TimeflowOpatiqueImage[] timeflowOpatiqueImages;
    [SerializeField]
    TimeflowObject[] TimeflowObjects;


    public void Tick(float t)
    {
        foreach (var o in TimeflowObjects)
        {
            if (t >= o.startTime && t <= o.endTime)
            {
                o.obj.SetActive(o.isToShow);
            }
            else
            {
                o.obj.SetActive(!o.isToShow);
            }
        }

        foreach (var i in timeflowOpatiqueImages)
        {
            var c = i.img.color;
            if (t >= i.startTime && t <= i.endTime)
            {
                var ratio = (t - i.startTime) / (i.endTime - i.startTime);
                if (i.isToShow)
                {
                    c.a = Mathf.Lerp(0, 1, ratio);
                }
                else
                {
                    c.a = Mathf.Lerp(1, 0, ratio);
                }
            }
            else if (t < i.startTime)
            {
                if (i.isToShow)
                {
                    c.a = 0;
                }
                else
                {
                    c.a = 1;
                }
            }
            else if (t > i.endTime)
            {
                if (i.isToShow)
                {
                    c.a = 1;
                }
                else
                {
                    c.a = 0;
                }
            }
            i.img.color = c;
        }
    }
}
