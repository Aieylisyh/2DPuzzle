using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayAnime : MonoBehaviour
{
    public Sprite[] sps;

    public float duration;
    public float fadeTime;

    public float delay;

    private int _index;
    private int _indexBack;
    public Image img;
    public Image imgBack;

    public float a;
    void Start()
    {
        _index = 0;
        _indexBack = sps.Length - 1;
        StartCoroutine(PlayAnimationCoroutine());
    }

    IEnumerator PlayAnimationCoroutine()
    {
        SetImage();
        SetImageBack();
        yield return new WaitForSeconds(delay);

        while (true)
        {
            img.DOFade(0, fadeTime).SetEase(Ease.Linear);
            yield return new WaitForSeconds(fadeTime);

            SetImage();
            yield return new WaitForSeconds(duration);

            img.DOFade(1, fadeTime).SetEase(Ease.Linear);
            yield return new WaitForSeconds(fadeTime);

            SetImageBack();
            yield return new WaitForSeconds(duration);
        }
    }

    void SetImage()
    {
        img.sprite = sps[_index];

        _index++;
        if (_index >= sps.Length)
            _index = 0;
    }
    void SetImageBack()
    {
        imgBack.sprite = sps[_indexBack];

        _indexBack--;
        if (_indexBack < 0)
            _indexBack = sps.Length - 1;
    }
}
