using com;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class RLSystem : MonoBehaviour
{
    public void InitClassroom2Scene()
    {
        /*

I: Yes, here it is.
Mrs. Fernandes: Awesome, I will check this list and I will start writing those letters.
I: Thank you so much, Mrs. Fernandes.
Mrs. Fernandes: You're welcome. Goodbye.
I: Bye.

        */

        ScreenEffectToggle.instance.ToggleDrunk(true);
        var s = new List<string>();
        s.Add("<color=#AA2200><b>Mrs. Fernandes: </b></color>Hello again young man.");
        s.Add("<color=#AA2200><b>Mrs. Fernandes: </b></color>I remember you want to talk about your recommendation letter?");
        s.Add(" ");
        s.Add("<color=#000000><b>I: </b></color>Yes, Mrs. Fernandes, I took your Spanish class two yeas ago.");
        s.Add("<color=#000000><b>I: </b></color>I hope you still remember me.");
        s.Add(" ");
        s.Add("<color=#AA2200><b>Mrs. Fernandes: </b></color>Of course, you did a great job on my class.");
        s.Add("<color=#AA2200><b>Mrs. Fernandes: </b></color>I am happy to write some recommendation letters for you, do you have all the list of the university you want to apply for?");
        s.Add(" ");
        s.Add("<color=#000000><b>I: </b></color>Yes, here it is.");
        s.Add("<color=#000000><b>I: </b></color>And also, I remember you love <color=#887705><b>chocolates</b></color>, I brought you a bar of chocolate from the school's vending machine, I hope you like it.");
        s.Add(" ");
        s.Add("<color=#AA2200><b>Mrs. Fernandes: </b></color>Awesome, that is really kind, I am going to enjoy it, thank you! ");
        s.Add("<color=#AA2200><b>Mrs. Fernandes: </b></color>I will check this list and <color=#A02244>start writing those letters.</color>");
        s.Add(" ");
        s.Add("<color=#000000><b>I: </b></color>Thank you so much, Mrs. Fernandes.");
        s.Add(" ");
        s.Add("<color=#AA2200><b>Mrs. Fernandes: </b></color>You're welcome. Is there anything else I can help you?");
        s.Add(" ");
        s.Add("<color=#000000><b>I: </b></color>No, that’s it, goodbye!");
        s.Add(" ");
        s.Add("<color=#AA2200><b>Mrs. Fernandes: </b></color>Bye!");

        DialogBehaviour.instance.SetDialog(s);
        DialogBehaviour.instance.SetCallback(
         () =>
         {
             ScreenEffectToggle.instance.ToggleDrunk(false);
             ToggleContinueButton(true);
         }
         );
        DialogBehaviour.instance.Show();
    }
}