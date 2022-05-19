using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMoneyTimesThree : MonoBehaviour
{
    MoneyManager moneyMan;
    ScoreManager scoreMan;

    private void Awake()
    {
        moneyMan = GameObject.Find("EXTRAS MANAGER").GetComponent<MoneyManager>();
        scoreMan = GameObject.Find("SCORE MANAGER").GetComponent<ScoreManager>();
    }

    public void AddThreeTimes()
    {
        moneyMan.AddMoney(scoreMan._score * 2);
    }
}
