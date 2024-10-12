using com;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class RLSystem : MonoBehaviour
{
    public CanvasGroup classroom1DialogBubble;

    public void OnClickDialogBubble()
    {
        classroom1DialogBubble.gameObject.SetActive(false);

        Debug.Log("OnClickDialogBubble");
        var s = new List<string>();
        s.Add("<color=#AA2200><b>Mrs. Fernandes: </b></color>So this is the reason why this question should be a B, not C, got it?");
        s.Add("<color=#22AA00><b>Other Student: </b></color>Got it, thank you Mrs. Fernandes, but I got another question on number 34.");
        s.Add(" ");
        s.Add("<color=#000000><b>I: </b></color>Excuse me.");
        s.Add("<color=#AA2200><b>Mrs. Fernandes: </b></color>Hello. How can I help you?");
        s.Add("<color=#000000><b>I: </b></color>Sorry to bother,");
        s.Add("<color=#000000><b>I: </b></color>but I am wondering if you could please write me a recommendation letter?");
        s.Add(" ");
        s.Add("<color=#AA2200><b>Mrs. Fernandes: </b></color>Absolutely, I am happy to do that, just not now.");
        s.Add("<color=#AA2200><b>Mrs. Fernandes: </b></color>As you can see, I am helping Thomas here.");
        s.Add("<color=#AA2200><b>Mrs. Fernandes: </b></color>Can you come after school if that is fine for you?");
        s.Add("<color=#000000><b>I: </b></color>Of course, I will see you then!");
        s.Add("<color=#AA2200><b>Mrs. Fernandes: </b></color>See you.");

        DialogBehaviour.instance.SetCallback(
       () =>
       {
           ToggleContinueButton(true);
       }
       );


        DialogBehaviour.instance.SetDialog(s);
        DialogBehaviour.instance.Show();
    }

    public void InitClassroom1Scene()
    {
        todolist_classroom1.SetActive(true);
    }

    [SerializeField] GameObject todolist_classroom1;
    void OnTodoList2Checked()
    {
        var fadeToBlackTime = 1.2f;
        UiImageScreenFader.instance.FadeInBlack(null, fadeToBlackTime);
        StartCoroutine(DelayAction(fadeToBlackTime, () =>
        {
            todolist_classroom1.SetActive(false);
            ClassRoom1KnockDoorDialog();
        }));
    }

    void ClassRoom1KnockDoorDialog()
    {
        var s = new List<string>();
        s.Add("Well, take a deep breath");
        s.Add("knock on the door and go in and politely ask her if you can get a letter of recommendation from her.");
        s.Add("I've practiced this many times.");
        s.Add("It's no problem.");
        DialogBehaviour.instance.SetDialog(s);
        DialogBehaviour.instance.SetCallback(
         () =>
         {
             SoundSystem.instance.Play("knock door");
             StartCoroutine(DelayAction(2.5f, () =>
                {
                    UiImageScreenFader.instance.FadeOutBlack(null);
                    classroom1DialogBubble.DOFade(1, 2).SetDelay(3.0f);
                }));
         }
         );
        DialogBehaviour.instance.Show();
    }
}