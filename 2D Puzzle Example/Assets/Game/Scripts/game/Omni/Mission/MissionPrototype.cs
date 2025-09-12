using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.game.Omni.Mission
{
    [CreateAssetMenu]
    public class MissionPrototype : ScriptableObject
    {
        public int order;
        public string title;
        public Sprite agentPhoto;
        [Multiline]
        public string desc;

        public enum Type
        {
            None,
            TextInfo,
            VideoRecord,
            VoiceRecord,
            Map,
            Map2,
        }

        public Type type;
    }
}