using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftControlPanel : MonoBehaviour
{
    //点击按钮，按钮亮
    //点击按钮，其它按钮暗
    //点击按钮，红灯亮
    //红灯亮，过一会暗

    public List<LiftControlPanelButton> buttons = new List<LiftControlPanelButton>();//列表

    public GameObject redLightOn;
    public void OnButtonClicked(int btnFloor)
    {
        Debug.Log("按下了button floor " + btnFloor);
        //遍历
        foreach (LiftControlPanelButton btn in buttons)
        {
            if (btnFloor == btn.floor)
            {
                btn.btnLight.SetActive(true);
                btn.btnHalo.SetActive(true);

                redLightOn.SetActive(true);
                StartCoroutine(TurnOffRedLight());
            }
            else
            {
                btn.btnLight.SetActive(false);
                btn.btnHalo.SetActive(false);
            }
        }
    }

    IEnumerator TurnOffRedLight()
    {
        yield return new WaitForSeconds(1.0f);
        redLightOn.SetActive(false);
        yield return new WaitForSeconds(0.15f);
        redLightOn.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        redLightOn.SetActive(false);
        yield return new WaitForSeconds(0.15f);
        redLightOn.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        redLightOn.SetActive(false);
    }
}
