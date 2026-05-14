using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Linyifei_Game.Script.Onmi_sys
{
    public class RoomTrigger : MonoBehaviour
    {
        public UnityEvent evt;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("RoomTrigger Entered by Player");
                evt.Invoke();
                this.gameObject.SetActive(false);
            }
        }
    }
}