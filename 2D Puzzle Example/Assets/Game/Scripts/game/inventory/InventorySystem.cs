using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;

    public List<ItemPrototype> items;
    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    public void AddItem()
    {

    }
}