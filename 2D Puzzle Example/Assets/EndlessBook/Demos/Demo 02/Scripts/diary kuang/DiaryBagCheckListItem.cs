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
        isChecked = true;
        checkmark.SetActive(true);
        checkmark.transform.DOKill();
        if (withAnime)
        {
            checkmark.transform.DOPunchScale(Vector3.one * 0.35f, 0.8f, 5, 0.7f);
        }
    }

    public void UnCheck()
    {
        isChecked = false;
        checkmark.SetActive(false);
    }
}