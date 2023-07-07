using UnityEngine;

public class LiftDoors : MonoBehaviour
{
    public LiftDoor doorRight;
    public LiftDoor doorLeft;

    public bool DoorsClosedAndStopped()
    {
        return doorRight.closedAndStopped && doorLeft.closedAndStopped;
    }
}