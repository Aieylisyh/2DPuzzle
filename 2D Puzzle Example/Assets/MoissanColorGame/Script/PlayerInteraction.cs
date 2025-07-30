using System.Collections;
using UnityEngine;

namespace Assets.MoissanColorGame.Script
{
    public class PlayerInteraction : MonoBehaviour
    {
        public SpriteRenderer sr;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnMouseOver()
        {
            //Debug.Log("OnMouseOver");
            sr.color = new Color(0.9f, 0.9f, 0.9f, 1);
        }

        private void OnMouseExit()
        {
            sr.color = new Color(1, 1, 1, 1);
        }

        private void OnMouseDown()
        {
            //Debug.Log("OnMouseDown");
            InventoryColorPalette.instance.Show();
        }
    }
}