using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _peasants;
    [SerializeField] private int _warriors;
    [SerializeField] private int _wheat;

    [SerializeField] private int _peasantPrice;
    [SerializeField] private int _warriorPrice;

    [SerializeField] private int _peasantProduce;
    [SerializeField] private int _warriorConsume;

    //Поля времени здесь для настройки, в дальнейшем время таймера перенести в класс таймера
    [SerializeField] private float _wheatProductionTime;
    [SerializeField] private float _wheatConsumeTime;
    [SerializeField] private float _waveTime;
    [SerializeField] private float _hireWarriorTime;
    [SerializeField] private float _hirePeasantTime;

    [SerializeField] private Timer _wheatProductionTimer;
    [SerializeField] private Timer _wheatConsumeTimer;
    [SerializeField] private Timer _waveTimer;
    [SerializeField] private Timer _hireWarriorTimer;
    [SerializeField] private Timer _hirePeasantTimer;

    [SerializeField] private Text _infoResourcesText;
    [SerializeField] private Text _infoEnemyWaveText;

    private int _dead = 0;

    private void Awake()
    {
        _wheatProductionTimer.TimerTime = _wheatProductionTime;
        _wheatConsumeTimer.TimerTime = _wheatConsumeTime;
        _waveTimer.TimerTime = _waveTime;
        _hirePeasantTimer.TimerTime = _hirePeasantTime;
        _hireWarriorTimer.TimerTime = _hireWarriorTime;
    }

    private void Start()
    {
        _wheatProductionTimer.StartTimer();
        _wheatConsumeTimer.StartTimer();
        PrintInfo();
    }

    private void Update()
    {
        PrintInfo();
        if (_wheatProductionTimer.TimerWork == false)
        {
            CreateWheat();
            _wheatProductionTimer.StartTimer();
        }

        if (_wheatConsumeTimer.TimerWork == false)
        {
            ConsumeWheat();
            _wheatConsumeTimer.StartTimer();
        }
    }
    // Это бред отдельный метод на создание юнита.
    //Надо создавать или класс создания юнитов, или передавать в инспекторе метод с 2-мя параметрами.
    public void HireWarrior()
    {
        if (_wheat > 0 && (_wheat - _warriorPrice) > 0)
        {
            ++_warriors;
            _wheat -= _warriorPrice;
        }
        
    }
    public void HirePeasant()
    {
        if (_wheat > 0 && (_wheat - _peasantPrice) > 0)
        {
            ++_peasants;
            _wheat -= _peasantPrice;
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
            _dead += _warriors;
            _warriors = 0;
        }
    }
    private void PrintInfo()
    {
        _infoResourcesText.text = $"Воины: {_warriors}\n\n Крестьяне: {_peasants}\n\n Пшеница: {_wheat}\n\n Погибло: {_dead}";
    }
}
