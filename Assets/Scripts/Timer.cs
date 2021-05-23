using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time;
    public GameObject timerImage;
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
}
