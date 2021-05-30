﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _peasants;
    [SerializeField] private int _warriors;
    [SerializeField] private int _wheat;
    [SerializeField] private int[] _waveEnemies;

    [SerializeField] private int _peasantCost;
    [SerializeField] private int _warriorCost;

    [SerializeField] private int _peasantProduce;
    [SerializeField] private int _warriorConsume;

    [SerializeField] private ImageTimer _wheatProductionTimer;
    [SerializeField] private ImageTimer _wheatConsumeTimer;
    [SerializeField] private ImageTimer _waveTimer;
    [SerializeField] private ImageTimer _hireWarriorTimer;
    [SerializeField] private ImageTimer _hirePeasantTimer;
    [SerializeField] private HandicapTimer _handicapTimer;

    [SerializeField] private Text _infoResourcesText;
    [SerializeField] private Text _infoEnemyWaveText;

    [SerializeField] private Button _hireWarriorButton;
    [SerializeField] private Button _hirePeasantButton;

    private int _died = 0;
    private int _wave = 0;

    private void Start()
    {
        _wheatProductionTimer.Enable();
        _wheatConsumeTimer.Enable();
        PrintResourcesInfo();
        PrintWaveInfo();
    }
    private void Update()
    {
        CheckAmountWheat();

        if (_handicapTimer.HandicapeOut)
            _waveTimer.Enable();

        if (_wheatProductionTimer.CycleCompleted)
        {
            CreateWheat();
            _wheatProductionTimer.Enable();
        }

        if (_wheatConsumeTimer.CycleCompleted)
        {
            ConsumeWheat();
            _wheatConsumeTimer.Enable();
        }

        if (_hireWarriorTimer.CycleCompleted)
        {
            _hireWarriorTimer.gameObject.SetActive(false);
            _warriors++;
            _hireWarriorTimer.CycleCompleted = false;
            _hireWarriorButton.interactable = true;
        }

        if (_hirePeasantTimer.CycleCompleted)
        {
            _hirePeasantTimer.gameObject.SetActive(false);
            _peasants++;
            _hirePeasantTimer.CycleCompleted = false;
            _hirePeasantButton.interactable = true;
        }

        if (_waveTimer.CycleCompleted)
        {
            if (_wave < _waveEnemies.Length - 1)
            {
                _warriors -= _waveEnemies[_wave];
                _wave++;
                _waveTimer.Enable();
            }
            else
            {
                _warriors -= _waveEnemies[_wave];
                _waveTimer.CycleCompleted = false;
                _waveTimer.gameObject.SetActive(false);
            }
        }

        PrintResourcesInfo();
        PrintWaveInfo();
    }
    // Это бред отдельный метод на создание юнита.
    //Надо создавать или класс создания юнитов, или передавать в инспекторе метод с 2-мя параметрами.
    public void HireWarrior()
    {
            _wheat -= _warriorCost;
            _hireWarriorButton.interactable = false;
            _hireWarriorTimer.gameObject.SetActive(true);
            _hireWarriorTimer.Enable();
    }
    public void HirePeasant()
    {
            _wheat -= _peasantCost;
            _hirePeasantButton.interactable = false;
            _hirePeasantTimer.gameObject.SetActive(true);
            _hirePeasantTimer.Enable();
    }
    private void CreateWheat()
    {
        _wheat += (_peasants * _peasantProduce);
    }
    private void ConsumeWheat()
    {
        if (_wheat >= (_warriors * _warriorConsume))
        {
            _wheat -= (_warriors * _warriorConsume);
        }
        else
        {
            _died += _warriors;
            _warriors = 0;
        }
    }
    private void PrintResourcesInfo()
    {
        _infoResourcesText.text = $"Warriors: {_warriors}\n\nPeasants: {_peasants}\n\nWheat: {_wheat}\n\nWarriors Died: {_died}";
    }
    private void PrintWaveInfo()
    {
        if (_wave == _waveEnemies.Length - 1)
        {
            _infoEnemyWaveText.text = $"WAVE: {_wave + 1}\nEnemies: {_waveEnemies[_wave]}";
        }
        else
        {
            _infoEnemyWaveText.text = $"WAVE: {_wave + 1}\nEnemies: {_waveEnemies[_wave]}\n\nNEXT WAVE: {_wave + 2}\nEnemies: {_waveEnemies[_wave + 1]}";
        }

    }
    private void CheckAmountWheat()
    {
        if (_wheat < _warriorCost || _hireWarriorTimer.Running == true)
            _hireWarriorButton.interactable = false;
        else
            _hireWarriorButton.interactable = true;
        if (_wheat < _peasantCost || _hirePeasantTimer.Running == true)
            _hirePeasantButton.interactable = false;
        else
            _hirePeasantButton.interactable = true;
    }
}
