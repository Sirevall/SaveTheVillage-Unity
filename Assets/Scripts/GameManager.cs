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
    [SerializeField] private Text _infoWave;
    [SerializeField] private Text _infoNextWave;

    [SerializeField] private Button _hireWarriorButton;
    [SerializeField] private Button _hirePeasantButton;

    private int _died = 0;
    private int _wave = 0;
    private Text _peasantCostText;
    private Text _warriorCostText;

    private void Start()
    {
        _peasantCostText = _hirePeasantButton.transform.Find("PeasantCostText").GetComponent<Text>();
        _warriorCostText = _hireWarriorButton.transform.Find("WarriorCostText").GetComponent<Text>();

        _wheatProductionTimer.Enable();
        _wheatConsumeTimer.Enable();

        PrintResourcesInfo();
        PrintWaveInfo();
    }
    private void Update()
    {
        SwitchButton();

        if (_handicapTimer.HandicapeOut)
        {
            _waveTimer.Enable();
        }

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
            HireWarrior();
        }

        if (_hirePeasantTimer.CycleCompleted)
        {
            HirePeasant();
        }

        if (_waveTimer.CycleCompleted)
        {
            EnemyAttack();
            WaveState();
        }

        _peasantCostText.text = _peasantCost.ToString();
        _warriorCostText.text = _warriorCost.ToString();
        PrintResourcesInfo();
        PrintWaveInfo();
    }
    public void HireWarriorButtonClick()
    {
        _wheat -= _warriorCost;
        _hireWarriorButton.interactable = false;
        _hireWarriorTimer.gameObject.SetActive(true);
        _hireWarriorTimer.Enable();
    }
    public void HirePeasantButtonClick()
    {
        _wheat -= _peasantCost;
        _peasantCost = CostMultiplier(_peasantCost, multiplier: 2);
        _hirePeasantButton.interactable = false;
        _hirePeasantTimer.gameObject.SetActive(true);
        _hirePeasantTimer.Enable();
    }
    private void HireWarrior()
    {
        _hireWarriorTimer.gameObject.SetActive(false);
        _warriors++;
        _hireWarriorTimer.CycleCompleted = false;
        _hireWarriorButton.interactable = true;
    }
    private void HirePeasant()
    {
        _hirePeasantTimer.gameObject.SetActive(false);
        _peasants++;
        _hirePeasantTimer.CycleCompleted = false;
        _hirePeasantButton.interactable = true;
    }
    private void EnemyAttack()
    {
        if (_warriors >= _waveEnemies[_wave])
        {
            _warriors -= _waveEnemies[_wave];
            _died += _waveEnemies[_wave];
        }
        else
        {
            _died += _warriors;
        }

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
        _infoResourcesText.text = $"{_warriors}\n\n{_peasants}\n\n{_died}\n\n{_wheat}";
    }
    private void PrintWaveInfo()
    {
        if (_wave == _waveEnemies.Length - 1)
        {
            // Сомнительная вещь, но надо выключить родительский объект
            _infoNextWave.gameObject.transform.parent.gameObject.SetActive(false);

            _infoWave.text = $"WAVE: {_wave + 1}\n\n\t\t{_waveEnemies[_wave]}";
        }
        else
        {
            _infoWave.text = $"WAVE: {_wave + 1}\n\n\t\t{_waveEnemies[_wave]}";
            _infoNextWave.text = $"NEXT WAVE\n\n\t\t{_waveEnemies[_wave + 1]}";
        }

    }
    private void WaveState()
    {
        if (_wave < _waveEnemies.Length - 1)
        {
            _wave++;
            _waveTimer.Enable();
        }
        else
        {
            _waveTimer.CycleCompleted = false;
            _waveTimer.gameObject.SetActive(false);
        }
    }
    private void SwitchButton()
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
    private int CostMultiplier(int cost, int multiplier)
    {
        return cost += multiplier;
    }

}
