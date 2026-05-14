using Assets.Linyifei_Game.Script.Onmi_sys;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door3DOmniBehaviour : Object3DOmniBehaviour
{
    public Transform door;
    public float duration = 1.5f;

    private bool openning = false;
    public bool locked;

    public void SetLocked(bool value)
    {
        Debug.Log("Door locked state changed to: " + value);
        locked = value;
    }

    public void OpenOut()
    {
        if (locked) return;
        door.DOKill();
        door.DOLocalRotate(new Vector3(0, 90, 0), duration, RotateMode.Fast).SetEase(Ease.OutBounce);
        openning = true;
    }

    public void OpenIn()
    {
        if (locked) return;
        door.DOKill();
        door.DOLocalRotate(new Vector3(0, -90, 0), duration, RotateMode.Fast).SetEase(Ease.OutBounce);
        openning = true;
    }
    public void Close()
    {
        door.DOKill();
        door.DOLocalRotate(new Vector3(0, 0, 0), duration, RotateMode.Fast).SetEase(Ease.OutCubic);
        openning = false;
    }

    public override void OnInteract()
    {
        isInteracting = false;
        if (openning)
        {
            Close();
        }
        else
        {
            OpenIn();
        }
    }

    public GameObject room1;
    public GameObject room2;
    public GameObject room3;
    public GameObject room4;
    public void ChangeRoom1To3()
    {
        room1.SetActive(false);
        room3.SetActive(true);
    }

    public void ChangeRoom2To4()
    {
        room2.SetActive(false);
        room4.SetActive(true);
    }
    public void ChangeRoom1To3WithDelay()
    {
        StartCoroutine(DelayedAction(ChangeRoom1To3));
    }

    public void ChangeRoom2To4WithDelay()
    {
        StartCoroutine(DelayedAction(ChangeRoom2To4));
    }

    IEnumerator DelayedAction(Action a)
    {
        yield return new WaitForSeconds(duration);
        a?.Invoke();
    }
}
