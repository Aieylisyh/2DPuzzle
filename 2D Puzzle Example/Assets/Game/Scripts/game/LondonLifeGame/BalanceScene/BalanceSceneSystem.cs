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

    [SerializeField] OpponentItem[] ois;
    int _oisIndex;

    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start()
    {

    }
    public void Reinit()
    {
        _pcgs.Show(true, true);
        _rightWeight = 0;
        _leftWeight = 0;
        balanceHorizontal.localEulerAngles = Vector3.zero;
        StopAllCoroutines();
        StartCoroutine(LondonLifeGameSystem.instance.DelayAction(1, AddOpponentItem));
        StartCoroutine(LondonLifeGameSystem.instance.DelayAction(3, AddOpponentItem));
    }

    void AddOpponentItem()
    {
        if (_oisIndex >= ois.Length)
        {
            return;
        }

        var oi = ois[_oisIndex++];
        oi.Init();
    }

    // Update is called once per frame
    void Update()
    {

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
        SyncBalance();
    }

    void SyncBalance()
    {
        var w = weightAngleRatio * (_rightWeight - _leftWeight);
        balanceHorizontal.DOKill();
        balanceHorizontal.DORotate(new Vector3(0, 0, w), 1).SetEase(Ease.OutElastic);
    }
}