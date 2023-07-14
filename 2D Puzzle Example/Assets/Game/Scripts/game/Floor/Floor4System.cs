using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using com;

public class Floor4System : MonoBehaviour
{
    public static Floor4System instance;

    public ClickSpeak speaker;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartPlay();
    }

    void StartPlay()
    {

    }

    void OnClickMan()
    {
        speaker.Speak();
    }
}