using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.Omni._3d
{
    public class PlayerInteractor : MonoBehaviour
    {
        public Door crtDoor;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("f"))
            {
                if (crtDoor != null)
                    crtDoor.Open();
            }
        }
    }
}