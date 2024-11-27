using com;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class 越来越多人加入System : MonoBehaviour
{
    public static 越来越多人加入System instance;

    [SerializeField] PanelCanvasGroupSwitcher _pcgs;

    private int _ganbeiIndex;

    private void Awake()
    {
        instance = this;

    }

    public void Reinit()
    {
        _pcgs.Show(true, true);
        // image1.DOFade(1, 1);
        //   clockCg.DOFade(1, 1).SetDelay(0.8f);
        SceneTextSystem.instance.SetText(9, false);
        _ganBeiDone = true;
        _ganbeiIndex = 0;

        foreach (var p in peoples)
        {
            p.endPos = p.view.transform.position;
        }
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

    public CanvasGroup[] dishes;

    [System.Serializable]
    public class PeopleHand
    {
        public GameObject view;
        public Transform from;
        [HideInInspector]
        public Vector3 endPos;
    }

    public PeopleHand[] peoples;

    bool _ganBeiDone;

    public void Click()
    {
        if (!_ganBeiDone)
            return;

        _ganBeiDone = false;
        if (_ganbeiIndex == 0)
        {
            ToggleDishes(_ganbeiIndex);
            Toggle干杯(0, true, 2f);
            Toggle干杯(1, true, 2.5f, true, true);
            Toggle干杯(2, false);
            Toggle干杯(3, false);
            Toggle干杯(4, false);
            Toggle干杯(5, false);
            _ganbeiIndex++;
        }
        else if (_ganbeiIndex == 1)
        {
            ToggleDishes(_ganbeiIndex);
            Toggle干杯(0, true, 2f);
            Toggle干杯(1, true, 2.3f, true);
            Toggle干杯(2, true, 2.6f, true, true);
            Toggle干杯(3, false);
            Toggle干杯(4, false);
            Toggle干杯(5, false);
            _ganbeiIndex++;
        }
        else if (_ganbeiIndex == 2)
        {
            ToggleDishes(_ganbeiIndex);
            Toggle干杯(0, true, 2f);
            Toggle干杯(1, true, 2.3f, true);
            Toggle干杯(2, true, 2.5f, true);
            Toggle干杯(3, true, 2.7f, true);
            Toggle干杯(4, true, 2.8f, true, true);
            Toggle干杯(5, false);
            _ganbeiIndex++;
        }
        else if (_ganbeiIndex == 3)
        {
            ToggleDishes(_ganbeiIndex);
            Toggle干杯(0, true, 2f);
            Toggle干杯(1, true, 2.3f, true);
            Toggle干杯(2, true, 2.5f, true);
            Toggle干杯(3, true, 2.7f, true);
            Toggle干杯(4, true, 2.8f, true);
            Toggle干杯(5, true, 3.0f, true, true);
            _ganbeiIndex++;
        }
        else
        {
            End();
        }
    }
    void Toggle干杯(int peopleIndex, bool toggle, float duration = 2f, bool sound = false, bool last = false)
    {
        var p = peoples[peopleIndex];
        if (!toggle)
        {
            p.view.SetActive(false);
            return;
        }

        p.view.SetActive(true);
        p.view.transform.position = p.from.position;
        p.view.transform.DOKill();
        p.view.transform.DOMove(p.endPos, duration).SetEase(Ease.InQuad).SetDelay(0.5f).OnComplete(
            () =>
            {
                if (sound)
                {
                    if (Random.value < 0.5f)
                        SoundSystem.instance.Play("ding");
                    else
                        SoundSystem.instance.Play("item");
                }

                if (last)
                    _ganBeiDone = true;
            }
            );
    }

    void ToggleDishes(int dishIndex)
    {
        for (int i = 0; i < dishes.Length; i++)
        {
            if (i == dishIndex)
            {
                dishes[i].DOKill();
                dishes[i].DOFade(1, 1);
            }
            else
            {
                dishes[i].DOKill();
                dishes[i].alpha = 0;
            }
        }
    }
}