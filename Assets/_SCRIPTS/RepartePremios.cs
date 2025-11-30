using System.Collections;
using UnityEngine;

public class RepartePremios : MonoBehaviour
{
    MoneyManager _moneyManager;
    ExtraLiveBonus _extraLive;
    RevisarBonus _revisarBonus;

    InterstitialAdsButton interstitialAdsButton;

    private void Awake()
    {
        _moneyManager = GetComponent<MoneyManager>();
        _extraLive = GetComponent<ExtraLiveBonus>();
        _revisarBonus = GetComponent<RevisarBonus>();

       interstitialAdsButton = GameObject.Find("ADS MANAGER").GetComponent<InterstitialAdsButton>();
    }

    
    public void SelectorDePremio(int i)
    {
        switch (i)
        {
            case 0:
                _moneyManager.AddMoney(1000);
                break;
            case 1:
                StartCoroutine(AnuncioCorr());
                break;
            case 2:
                _extraLive.AddBonusForFree();
                break;
            case 3:
                _moneyManager.AddMoney(500);
                break;
            case 4:
                _moneyManager.AddMoney(250);
                break;
            case 5:
                _moneyManager.AddMoney(AvailableMoney());
                break;
            case 6:
                _moneyManager.AddMoney(750);
                break;
            case 7:
                _revisarBonus.AddBonusForFree();
                break;
            case 8:
                _moneyManager.AddMoney(100);
                break;
            case 9:
                _moneyManager.AddMoney(250);
                break;
            case 10:
                _moneyManager.AddMoney(500);
                break;
            case 11:
                StartCoroutine(AnuncioCorr());
                break;
            case 12:
                _moneyManager.AddMoney(10000);
                break;
            case 13:
                _moneyManager.AddMoney(1000);
                break;
            case 14:
                _revisarBonus.AddBonusForFree();
                break;
            case 15:
                _moneyManager.AddMoney(750);
                break;
            case 16:
                _moneyManager.AddMoney(1000);
                break;
            case 17:
                _extraLive.AddBonusForFree();
                break;
            case 18:
                _moneyManager.AddMoney(500);
                break;
            case 19:
                _moneyManager.AddMoney(-(AvailableMoney()/2));
                break;
            case 20:
                _moneyManager.AddMoney(1500);
                break;
            case 21:
                _moneyManager.AddMoney(5000);
                break;
            case 22:
                _moneyManager.AddMoney(500);
                break;
            case 23:
                _revisarBonus.AddBonusForFree();
                break;



        }

    }

    IEnumerator AnuncioCorr()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        interstitialAdsButton.ShowAd();
    }

    private int AvailableMoney()
    {
        return _moneyManager._availableMoney;
    }
}
