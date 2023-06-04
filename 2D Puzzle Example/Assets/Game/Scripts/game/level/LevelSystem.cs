using System.Collections;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem instance;
    // Use this for initialization
    void Start()
    {
        instance = this;
    }


    public void OnClickTestKey()
    {
        TipSystem.instance.ShowText("This is a key, but it is rusty...", true);
    }
}