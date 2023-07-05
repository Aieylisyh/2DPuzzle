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
    public GameObject exitBtn;

    private Coroutine _redLightCoroutine;
    public void ResetBtns()
    {
        OnButtonClicked(0);
        exitBtn.SetActive(false);
        if (_redLightCoroutine != null)
            StopCoroutine(_redLightCoroutine);
    }

    public void OnButtonClicked(int btnFloor)
    {
        Debug.Log("按下了button floor " + btnFloor);
        exitBtn.SetActive(false);
        //遍历
        foreach (LiftControlPanelButton btn in buttons)
        {
            if (btnFloor == btn.floor)
            {
                btn.btnLight.SetActive(true);
                btn.btnHalo.SetActive(true);

                redLightOn.SetActive(true);
                _redLightCoroutine = StartCoroutine(TurnOffRedLight());
            }
            else
            {
                btn.btnLight.SetActive(false);
                btn.btnHalo.SetActive(false);
            }
        }

        LiftSystem.instance.OnLiftDestinationSet(btnFloor);
        exitBtn.SetActive(true);
    }

    public void OnClickExit()
    {
        exitBtn.SetActive(false);
        gameObject.SetActive(false);
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
        // exitBtn.SetActive(true);
        // exitBtn.SetActive(false);
        gameObject.SetActive(false);
    }
}
