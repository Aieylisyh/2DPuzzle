using System.Collections;
using TMPro;
using UnityEngine;
using com;

public class LiftSystem : MonoBehaviour
{
    public static LiftSystem instance;

    public LiftControlPanel liftControlPanel;

    private float _crtFloor;//rounded up for the int floor number
    public int currentFloor { get { return Mathf.RoundToInt(_crtFloor); } }

    public TextMeshProUGUI liftNumLabel;

    public float liftSpeed;
    private float _timeArrive;
    private float _targetFloor;
    private bool _goDownDirection;

    public LiftDoors liftDoors;

    public GameObject floor1Dark;
    public GameObject floor2Dark;
    public GameObject floor3Dark;
    public GameObject floor4Dark;
    public GameObject floor5Dark;

    public GameObject scp_floor1Light;
    public GameObject scp_floor2Light;
    public GameObject scp_floor3Light;
    public GameObject scp_floor4Light;
    public GameObject scp_floor5Light;
    public GameObject scp_RedLight;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _crtFloor = 5;
        liftDoors.doorRight.Set(true, true);
        liftDoors.doorLeft.Set(true, true);
        SyncFloorLight();
    }

    private bool _lastIsLiftRunning = false;
    public bool IsLiftRunning
    {
        get { return GameTime.time <= _timeArrive; }
    }

    void SyncFloorLight()
    {
        floor1Dark.SetActive(currentFloor != 1);
        floor2Dark.SetActive(currentFloor != 2);
        floor3Dark.SetActive(currentFloor != 3);
        floor4Dark.SetActive(currentFloor != 4);
        floor5Dark.SetActive(currentFloor != 5);
    }

    private void Update()
    {
        if (IsLiftRunning)
        {
            Sync();
            _lastIsLiftRunning = true;
        }
        else
        {
            if (_lastIsLiftRunning)
            {
                _lastIsLiftRunning = false;
                Sync();
            }
        }
    }

    void Sync()
    {
        SyncLiftDisplayer();
        SyncFloorLight();
        SyncSmallControlPanel();
    }

    void SyncLiftDisplayer()
    {
        _crtFloor += GameTime.deltaTime * liftSpeed * (_goDownDirection ? -1f : 1f);
        liftNumLabel.text = currentFloor.ToString();
    }

    void SyncSmallControlPanel()
    {
        scp_floor1Light.SetActive(IsLiftRunning && _targetFloor == 1);
        scp_floor2Light.SetActive(IsLiftRunning && _targetFloor == 2);
        scp_floor3Light.SetActive(IsLiftRunning && _targetFloor == 3);
        scp_floor4Light.SetActive(IsLiftRunning && _targetFloor == 4);
        scp_floor5Light.SetActive(IsLiftRunning && _targetFloor == 5);
        scp_RedLight.SetActive(IsLiftRunning);
    }

    public void TryShowLiftControlPanel()
    {
        if (IsLiftRunning)
        {
            //SoundSystem.instance.Play("dudu");
        }
        else
        {
            ShowLiftControlPanel();
        }
    }

    public void ShowLiftControlPanel()
    {
        liftControlPanel.gameObject.SetActive(true);
        liftControlPanel.ResetBtns();
    }

    public void HideLiftControlPanel()
    {
        liftControlPanel.gameObject.SetActive(true);
    }

    public void OnLiftDestinationSet(int intTargetFloor)
    {
        _targetFloor = (float)intTargetFloor;
        var delta = _targetFloor - _crtFloor;
        var duration = Mathf.Abs(delta) / liftSpeed;
        _timeArrive = GameTime.time + duration;
        _goDownDirection = delta < 0;

        liftDoors.doorLeft.Set(false, false);
        liftDoors.doorRight.Set(false, false);
    }

    void OnArrived()
    {
        liftDoors.doorLeft.Set(true, false);
        liftDoors.doorRight.Set(true, false);
    }
}