using com;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class 自己做饭System : MonoBehaviour
{
    public static 自己做饭System instance;

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
        SceneTextSystem.instance.SetText(6, false);

        Carrot.enabled = true;
        Potato.enabled = true;
        Chicken.enabled = true;
        Carrot_sliced.enabled = false;
        Potato_sliced.enabled = false;
        Chicken_sliced.enabled = false;

        Knife.enabled = true;
        Ladle.enabled = true;
        Kettle.enabled = true;
        EmptyPot.enabled = true;
        CurryPot.enabled = false;
        cuttingKnife.gameObject.SetActive(false);
        mixingLadle.gameObject.SetActive(false);
        pouringKettle.gameObject.SetActive(false);
    }

    IEnumerator EndScene()
    {
        yield return new WaitForSeconds(0.2f);
        _pcgs.Show(false, false);
        女邻居点赞System.instance.Reinit();
    }

    public void End()
    {
        Debug.Log("end");
        StartCoroutine(EndScene());
    }

    [SerializeField] Image Carrot;
    [SerializeField] Image Potato;
    [SerializeField] Image Chicken;

    [SerializeField] Image Carrot_sliced;
    [SerializeField] Image Potato_sliced;
    [SerializeField] Image Chicken_sliced;

    [SerializeField] Image Knife;
    [SerializeField] Image Ladle;
    [SerializeField] Image Kettle;
    [SerializeField] Image EmptyPot;
    [SerializeField] Image CurryPot;

    [SerializeField] RectTransform cuttingKnife;
    [SerializeField] RectTransform mixingLadle;
    [SerializeField] RectTransform pouringKettle;

    bool _isSelectedKnife;
    bool _isSelectedLadle;
    bool _isSelectedCarrotSliced;
    bool _isSelectedChickenSliced;
    bool _isSelectedPotatoSliced;

    void ShowCutAnimation(Image toHide, Image toShow)
    {
        toHide.raycastTarget = false;
        ToggleSelectKnife(false);
        Knife.enabled = false;

        cuttingKnife.anchoredPosition = toHide.rectTransform.anchoredPosition + new Vector2(50, 10);
        cuttingKnife.gameObject.SetActive(true);

        var seq = DOTween.Sequence();
        seq.AppendInterval(1.2f);
        seq.AppendCallback(() => { toHide.DOFade(0, 1); });
        seq.AppendInterval(1);
        seq.AppendCallback(() =>
        {
            toShow.color = new Color(1, 1, 1, 0);
            toShow.enabled = true;
            toShow.DOFade(1, 1);
        });
        seq.AppendInterval(1);
        seq.AppendCallback(() =>
        {
            Knife.enabled = true;
            cuttingKnife.gameObject.SetActive(false);
        });

        seq.Play();
    }

    void ToggleSelectKnife(bool b)
    {
        _isSelectedKnife = b;
        Knife.color = b ? new Color(1, 0.5f, 0.5f, 1) : Color.white;
    }

    void ToggleSelectLadle(bool b)
    {
        _isSelectedLadle = b;
        Ladle.color = b ? new Color(1, 0.5f, 0.5f, 1) : Color.white;
    }

    public void OnClick_Carrot()
    {
        if (_isSelectedKnife)
        {
            ShowCutAnimation(Carrot, Carrot_sliced);
        }
        ToggleSelectKnife(false);
        ToggleSelectLadle(false);
    }

    public void OnClick_Potato()
    {
        if (_isSelectedKnife)
        {
            ShowCutAnimation(Potato, Potato_sliced);
        }
        ToggleSelectKnife(false);
        ToggleSelectLadle(false);
    }

    public void OnClick_Chicken()
    {
        if (_isSelectedKnife)
        {
            ShowCutAnimation(Chicken, Chicken_sliced);
        }
        ToggleSelectKnife(false);
        ToggleSelectLadle(false);
    }

    public void OnClick_Carrot_sliced()
    {
        ToggleSelectKnife(false);
        ToggleSelectLadle(false);
        _isSelectedCarrotSliced = true;
        Carrot_sliced.color = new Color(1, 0.5f, 0.5f, 1);
    }

    public void OnClick_Potato_sliced()
    {
        ToggleSelectKnife(false);
        ToggleSelectLadle(false);
        _isSelectedPotatoSliced = true;
        Potato_sliced.color = new Color(1, 0.5f, 0.5f, 1);
    }

    public void OnClick_Chicken_sliced()
    {
        ToggleSelectKnife(false);
        ToggleSelectLadle(false);
        _isSelectedChickenSliced = true;
        Chicken_sliced.color = new Color(1, 0.5f, 0.5f, 1);
    }

    public void OnClick_Kettle()
    {
        ToggleSelectKnife(false);
        ToggleSelectLadle(false);

        if (_potHasChicken && _potHasPotato && _potHasCarrot)
        {
            PourWater();
        }
    }

    public void OnClick_Ladle()
    {
        ToggleSelectLadle(true);
        ToggleSelectKnife(false);
    }

    public void OnClick_Knife()
    {
        ToggleSelectKnife(true);
        ToggleSelectLadle(false);
    }

    public void OnClick_EmptyPot()
    {
        if (_isSelectedLadle && _potReadyToMix)
        {
            _isSelectedLadle = false;
            StartMix();
            ToggleSelectKnife(false);
            ToggleSelectLadle(false);
            return;
        }

        ToggleSelectKnife(false);
        ToggleSelectLadle(false);
        if (_isSelectedChickenSliced)
        {
            _isSelectedChickenSliced = false;
            MoveSlicedFoodToPot(Chicken_sliced);
            _potHasChicken = true;
            return;
        }
        if (_isSelectedCarrotSliced)
        {
            _isSelectedCarrotSliced = false;
            MoveSlicedFoodToPot(Carrot_sliced);
            _potHasCarrot = true;
            return;
        }
        if (_isSelectedPotatoSliced)
        {
            _isSelectedPotatoSliced = false;
            MoveSlicedFoodToPot(Potato_sliced);
            _potHasPotato = true;
            return;
        }

    }

    void MoveSlicedFoodToPot(Image food)
    {
        food.raycastTarget = false;

        var startPos = food.rectTransform.anchoredPosition;
        var endPos = EmptyPot.rectTransform.anchoredPosition + new Vector2(0, 100);
        var highPos = (startPos + endPos) * 0.5f + new Vector2(0, 330);
        food.color = Color.white;
        food.rectTransform.DOAnchorPos(highPos, 0.7f).SetEase(Ease.InCubic).OnComplete(
            () =>
            {
                food.rectTransform.DOAnchorPos(endPos, 0.7f).SetEase(Ease.OutCubic);
            }
            );

    }

    bool _potHasChicken;
    bool _potHasPotato;
    bool _potHasCarrot;
    bool _potReadyToMix;

    void PourWater()
    {
        Kettle.enabled = false;
        pouringKettle.gameObject.SetActive(true);

        var seq = DOTween.Sequence();
        seq.AppendInterval(2f);
        seq.AppendCallback(() =>
        {
            Kettle.enabled = true;
            pouringKettle.gameObject.SetActive(false);
            _potReadyToMix = true;
        });
        seq.Play();
    }

    void StartMix()
    {
        EmptyPot.raycastTarget = false;
        ToggleSelectLadle(false);
        Ladle.enabled = false;
        Carrot_sliced.DOFade(0, 2);
        Chicken_sliced.DOFade(0, 2);
        Potato_sliced.DOFade(0, 2);

        mixingLadle.gameObject.SetActive(true);

        var seq = DOTween.Sequence();
        seq.AppendInterval(2f);

        seq.AppendCallback(() =>
        {
            mixingLadle.gameObject.SetActive(false);
        });
        seq.AppendInterval(0.5f);
        seq.AppendCallback(() =>
        {

            EmptyPot.enabled = false;
            CurryPot.enabled = true;
        });

        seq.Play();
    }

    bool ending;
    public void OnClick_CurryPot()
    {
        if (ending)
            return;

        End();
        ending = true;
    }
}