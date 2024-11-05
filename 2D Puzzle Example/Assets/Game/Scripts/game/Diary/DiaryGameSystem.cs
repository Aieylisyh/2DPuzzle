using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.Diary
{
    public class DiaryGameSystem : MonoBehaviour
    {
        public static DiaryGameSystem instance;
        public DiaryCameraController cameraController;



        private void Awake()
        {
            instance = this;
        }


    }
}