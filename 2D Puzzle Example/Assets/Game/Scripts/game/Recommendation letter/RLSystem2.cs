using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class RLSystem : MonoBehaviour
{
    [SerializeField] Image _blackScreen;

    [SerializeField] CanvasGroup desk;

    [SerializeField] CanvasGroup calendar;

    // [SerializeField] 

    [SerializeField] CanvasGroup checkedList;

    [SerializeField] CanvasGroup map;

    public void OnClickPaperOnDesk()
    {

    }

    public void OnCalender()
    {

    }

    public void OnClickExitCalender()
    {
        //Show check list
    }

    public void OnClickChecklistItem()
    {
        //check the 1st item
        //todo add some words
    }

    public void OnClickExitChecklist()
    {
        //black screen walk sound
        //show map to choose go where
    }

    public void OnClickDestination()
    {

    }
}
