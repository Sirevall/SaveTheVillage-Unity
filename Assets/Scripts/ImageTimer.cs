using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTimer : MonoBehaviour
{
    [SerializeField] private Image _timeDisplay;
    [SerializeField] private float _timeMax;

    private bool _running = false;
    private bool _cycleCompleted = false;
    private float _timeLeft;

    private void Update()
    {
        if (_running)
        {
            Tick();
            CheckStatus();
            DisplayTimeLeft();
        }

    }
    public float TimeMax
    {
        set
        {
            _timeMax = value;
        }
    }
    public bool CycleCompleted
    {
        get
        {
            return _cycleCompleted;
        }
        set
        {
            _cycleCompleted = value;
        }
    }
    public bool Running
    {
        get
        {
            return _running;
        }
    }
    public void Enable()
    {
        _timeLeft = _timeMax;
        _running = true;
        _cycleCompleted = false;
    }
    private void Tick()
    {
        _timeLeft -= Time.deltaTime;
    }
    private void CheckStatus()
    {
        if (_timeLeft <= 0)
        {
            _running = false;
            _cycleCompleted = true;
        }
    }
    private void DisplayTimeLeft()
    {
        _timeDisplay.fillAmount = _timeLeft / _timeMax;
    }
}
