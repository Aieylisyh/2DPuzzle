using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com;

public class PlaneSceneGameSystem : MonoBehaviour
{
    public static PlaneSceneGameSystem instance;

    [SerializeField] PanelCanvasGroupSwitcher _pcgs;

    GoToPlaneBehaviour _goToPlane;

    public DreamBubble.气泡参数[] 气泡们;
    List<DreamBubble.气泡参数> _待生成的气泡们 = new List<DreamBubble.气泡参数>();
    List<DreamBubble> _bubbles = new List<DreamBubble>();

    public float bubbleInterval;
    public DreamBubble bubblePrefab;
    public Transform bubbleParent;

    Coroutine _生成气泡循环Co;

    private void Awake()
    {
        instance = this;
        _goToPlane = GetComponent<GoToPlaneBehaviour>();

        foreach (var p in 气泡们)
            _待生成的气泡们.Add(p);
    }

    private void Start()
    {

    }

    public void Reinit()
    {
        _pcgs.Show(true, true);
        _goToPlane.ShowStartAnimation();
    }

    public void StartGenerateBubbles()
    {
        _生成气泡循环Co = StartCoroutine(生成气泡循环());
    }

    public void AddTo待生成的气泡们(DreamBubble.气泡参数 p)
    {
        _待生成的气泡们.Add(p);
    }

    IEnumerator 生成气泡循环()
    {
        while (true)
        {
            yield return new WaitForSeconds(bubbleInterval);
            if (_待生成的气泡们.Count > 0)
            {
                CreateBubble(_待生成的气泡们[0]);
                _待生成的气泡们.RemoveAt(0);
            }
        }
    }

    void CreateBubble(DreamBubble.气泡参数 p)
    {
        var b = Instantiate(bubblePrefab, bubbleParent);
        b.Init(p);
    }

    public void OnClickBubble()
    {
        _goToPlane.OnClickBubble();
    }

    public void OnGoToPlaneEnd()
    {
        if (_生成气泡循环Co != null)
            StopCoroutine(_生成气泡循环Co);

        ClearBubbles();
        StartCoroutine(ShowNextPanel());
    }

    void ClearBubbles()
    {
        foreach (var b in _bubbles)
        {
            Destroy(b.gameObject);
        }

        _bubbles = new List<DreamBubble>();
    }

    IEnumerator ShowNextPanel()
    {
        yield return new WaitForSeconds(1);
        _pcgs.Show(false, false);
    }
}