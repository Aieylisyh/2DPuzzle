using System.Collections;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem instance;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("debug fast OnClickExitJisawScene");
            Floor5System.instance.OnClickExitJisawScene();
            Floor5System.instance.RevealList();
        }
    }

    public void OnClickTestKey()
    {
        TipSystem.instance.ShowText("This is a key, but it is rusty...", true);

        InventorySystem.instance.AddItem(new ItemData(1, "key"));
    }

    public void OnClickCandle()
    {
        InventorySystem.instance.AddItem(new ItemData(1, "candle"));
    }
}