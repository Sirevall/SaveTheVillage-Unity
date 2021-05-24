using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeMax;
    public Image timerField;
    public bool timerWork = false;
    private float _timeLeft;

    private void Start()
    {
        _timeLeft = timeMax;
    }
    private void Update()
    {
        if (timerWork)
        {
            Tick();
            CheckTimerStatus();
            DisplayTimeLeft();
        }

    }

    public void StartTimer()
    {
        timerWork = true;
    }

    private void Tick()
    {
        _timeLeft -= Time.deltaTime;
    }
    private void CheckTimerStatus()
    {
        if (_timeLeft <= 0)
        {
            timerWork = false;
        }
    }
    private void DisplayTimeLeft()
    {
        timerField.fillAmount = _timeLeft / timeMax;
    }
}
