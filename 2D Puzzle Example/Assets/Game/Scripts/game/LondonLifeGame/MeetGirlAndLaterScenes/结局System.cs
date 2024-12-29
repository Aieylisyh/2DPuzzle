using com;
using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class 结局System : MonoBehaviour
{
    public static 结局System instance;
    public CameraFilterPack_Drawing_Manga_Flash Manga_Flash;
    [SerializeField] PanelCanvasGroupSwitcher _pcgs;
    public CanvasGroup endingCg;

    private void Awake()
    {
        instance = this;

    }

    public void Reinit()
    {
        _pcgs.Show(true, true);
        // image1.DOFade(1, 1);
        //   clockCg.DOFade(1, 1).SetDelay(0.8f);
        SceneTextSystem.instance.SetText(10, false);

        StartCoroutine(EndSceneCo());
    }

    IEnumerator EndSceneCo()
    {
        yield return new WaitForSeconds(1);
        Manga_Flash.enabled = true;
        endingCg.gameObject.SetActive(true);
        endingCg.DOFade(1, 3);
        yield return new WaitForSeconds(1);
        //_pcgs.Show(false, false);
        SetText(descEnd, false);

    }

    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] float interval = 0.05f;
    [Multiline]
    public string descEnd;

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