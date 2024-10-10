using com;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InRoomClockScene : MonoBehaviour
{
    public static InRoomClockScene instance;
    public Image image1;
    public Image image2;
    public Image image3;

    public CanvasGroup clockCg;

    [SerializeField] PanelCanvasGroupSwitcher _pcgs;

    private void Awake()
    {
        instance = this;
        image1.color = new Color(1, 1, 1, 0);
        image2.color = new Color(1, 1, 1, 0);
        image3.color = new Color(1, 1, 1, 0);

        clockCg.alpha = 0;
    }

    public void Reinit()
    {
        _pcgs.Show(true, true);
        image1.DOFade(1, 1);
        clockCg.DOFade(1, 1).SetDelay(0.8f);
    }

    IEnumerator EndClockScene()
    {
        Debug.Log("EndClockScene");
        yield return new WaitForSeconds(2);
        _pcgs.Show(false, false);

    }

    public void ClockEnd()
    {
        StartCoroutine(EndClockScene());
    }
}