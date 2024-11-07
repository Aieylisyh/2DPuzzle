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

    [SerializeField] GameObject cuttingKnife;
    [SerializeField] GameObject mixingLadle;
    [SerializeField] GameObject pouringKettle;

    bool _isSelectedKnife;
    bool _isSelectedLadle;

    void ShowCutAnimation(Image toHide, Image imageToShow)
    {
        Knife.enabled = false;
        toHide.DOFade(0, 1).SetDelay(2).OnComplete(
            () =>
            {
                Knife.enabled = true;
            });

    }
    public void OnClick_Carrot()
    {
        if (_isSelectedKnife)
        {
            ShowCutAnimation(Carrot, Carrot_sliced);

        }
    }

    public void OnClick_Potato()
    {

    }

    public void OnClick_Chicken()
    {

    }

    public void OnClick_Carrot_sliced()
    {

    }

    public void OnClick_Potato_sliced()
    {

    }

    public void OnClick_Chicken_sliced()
    {

    }

    public void OnClick_Kettle()
    {

    }

    public void OnClick_Ladle()
    {

    }

    public void OnClick_Knife()
    {

    }

    public void OnClick_EmptyPot()
    {

    }

    public void OnClick_CurryPot()
    {

    }
}