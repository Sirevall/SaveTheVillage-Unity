using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time;
    public Image timerField;
    public bool timeOver;
    private float _timeLeft;

    void Start()
    {
        _timeLeft = time;
    }

    // Update is called once per frame
    void Update()
    {
        Tick();
        CheckTimerStatus();
        DisplayTimeLeft();
    }

    private void Tick()
    {
        _timeLeft -= Time.deltaTime;
    }
    private void CheckTimerStatus()
    {
        if (_timeLeft > 0)
        {
            timeOver = false;
        }
        else
        {
            _timeLeft = time;
            timeOver = true;
        }
    }
    private void DisplayTimeLeft()
    {
        timerField.fillAmount = _timeLeft / time;
    }
}
