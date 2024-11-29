using System.Collections;
using UnityEngine;

public class TinyStageOfMimicPanels : MonoBehaviour
{
    public Transform mimicPanelObjectParent;
    public MimicPanelObject[] mimicPanelObjects { get; private set; }
    public float stageDuration;
    Coroutine crtStagePlayCoroutine;
    public bool playing { get; private set; }
    public void Init()
    {
        if (mimicPanelObjectParent == null)
            mimicPanelObjectParent = this.transform;

        playing = false;
        mimicPanelObjects = mimicPanelObjectParent.GetComponentsInChildren<MimicPanelObject>();
        foreach (var mpo in mimicPanelObjects)
            mpo.ResetAnim();
    }

    public void StartPlay()
    {
        if (playing)
            return;

        Debug.Log("StartPlay");
        playing = true;
        foreach (var mpo in mimicPanelObjects)
        {
            mpo.ResetAnim();
            mpo.StartAnim();
        }

        if (crtStagePlayCoroutine != null) StopCoroutine(crtStagePlayCoroutine);
        crtStagePlayCoroutine = StartCoroutine(StagePlayCoroutine());
    }

    public void ResetPlay()
    {
        Debug.Log("ResetPlay");
        if (crtStagePlayCoroutine != null) StopCoroutine(crtStagePlayCoroutine);
        playing = false;
        foreach (var mpo in mimicPanelObjects)
        {
            mpo.StartAnim();
        }
    }

    IEnumerator StagePlayCoroutine()
    {
        yield return new WaitForSeconds(stageDuration);
        playing = false;
    }
}