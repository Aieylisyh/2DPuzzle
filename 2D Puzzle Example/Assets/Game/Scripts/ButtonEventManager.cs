using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class ButtonEventManager : MonoBehaviour
    {
        public void 创建苹果()
        {
            AppleManager.instance.CreateApples();
        }

        public void 删除苹果()
        {
            AppleManager.instance.DeleteApples();
        }
    }
}