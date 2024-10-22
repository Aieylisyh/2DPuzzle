using com;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BalanceSceneSystem : MonoBehaviour
{
    public static BalanceSceneSystem instance;
    [SerializeField] RectTransform balanceHorizontal;
    [SerializeField] RectTransform balanceRightPoint;
    [SerializeField] float weightAngleRatio;
    [SerializeField] PanelCanvasGroupSwitcher _pcgs;
    float _leftWeight;
    float _rightWeight;

    [SerializeField] OpponentItemData[] oids;
    [SerializeField] int myItemsCount;
    int _totalOnBalanceCount;
    [SerializeField] GameObject[] myWords;

    [System.Serializable]
    public class OpponentItemData
    {
        public OpponentItem oi;
        public float delay;

    }

    private void Awake()
    {
        instance = this;
    }

    public void Reinit()
    {
        _pcgs.Show(true, true);
        _rightWeight = 0;
        _leftWeight = 0;
        _totalOnBalanceCount = 0;

        balanceHorizontal.localEulerAngles = Vector3.zero;
        SceneTextSystem.instance.SetText(1, false);

        StopAllCoroutines();
        foreach (var oid in oids)
        {
            StartCoroutine(LondonLifeGameSystem.instance.DelayAction(oid.delay, () =>
            {
                oid.oi.Init();
            }));
        }
        foreach (var w in myWords)
        {
            w.SetActive(false);
        }
        StartCoroutine(ShowMyWordsOneByOne());
    }

    public void OnReceiveItem(float weight, bool rightOrLeft)
    {
        if (rightOrLeft)
        {
            _rightWeight += weight;
        }
        else
        {
            _leftWeight += weight;
        }

        _totalOnBalanceCount++;
        SyncBalance();
        CheckEnd();
    }

    void SyncBalance()
    {
        var w = weightAngleRatio * (_rightWeight - _leftWeight);
        balanceHorizontal.DOKill();
        balanceHorizontal.DORotate(new Vector3(0, 0, w), 1).SetEase(Ease.OutElastic);
    }

    void CheckEnd()
    {
        var total = oids.Length + myItemsCount;
        if (_totalOnBalanceCount >= total)
        {
            StartCoroutine(EndBalanceScene());
        }
    }

    IEnumerator EndBalanceScene()
    {
        //Debug.Log("EndBalanceScene");
        yield return new WaitForSeconds(2.5f);
        _pcgs.Show(false, false);
        yield return new WaitForSeconds(1.5f);
        OpenDoorSceneSystem.instance.Reinit();
    }

    IEnumerator ShowMyWordsOneByOne()
    {
        //Debug.Log("ShowMyWordsOneByOne");
        yield return new WaitForSeconds(2);

        foreach (var w in myWords)
        {
            yield return new WaitForSeconds(1.5f);
            w.SetActive(true);
            var imgs = w.GetComponentsInChildren<Image>();
            foreach (var img in imgs)
            {
                var c = img.color;
                img.color = new Color(1, 1, 1, 0);
                img.DOColor(c, 2);
            }
        }
    }
}