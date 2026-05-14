using Assets.Linyifei_Game.Script.Onmi_sys;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine3DOmniBehaviour : Object3DOmniBehaviour
{
    public GameObject[] targets;
    public FirstPersonController fpc;
    public bool isDisplaying;
    public override void OnInteract()
    {
        if (!isDisplaying)
        {
            Show();
        }
    }

    void Show()
    {
        isInteracting = true;
        StartDisplay();
        extraEvt?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isDisplaying && !GlobalInputState.isMouse0ConsumedThisFrame)
        {
            EndDisplay();
            GlobalInputState.isMouse0ConsumedThisFrame = true;
        }
    }

    void StartDisplay()
    {
        Debug.Log("StartDisplay called");
        isDisplaying = true;
        foreach (var t in targets)
        {
            t.SetActive(true);
        }

        fpc.cameraCanMove = false;
        fpc.playerCanMove = false;
    }

    void EndDisplay()
    {
        Debug.Log("EndDisplay called");
        isDisplaying = false;
        foreach (var t in targets)
        {
            t.SetActive(false);
        }

        fpc.cameraCanMove = true;
        fpc.playerCanMove = true;
        isInteracting = false;
    }
}
