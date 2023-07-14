using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using com;

public class Floor1System : MonoBehaviour
{
    public static Floor1System instance;

    public GameObject woodenFace1;
    public GameObject woodenFace2;

    public int correctIndex1;
    public int correctIndex2;
    public int correctIndex3;
    public int correctIndex4;

    public ClickChangeImageQueue icon1;
    public ClickChangeImageQueue icon2;
    public ClickChangeImageQueue icon3;
    public ClickChangeImageQueue icon4;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartPlay();
    }

    void StartPlay()
    {

    }

    public void CheckIcons()
    {
        if (icon1.crtIndex == correctIndex1
            && icon2.crtIndex == correctIndex2
              && icon3.crtIndex == correctIndex3
                && icon4.crtIndex == correctIndex4
            )
        {
            SoundSystem.instance.Play("change");

            woodenFace2.SetActive(true);
            woodenFace1.SetActive(false);

            icon1.gameObject.SetActive(false);
            icon2.gameObject.SetActive(false);
            icon3.gameObject.SetActive(false);
            icon4.gameObject.SetActive(false);
        }
    }

    public void OnClickKeyHole()
    {
        Debug.Log("OnClickKeyHole");

        var itemData = InventorySystem.instance.GetCurrentItemData();
        if (itemData != null && itemData.id == "key")
        {
            SoundSystem.instance.Play("change");
            InventorySystem.instance.RemoveItem(new ItemData(1, "key"));
            StartCoroutine(EndGameSeq());
        }
    }

    public Transform doorLayer1_r;
    public Transform doorLayer2_r;
    public Transform doorLayer3_r;
    public Transform doorLayer1_l;
    public Transform doorLayer2_l;
    public Transform doorLayer3_l;
    public float doorMoveEndX_r;
    public float doorMoveEndX_l;

    public Transform bikeEndPos;
    public GameObject bike;

    IEnumerator EndGameSeq()
    {
        woodenFace2.transform.DOShakePosition(4, 5);
        yield return new WaitForSeconds(1);

        bike.transform.DOMoveX(bikeEndPos.position.x, 7);
        bike.transform.DOShakeRotation(7, 1, 18, 70, false);
    }
}