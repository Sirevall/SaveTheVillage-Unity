using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandicapTimer : MonoBehaviour
{
    [SerializeField] private Text _timeLeftText;
    [SerializeField] private float _timeMax;

    private bool _handicapeOut = false;

    void Update()
    {
        if (_timeMax >= 0)
        {
            _timeMax -= Time.deltaTime;
            _timeLeftText.text = Math.Ceiling(_timeMax).ToString();
        }
        else if (_timeMax == -1f)
        {
            _handicapeOut = false;
        }
        else
        {
            _handicapeOut = true;
           _timeMax = -1f;
            _timeLeftText.gameObject.SetActive(false);
        }
    }
    public bool HandicapeOut
    {
        get
        {
            return _handicapeOut;
        }
    }
}
