using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

    [SerializeField] private GameObject ResultPanel;

    private int _died = 0;
    private int _wave = 0;
    private Text _peasantCostText;
    private Text _warriorCostText;
    private bool _gameOver = false;
    private bool _villageCaptured = false;
    private bool _warriorsConsumeWheat = false;

    public UnityEvent GameEnd;

    private void Start()
    {
        _peasantCostText = _hirePeasantButton.transform.Find("PeasantCostText").GetComponent<Text>();
        _warriorCostText = _hireWarriorButton.transform.Find("WarriorCostText").GetComponent<Text>();

        _wheatProductionTimer.Enable();

        PrintResourcesInfo();
        PrintWaveInfo();
    }
    private void Update()
    {
        SwitchButton();

        if (_warriors == 0)
        {
            _wheatConsumeTimer.Disable();
            _warriorsConsumeWheat = false;
        }

        if (_handicapTimer.HandicapeOut)
        {
            _waveTimer.Enable();
        }

        if (_wheatProductionTimer.CycleCompleted && _warriorsConsumeWheat == true)
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
        CheckEndGame();
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
    public void RestartGame()
    {
        // Надо как-то возвращать к стартовым значениям. Больше переменных?
        _died = 0;
        _wave = 0;
        _peasants = 2;
        _wheat = 50;
        _gameOver = false;
        _villageCaptured = false;
        _handicapTimer.RestartTimer(30f);
        _waveTimer.gameObject.SetActive(true);
        _waveTimer.RestartTimer();
        _wheatProductionTimer.Enable();
        _wheatConsumeTimer.Enable();
    }
    private void HireWarrior()
    {
        if (_warriors == 0)
        {
            _wheatConsumeTimer.Enable();
        }

        _hireWarriorTimer.gameObject.SetActive(false);
        _warriors++;
        _hireWarriorTimer.CycleCompleted = false;
        _hireWarriorButton.interactable = true;
        _warriorsConsumeWheat = true;
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
            if (_warriorsConsumeWheat)
            {

            }
        }
        else
        {
            _died += _warriors;
            _villageCaptured = true;
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

            _infoWave.text = $"WAVE: {_wave + 1}/{_waveEnemies.Length}\n\n\t\t{_waveEnemies[_wave]}";
        }
        else
        {
            _infoWave.text = $"WAVE: {_wave + 1}/{_waveEnemies.Length}\n\n\t\t{_waveEnemies[_wave]}";
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
            _gameOver = true;
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
    private void PrintResults()
    {
        Text titleResult = ResultPanel.transform.Find("TitleResult").GetComponent<Text>();
        Text results = ResultPanel.transform.Find("ResultText").GetComponent<Text>();

        if (_villageCaptured)
        {
            titleResult.text = "You lose!";
            titleResult.color = new Color(0.83f, 0.27f, 0.27f);
            results.text = $"Waves: {_wave}/{_waveEnemies.Length}\n\nWarriors died: {_died}";
        }
        else if (_gameOver)
        {
            titleResult.text = "You win!";
            titleResult.color = new Color(0.056f, 0.56f, 0.08f);
            results.text = $"Waves: {_wave + 1}/{_waveEnemies.Length}\n\nWarriors died: {_died}";
        }

        
    }
    private void CheckEndGame()
    {
        if (_gameOver || _villageCaptured)
        {
            PrintResults();
            GameEnd.Invoke();
        }
    } 
}
