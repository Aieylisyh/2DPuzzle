using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;


public class SceneTextSystem : MonoBehaviour
{
    public static SceneTextSystem instance;
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] float interval = 0.05f;

    [Multiline]
    public string[] descs;

    private void Awake()
    {
        instance = this;
    }

    public void SetText(int i, bool instant = true)
    {
        SetText(descs[i], instant);
    }

    public void SetText(string s, bool instant = true)
    {
        StopAllCoroutines();
        if (instant)
        {
            txt.text = s;
            txt.maxVisibleCharacters = s.Length;
        }
        else
        {
            txt.text = s;
            txt.maxVisibleCharacters = 0;
            StartCoroutine(TweenTxtMaxVisibleCharacters(s.Length));
        }
    }
    IEnumerator TweenTxtMaxVisibleCharacters(int max)
    {
        int i = 0;
        while (i < max)
        {
            yield return new WaitForSeconds(interval);
            i++;
            txt.maxVisibleCharacters = i;
        }
    }
}