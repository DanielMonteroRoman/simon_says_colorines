using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    int availableMoney;
    public int _availableMoney { get => availableMoney; set => availableMoney = value; }

    [SerializeField] TMP_Text moneyText;

    private void Start()
    {
        availableMoney = GetMoney();
        UpdateMoneyText(availableMoney);
    }

    public void AddMoney(int money)
    {
        if(availableMoney <= 999999999)
        {
            availableMoney += money;
        }

        UpdateMoneyText(availableMoney);
        SetMoney();
    }
    public void SustractMoney(int money)
    {
        availableMoney -= money;

        UpdateMoneyText(availableMoney);
        SetMoney();
    }


    void UpdateMoneyText(int money)
    {
        moneyText.text = availableMoney.ToString() + " $";
    }

    void SetMoney()
    {
        PlayerPrefs.SetInt("AvailableMoney", availableMoney);
    }

    int GetMoney()
    {
       int moneyLoaded = PlayerPrefs.GetInt("AvailableMoney");

        return moneyLoaded;
    }

    [ContextMenu("ResetMoney")]
    private void ResetMoney()
    {
        PlayerPrefs.SetInt("AvailableMoney", 0);
        availableMoney = 0;
        UpdateMoneyText(0);

    }
    private void Update()
    {
        if(availableMoney<0)availableMoney = 0;
    }
}
