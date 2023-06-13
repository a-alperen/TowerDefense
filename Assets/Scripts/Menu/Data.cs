using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Data 
{
    public float totalGold;
    public float totalScore;
    public float highGold;
    public float highScore;
    public float marketGold;
    public List<List<int>> upgradeLevels;
    public List<List<float>> powerMultiplier;
    public List<List<float>> upgradePowers;
    public List<int> correctAnswers;
    public List<int> wrongAnswers;
    public Data()
    {
        totalGold = 0;
        totalScore = 0;
        highGold = 0;
        highScore = 0;
        marketGold = 0;

        upgradeLevels = new List<List<int>>
        {
            new int[4].ToList(),
            new int[4].ToList(),
            new int[4].ToList(),
        };
        powerMultiplier = new List<List<float>>
        {
            new List<float>{ 1.38f, 1.38f, 1.15f, 1.085f },
            new List<float>{ 1.38f, 1.38f, 1.15f, 1.06f },
            new List<float>{ 1.38f, 1.38f, 1.15f, 1.06f }
        };
        upgradePowers = new List<List<float>>
        {
            new List<float>{ 10 , 100 , 1, 2f }, // 0-Hasar 1-Can 2-Hız 3-Menzil
            new List<float>{ 20, 50, 1.5f, 3f },
            new List<float>{ 50, 250, 0.5f, 1.5f }
        };
        correctAnswers = new List<int> { 0, 0, 0 };
        wrongAnswers = new List<int> { 0, 0, 0 };
    }
}
