using UnityEngine;
using UnityEngine.UI;

public class WashFaceSwipeWoundBehaviour : MonoBehaviour
{
    public RectTransform[] wounds;
    public float distanceTolerance = 5;
    public float healAlphaDelta = 0.1f;


    public void OnSwiping(Vector2 pos)
    {
        foreach (var w in wounds)
        {
            var dist = (w.anchoredPosition - pos).magnitude;
            if (dist < distanceTolerance)
            {
                HealWound(w);
            }
        }
    }

    void HealWound(RectTransform wound)
    {
        var img = wound.GetComponent<Image>();
        var c = img.color;
        c.a -= healAlphaDelta;
        c.a = Mathf.Clamp(c.a, 0, 1);
        img.color = c;
    }

}