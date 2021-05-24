using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int peasants;
    public int warriors;
    public int wheat;
    public int peasantPrice;
    public int warriorPrice;
    public int peasantProduce;
    public int warriorConsume;
    public float wheatProductionTime;

    public Text infoText;

    public Timer wheatProductionTimer;

    private int _dead = 0;

    private void Awake()
    {
        wheatProductionTimer.timeMax = wheatProductionTime;
    }

    private void Start()
    {
        wheatProductionTimer.StartTimer();
        PrintInfo();
    }

    private void Update()
    {
        PrintInfo();
        if (wheatProductionTimer.timerWork == false)
        {
            CreateWheat();
            wheatProductionTimer.StartTimer();
        }
    }

    public void CreateWarrior()
    {
        if (wheat > 0 && (wheat - warriorPrice) > 0)
        {
            ++warriors;
            wheat -= warriorPrice;
        }
        
    }
    private void CreateWheat()
    {
        wheat += (peasants * peasantProduce);
    }

    private void PrintInfo()
    {
        infoText.text = $"Воины: {warriors}\n\n Крестьяне: {peasants}\n\n Пшеница: {wheat}\n\n Погибло: {_dead}";
    }
}
