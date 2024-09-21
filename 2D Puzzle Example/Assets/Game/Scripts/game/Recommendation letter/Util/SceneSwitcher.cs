using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] SceneDefinition[] _scenes;
    public SceneId crtId;

    [System.Serializable]
    public class SceneDefinition
    {
        public SceneId id;
        public GameObject go;
    }

    void Start()
    {
        Set(crtId);
    }

    public void Set(SceneId s)
    {
        foreach (var scene in _scenes)
        {
            bool b = scene.id == s;
            scene.go.SetActive(b);
            if (b)
            {
                RLSystem.instance.StartScene(s);
            }
        }
        crtId = s;
    }
}