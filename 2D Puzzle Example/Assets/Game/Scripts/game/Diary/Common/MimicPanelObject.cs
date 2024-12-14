using DG.Tweening;
using System.Collections;
using UnityEngine;


[System.Serializable]
public class MimicPanelObject : MonoBehaviour
{
    public float delay;
    public float duration;
    public bool useCrtLocalPos;
    public Vector3 startPos;//local
    public Vector3 startEular;//local
    public Vector3 endPos;//local
    public Vector3 endEular;//local
    public DG.Tweening.Ease ease;
    public bool ignoreReset;
    private void Awake()
    {
        var mr = GetComponent<MeshRenderer>();
        if (mr != null) mr.enabled = false;

        if (useCrtLocalPos)
        {
            startPos = transform.localPosition;
            endPos = transform.localPosition;
        }
    }

    public void ResetAnim()
    {
        Debug.Log(gameObject.name);
        if (ignoreReset)
            return;
        transform.DOKill();
        transform.localPosition = startPos;
        transform.localEulerAngles = startEular;
    }

    public void StartAnim()
    {
        // transform.DOKill();
        transform.DOLocalMove(endPos, duration).SetEase(ease).SetDelay(delay);
        transform.DOLocalRotate(endEular, duration).SetEase(ease).SetDelay(delay);
    }
}