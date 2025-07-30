using System.Collections;
using UnityEngine;

namespace Assets.MoissanColorGame.Script
{
    public class InventoryColorPalette : MonoBehaviour
    {
        public static InventoryColorPalette instance;

        public GameObject view;
        private void Awake()
        {
            instance = this;
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Show()
        {
            view.SetActive(true);
        }

        public void Hide()
        {
             view.SetActive(false);
        }
    }
}