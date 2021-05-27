using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image _timerField;

    private float _timerTime;
    
    private bool _timerWork = false;
    private float _timeLeft;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (_timerWork)
        {
            Tick();
            CheckTimerStatus();
            DisplayTimeLeft();
        }

    }
    public bool TimerWork
    {
        get 
        {
            return _timerWork;
        }
    }

    public float TimerTime
    {
        set
        {
            _timerTime = value;
        }
    }

    public void StartTimer()
    {
        _timeLeft = _timerTime;
        _timerWork = true;
    }

    private void Tick()
    {
        _timeLeft -= Time.deltaTime;
    }
    private void CheckTimerStatus()
    {
        if (_timeLeft <= 0)
        {
            _timerWork = false;
        }
    }
    private void DisplayTimeLeft()
    {
        _timerField.fillAmount = _timeLeft / _timerTime;
    }
}
