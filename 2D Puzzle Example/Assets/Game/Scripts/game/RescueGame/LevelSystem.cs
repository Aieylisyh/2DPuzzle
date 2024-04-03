using com;
using System.Collections;
using UnityEngine;

namespace Rescue
{

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
                //Debug.Log("debug fast OnClickExitJisawScene");

            }
        }

        public void OnClickTestKey()
        {
            //add item will hide the current displaying tip, so must add item before set tip
            // InventorySystem.instance.AddItem(new ItemData(1, "key"));
            // TipSystem.instance.ShowText("This is a key, but it is rusty...", true);
        }
    }
}