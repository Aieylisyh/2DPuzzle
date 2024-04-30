using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    public float time;
    public Text timeText;
    public bool counting = true;
    void Start()
    {
        timeText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(counting)
        {
            time += Time.deltaTime;
        }
        timeText.text = time.ToString("F0");
    }
}