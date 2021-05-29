using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _peasants;
    [SerializeField] private int _warriors;
    [SerializeField] private int _wheat;

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

    private void Start()
    {
        _wheatProductionTimer.Enable();
        _wheatConsumeTimer.Enable();
        PrintInfo();
    }

    private void Update()
    {
        if (_handicapTimer.HandicapeOut == true)
            _waveTimer.Enable();

        if (_wheatProductionTimer.Running == false)
        {
            CreateWheat();
            _wheatProductionTimer.Enable();
        }

        if (_wheatConsumeTimer.Running == false)
        {
            ConsumeWheat();
            _wheatConsumeTimer.Enable();
        }

        if (_hireWarriorTimer.CycleCompleted == true)
        {
            _hireWarriorTimer.gameObject.SetActive(false);
            _warriors++;
            _hireWarriorTimer.CycleCompleted = false;
            _hireWarriorButton.interactable = true;
        }

        if (_hirePeasantTimer.CycleCompleted == true)
        {
            _hirePeasantTimer.gameObject.SetActive(false);
            _peasants++;
            _hirePeasantTimer.CycleCompleted = false;
            _hirePeasantButton.interactable = true;
        }

        PrintInfo();
    }
    // Это бред отдельный метод на создание юнита.
    //Надо создавать или класс создания юнитов, или передавать в инспекторе метод с 2-мя параметрами.
    public void HireWarrior()
    {
        if (_wheat >= _warriorCost)
        {
            _wheat -= _warriorCost;
            _hireWarriorButton.interactable = false;
            _hireWarriorTimer.gameObject.SetActive(true);
            _hireWarriorTimer.Enable();
        }
    }
    public void HirePeasant()
    {
        if (_wheat >= _peasantCost)
        {
            _wheat -= _peasantCost;
            _hirePeasantButton.interactable = false;
            _hirePeasantTimer.gameObject.SetActive(true);
            _hirePeasantTimer.Enable();
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
    private void PrintInfo()
    {
        _infoResourcesText.text = $"Warriors: {_warriors}\n\n Peasants: {_peasants}\n\n Wheat: {_wheat}\n\n Warriors Died: {_died}";
    }
}
