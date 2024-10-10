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
}