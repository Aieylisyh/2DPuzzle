using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class RLSystem : MonoBehaviour
{

    [SerializeField] Image[] checkmark;

    void InitCheckListScene()
    {
        foreach (var c in checkmark)
        {
            c.gameObject.SetActive(false);
        }
    }

    public void OnClickChecklistItem(int i)
    {
        if (i == 0 && sceneSwitcher.crtId == SceneId.Checklist && !checkmark[0].gameObject.activeSelf)
        {
            ShowCheckMark(checkmark[0]);

            StartCoroutine(DelayAction(3, () =>
            {
                ToggleContinueButton(true);
            }));
        }
        else if (i == 1 && sceneSwitcher.crtId == SceneId.Classroom1 && !checkmark[1].gameObject.activeSelf)
        {
            ShowCheckMark(checkmark[1]);

            StartCoroutine(DelayAction(3, () =>
            {
                OnTodoList2Checked();
            }));
        }
    }

    void ShowCheckMark(Image c)
    {
        c.color = new Color(1, 1, 1, 0);
        c.transform.localScale = Vector3.one * 2.5f;
        c.gameObject.SetActive(true);
        c.DOColor(Color.white, 1.8f);
        c.transform.DOScale(1, 1.8f);
    }

    public void OnClickExitChecklist()
    {
        //black screen walk sound
        //black screen walk sound
        //show map to choose go where
    }


}
