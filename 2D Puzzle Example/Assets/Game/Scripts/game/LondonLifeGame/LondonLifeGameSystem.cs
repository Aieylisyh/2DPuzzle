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
        女邻居点赞,
        一起做饭,
        越来越多人加入,
        结局,
    }
    public Stage startStage;
    Stage _crtStage;

    public CanvasGroup[] allScenes;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        SetStage(startStage);
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
        }
    }

    public IEnumerator DelayAction(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}