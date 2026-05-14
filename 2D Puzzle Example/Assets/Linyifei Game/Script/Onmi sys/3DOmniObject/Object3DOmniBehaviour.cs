using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Linyifei_Game.Script.Onmi_sys
{
    public class Object3DOmniBehaviour : MonoBehaviour
    {
        public Outline outline;
        [HideInInspector]
        public bool isInteracting;
        public UnityEvent extraEvt;
        private void Awake()
        {
            RemoveHighlight();
        }
        public virtual void OnInteract()
        {
            Debug.Log("Interacted with " + gameObject.name);
        }

        public void Highlight()
        {
            if (outline != null)
            {
                outline.enabled = true;
            }
        }

        public void RemoveHighlight()
        {
            if (outline != null)
            {
                outline.enabled = false;
            }
        }
    }
}