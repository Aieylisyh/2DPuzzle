using com;
using DG.Tweening;
using System.Collections;
using UnityEngine;

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

        StopAllCoroutines();
        foreach (var oid in oids)
        {
            StartCoroutine(LondonLifeGameSystem.instance.DelayAction(oid.delay, () =>
            {
                oid.oi.Init();
            }));
        }
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
        Debug.Log("EndBalanceScene");
        yield return new WaitForSeconds(2);
        _pcgs.Show(false, false);
        yield return new WaitForSeconds(1.5f);
        OpenDoorSceneSystem.instance.Reinit();
    }
}