using com;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class RLSystem : MonoBehaviour
{
    public GameObject pp2;

    public void OnClick贩卖机()
    {
        var s = new List<string>();
        s.Add("Maybe I should give Mrs.Fernandes something she likes...");
        DialogBehaviour.instance.SetDialog(s);
        DialogBehaviour.instance.Show();
    }

    public void OnClick其他货物()
    {
        var s = new List<string>();
        s.Add("I don't think she likes this...");
        DialogBehaviour.instance.SetDialog(s);
        DialogBehaviour.instance.Show();
    }
    public void OnClick巧克力()
    {
        var s = new List<string>();
        s.Add("Yes, I remember there were chocolates on her desk!");
        DialogBehaviour.instance.SetCallback(
       () =>
       {
           ToggleContinueButton(true);
       }
       );
        DialogBehaviour.instance.SetDialog(s);
        DialogBehaviour.instance.Show();
    }

    public void InitVendingMachineScene()
    {

        var s = new List<string>();
        s.Add("<color=#555555>(In front of Vending Machine)</color>");
        s.Add("May be I should by some snacks for Mrs. Fernandes, what does she like to eat?");
        DialogBehaviour.instance.SetDialog(s);

        DialogBehaviour.instance.Show();
    }
}