using com;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogWithGirlSystem : MonoBehaviour
{
    public static DialogWithGirlSystem instance;

    [SerializeField] PanelCanvasGroupSwitcher _pcgs;

    [SerializeField] Image b1;
    [SerializeField] Image b2;
    [SerializeField] Image b3;
    [SerializeField] Image g1;
    [SerializeField] Image g2;
    [SerializeField] Image g3;

    private void Awake()
    {
        instance = this;
    }

    public void Reinit()
    {
        _pcgs.Show(true, true);

        HideInstant(b1);
        HideInstant(b2);
        HideInstant(b3);
        HideInstant(g1);
        HideInstant(g2);
        HideInstant(g3);
        Show(b1);
        SceneTextSystem.instance.SetText(5, false);
    }

    public void HideInstant(Image img)
    {
        img.DOKill();
        img.raycastTarget = false;
        img.color = new Color(1, 1, 1, 0);
    }

    public void Hide(Image img)
    {
        img.DOKill();
        img.raycastTarget = false;
        img.DOColor(new Color(1, 1, 1, 0), 0.6f);
    }

    public void Show(Image img)
    {
        img.DOKill();
        img.raycastTarget = true;
        SoundSystem.instance.Play("bubble");
        img.DOColor(new Color(1, 1, 1, 1), 1.2f).SetDelay(1);
    }

    IEnumerator EndScene()
    {
        yield return new WaitForSeconds(2);
        _pcgs.Show(false, false);
        自己做饭System.instance.Reinit();
    }

    public void End()
    {
        StartCoroutine(EndScene());
    }
}