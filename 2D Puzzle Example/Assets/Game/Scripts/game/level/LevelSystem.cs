using System.Collections;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem instance;



    // Use this for initialization
    void Awake()
    {
        instance = this;
    }


    public void OnClickTestKey()
    {
        TipSystem.instance.ShowText("This is a key, but it is rusty...", true);

        InventorySystem.instance.AddItem(new ItemData(1, "key"));
    }

    public void OnClickTestCandle()
    {
        TipSystem.instance.ShowText("???...", true);

        InventorySystem.instance.AddItem(new ItemData(1, "candle"));
    }


}