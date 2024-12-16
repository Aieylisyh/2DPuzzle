using com;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DiaryBagCheckListItem : MonoBehaviour
{
    public bool isChecked;
    public GameObject checkmark;

    private void Awake()
    {
        UnCheck();
    }
    public void Check(bool withAnime)
    {
        if (isChecked)
            return;

        SoundSystem.instance.Play(new string[] { "s1", "s2" });
        isChecked = true;
        checkmark.SetActive(true);
        checkmark.transform.DOKill();
        //Debug.Log("Check");
        if (withAnime)
        {
            checkmark.transform.DOPunchScale(Vector3.one * 0.35f, 0.8f, 5, 0.7f);
        }

        PageView_Kuang_LuggageChecklist_1.instance.CheckLock();
    }

    public void UnCheck()
    {
        isChecked = false;
        checkmark.SetActive(false);
    }
}