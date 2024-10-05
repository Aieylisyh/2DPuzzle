using com;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class RLSystem : MonoBehaviour
{
    public GameObject corriderView;
    public void OnClickDestination()
    {
        UiImageScreenFader.instance.FadeWhite(
         () => { corriderView.SetActive(true); },
         0.4f, 0.4f, 0.1f
         );

    }

    public void OnClickOther()
    {
        Debug.Log("OnClickOther");
        var s = new List<string>();
        s.Add("I should find Mrs.Fernandes' office...");
        DialogBehaviour.instance.SetDialog(s);
        DialogBehaviour.instance.Show();
    }

    public void InitCorridorScene()
    {
        door_chinese.SetActive(false);
        door_spanish.SetActive(false);
        door_german.SetActive(false);
        door_french.SetActive(false);
        btnBackToCorrider.SetActive(false);

        corriderView.SetActive(false);
        //ToggleContinueButton(false);
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

    public GameObject door_chinese;
    public GameObject door_spanish;
    public GameObject door_german;
    public GameObject door_french;

    public void OnClickDoor(GameObject g)
    {
        ShowDoorOfSomeOffice(g);
    }

    void ShowDoorOfSomeOffice(GameObject door)
    {
        UiImageScreenFader.instance.FadeWhite(
    () => { door.SetActive(true); },
    0.35f, 0.35f, 0.1f
    );

        StartCoroutine(DelayAction(2, () => { btnBackToCorrider.SetActive(true); }));
    }

    [SerializeField] GameObject btnBackToCorrider;

    public void OnClickBtnBackToCorrider()
    {
        door_chinese.SetActive(false);
        door_spanish.SetActive(false);
        door_german.SetActive(false);
        door_french.SetActive(false);
        btnBackToCorrider.SetActive(false);
    }
}