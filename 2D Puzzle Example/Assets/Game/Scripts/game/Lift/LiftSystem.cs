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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _crtFloor = 5;
        liftDoors.doorRight.Set(true, true);
        liftDoors.doorLeft.Set(true, true);
    }

    private bool _lastIsLiftRunning = false;
    public bool IsLiftRunning
    {
        get { return GameTime.time <= _timeArrive; }
    }

    private void Update()
    {
        if (IsLiftRunning)
        {
            SyncLiftDisplayer();
            _lastIsLiftRunning = true;
        }
        else
        {
            if (_lastIsLiftRunning)
            {
                _lastIsLiftRunning = false;
                SyncLiftDisplayer();
            }
        }
    }

    void SyncLiftDisplayer()
    {
        _crtFloor += GameTime.deltaTime * liftSpeed * (_goDownDirection ? -1f : 1f);
        liftNumLabel.text = currentFloor.ToString();

        //sync small control panel
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
    }

    public void HideLiftControlPanel()
    {
        liftControlPanel.gameObject.SetActive(true);
        liftControlPanel.ResetBtns();
    }

    public void OnLiftDestinationSet(int intTargetFloor)
    {
        _targetFloor = (float)intTargetFloor;
        var delta = _targetFloor - _crtFloor;
        var duration = Mathf.Abs(delta) / liftSpeed;
        _timeArrive = GameTime.time + duration;
        _goDownDirection = delta < 0;
    }
}