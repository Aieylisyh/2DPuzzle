﻿using com;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class RLSystem : MonoBehaviour
{
    public GameObject pp11;

    public void OnClick1213()
    {
        Debug.Log("OnClickOther");
        var s = new List<string>();
        s.Add("I should find Mrs.Fernandes' office...");
        DialogBehaviour.instance.SetDialog(s);
        DialogBehaviour.instance.Show();
    }

    public void InitAdmissionEndingScene()
    {

        var s = new List<string>();
        s.Add("It's been a while since I took Mrs.Fernandes' Spanish class.");
        s.Add("Where's her classroom again?");
        DialogBehaviour.instance.SetDialog(s);
        DialogBehaviour.instance.SetCallback(
         () =>
         {
             //
         }
         );
        DialogBehaviour.instance.Show();
    }
}