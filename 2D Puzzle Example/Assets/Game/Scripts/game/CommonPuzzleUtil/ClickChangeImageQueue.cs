using System.Collections;
using UnityEngine;

public class ClickChangeImageQueue : MonoBehaviour
{
    public GameObject[] queue;
    int crtIndex;

    private void Start()
    {
        Set(0);
    }

    public void Next()
    {
        Set(crtIndex + 1);
    }

    public void Set(int index)
    {
        crtIndex = index;
        var len = queue.Length;
        var i = index % len;

        for (int j = 0; j < len; j++)
        {
            queue[j].SetActive(j == i);
        }
    }

    public void OnClick()
    {
        Next();
    }
}