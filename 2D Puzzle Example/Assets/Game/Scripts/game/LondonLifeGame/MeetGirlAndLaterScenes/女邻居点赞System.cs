using com;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class 女邻居点赞System : MonoBehaviour
{
    public static 女邻居点赞System instance;

    [SerializeField] PanelCanvasGroupSwitcher _pcgs;

    private void Awake()
    {
        instance = this;

    }

    public void Reinit()
    {
        _pcgs.Show(true, true);
        // image1.DOFade(1, 1);
        //   clockCg.DOFade(1, 1).SetDelay(0.8f);
        SceneTextSystem.instance.SetText(7, false);
        beggerScene.SetActive(true);
        chaseScene.SetActive(false);

        spoon.gameObject.SetActive(false);
        food1.gameObject.SetActive(false);
        food2.gameObject.SetActive(false);
        food3.gameObject.SetActive(false);

        _food1Done = false;
        _food2Done = false;
        _food3Done = false;
        _canClickOnBowl = true;
        spoon.gameObject.SetActive(false);
    }

    IEnumerator EndScene()
    {
        yield return new WaitForSeconds(2);
        _pcgs.Show(false, false);

    }

    public void End()
    {
        StartCoroutine(EndScene());
    }

    public GameObject beggerScene;
    public GameObject chaseScene;
    public Transform spoon;
    public Transform food1;
    public Transform food2;
    public Transform food3;
    public Transform spoonStart;
    public Transform spoonEnd;
    public Transform foodEnd;

    bool _food1Done;
    bool _food2Done;
    bool _food3Done;
    bool _canClickOnBowl;

    void ShowSpoon()
    {
        var t = 1.4f;

        spoon.gameObject.SetActive(true);
        spoon.DOKill();
        spoon.position = spoonStart.position;
        spoon.rotation = spoonStart.rotation;
        spoon.DOMove(spoonEnd.position, t).SetEase(Ease.OutBack);
        spoon.DORotate(spoonEnd.eulerAngles, t).OnComplete(() =>
        {
            spoon.gameObject.SetActive(false);
        });
    }

    public void OnClickOnBowl()
    {
        SoundSystem.instance.Play("bowl");
        if (!_canClickOnBowl)
            return;

        var t = 1.2f;
        var t_color = 0.7f;
        if (!_food1Done)
        {
            _food1Done = true;
            _canClickOnBowl = false;
            food1.gameObject.SetActive(true);
            food1.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            food1.GetComponent<Image>().DOFade(1, t_color);
            food1.DOMove(foodEnd.position, t).SetEase(Ease.InBack);
            food1.DOScale(foodEnd.localScale.x, t).SetEase(Ease.InCubic).OnComplete(() =>
            {
                _canClickOnBowl = true;
            });
            ShowSpoon();
            return;
        }

        if (!_food2Done)
        {
            _food2Done = true;
            _canClickOnBowl = false;
            food2.gameObject.SetActive(true);
            food2.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            food2.GetComponent<Image>().DOFade(1, t_color);
            food2.DOMove(foodEnd.position, t).SetEase(Ease.InBack);
            food2.DOScale(foodEnd.localScale.x, t).SetEase(Ease.InCubic).OnComplete(() =>
            {
                _canClickOnBowl = true;
            });
            ShowSpoon();
            return;
        }

        if (!_food3Done)
        {
            _food3Done = true;
            _canClickOnBowl = false;
            food3.gameObject.SetActive(true);
            food3.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            food3.GetComponent<Image>().DOFade(1, t_color);
            food3.DOMove(foodEnd.position, t).SetEase(Ease.InBack);
            food3.DOScale(foodEnd.localScale.x, t).SetEase(Ease.InCubic).OnComplete(() =>
            {
                _canClickOnBowl = true;
            });
            ShowSpoon();
            return;
        }

        beggerScene.SetActive(false);
        chaseScene.SetActive(true);
    }
}