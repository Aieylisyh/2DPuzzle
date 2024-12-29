using System;
using System.Collections;
using UnityEngine;

public class LondonLifeGameSystem : MonoBehaviour
{
    public static LondonLifeGameSystem instance;
    public enum Stage
    {
        None,
        GoToPlane,
        Balance,
        OpenDoor,
        Clock,
        MeetGirl,
        DialogWithGirl,
        自己做饭,
        女邻居点赞_一起做饭,
        越来越多人加入,
        结局,
    }

    public Stage startStage;
    Stage _crtStage;
    public GameObject frame;
    public GameObject Text;
    public GameObject Title;

    public CanvasGroup[] allScenes;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //SetStage(startStage);
        warp2.enabled = true;
    }

    void SetStage(Stage s)
    {

        _crtStage = s;
        switch (_crtStage)
        {
            case Stage.None:
                break;
            case Stage.GoToPlane:
                PlaneSceneGameSystem.instance.Reinit();
                break;
            case Stage.Balance:
                BalanceSceneSystem.instance.Reinit();
                break;
            case Stage.OpenDoor:
                OpenDoorSceneSystem.instance.Reinit();
                break;
            case Stage.Clock:
                InRoomClockScene.instance.Reinit();
                break;
            case Stage.MeetGirl:
                MeetGirlSystem.instance.Reinit();
                break;
            case Stage.DialogWithGirl:
                DialogWithGirlSystem.instance.Reinit();
                break;
            case Stage.自己做饭:
                自己做饭System.instance.Reinit();
                break;
            case Stage.女邻居点赞_一起做饭:
                女邻居点赞System.instance.Reinit();
                break;
            case Stage.越来越多人加入:
                越来越多人加入System.instance.Reinit();
                break;
            case Stage.结局:
                结局System.instance.Reinit();
                break;

        }
    }

    public IEnumerator DelayAction(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    public CameraFilterPack_Vision_Warp2 warp2;

    public void SetToFirstStage()
    {
        //   SetStage(Stage.GoToPlane);
        SetStage(startStage);
        frame.SetActive(true);
        Text.SetActive(true);
        Title.SetActive(false);
        warp2.enabled = false;
    }
}