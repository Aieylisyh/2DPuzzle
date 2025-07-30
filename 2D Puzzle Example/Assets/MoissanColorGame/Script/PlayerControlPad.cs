using System.Collections;
using UnityEngine;

namespace Assets.MoissanColorGame.Script
{
    public class PlayerControlPad : MonoBehaviour
    {

        public void ClickMoveRight()
        {
            PlayerController.instance.MoveRight();
        }

        public void ClickMoveLeft()
        {
            PlayerController.instance.MoveLeft();
        }
    }
}