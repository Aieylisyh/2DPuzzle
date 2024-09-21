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

    [SerializeField] Transform[] onDeskItems;

    public void InitCalenderScene()
    {
        /*
        fade in black, wear clother sound
        fade out show desk
        user click on papers
        all papers clicked, then focus on calender

        user click calender, then show big calender
        show dialogs
        show continue button, user click continue button

        show check list
        user click first item
        show checkmark
        show continue button, user click continue button

        fade in black, walk sound
        fade out show map
        show dialogs Which room should I go?...
        user click on a room in map.

         */
    }
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

    public void OnClickChecklistItem(Transform t)
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
