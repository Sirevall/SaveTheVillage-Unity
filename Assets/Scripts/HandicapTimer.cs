using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandicapTimer : MonoBehaviour
{
    [SerializeField] private Text _timeLeft;
    [SerializeField] private Text _info;
    [SerializeField] private float _timeMax;

    private bool _handicapeOut = false;

    void Update()
    {
        if (_timeMax >= 0)
        {
            _timeMax -= Time.deltaTime;
            _timeLeft.text = Math.Ceiling(_timeMax).ToString();
        }
        else if (_timeMax == -1f)
        {
            _handicapeOut = false;
        }
        else
        {
            _handicapeOut = true;
            _timeMax = -1f;
            _timeLeft.gameObject.SetActive(false);
            _info.gameObject.SetActive(false);
        }
    }
    public bool HandicapeOut
    {
        get
        {
            return _handicapeOut;
        }
    }
    public void RestartTimer(float time)
    {
        _handicapeOut = false;
        _timeLeft.gameObject.SetActive(true);
        _info.gameObject.SetActive(true);
        _timeMax = time;
    }
}
