using System.Collections;
using UnityEngine;

public class OpponentItem : MonoBehaviour
{
    public float weight;
    [SerializeField] float g;
    [SerializeField] RectTransform _dropTarget;
    [SerializeField] float _heightToDrop;
    [SerializeField] RectTransform _rect;
    float _v;
    bool _stopped;

    public void Init()
    {
        _rect = GetComponent<RectTransform>();
        var p = _dropTarget.position;
        _rect.position = p;
        _rect.anchoredPosition += new Vector2(Random.Range(-50, 50), _heightToDrop);
        gameObject.SetActive(true);
        _stopped = false;
        _v = 0;
    }

    private void Update()
    {
        if (_stopped)
            return;

        _v += Time.deltaTime * g;
        var p = _rect.anchoredPosition;
        var d = Time.deltaTime * _v;
        p.y -= d;

        _rect.anchoredPosition = p;
        if (_rect.transform.position.y < _dropTarget.transform.position.y)
        {
            _stopped = true;
            transform.SetParent(_dropTarget.transform);
            BalanceSceneSystem.instance.OnReceiveItem(weight, false);
        }
    }
}