using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class RLSystem : MonoBehaviour
{
    [SerializeField] Image nextBtn_RoofScene;
    // Start is called before the first frame update
    void OnAdmissionLetterSceneEnd()
    {
        StartCoroutine(DelayAction(1.5f, ShowNextBtn_RoofScene));
    }

    void ShowNextBtn_RoofScene()
    {
        nextBtn_RoofScene.gameObject.SetActive(true);
        var c = nextBtn_RoofScene.color;
        c.a = 0;
        nextBtn_RoofScene.color = c;
        c.a = 1;
        nextBtn_RoofScene.DOColor(c, 1f);
    }

    public void OnClickBtn_ShowRoofScene()
    {
        ToggleOff_AdmissionLetterScene();
    }

    void ToggleOff_RoofScene()
    {

    }
}
