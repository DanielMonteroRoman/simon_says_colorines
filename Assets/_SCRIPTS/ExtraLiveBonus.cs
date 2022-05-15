using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLiveBonus : MonoBehaviour
{

    [SerializeField] int numberOfLives;
    [SerializeField] bool AlreadyUsed;
    [SerializeField] bool blocked;

    [SerializeField] UnityEngine.UI.Button extraLiveButton;

    [Header("Estrellas del menú inicial")]
    [SerializeField] GameObject canvStar1;
    [SerializeField] GameObject canvStar2;

    MoneyManager _moneyManager;

    [SerializeField] int price;

    [SerializeField] UnityEngine.UI.Button shopButton;
    

    private void Awake()
    {
        numberOfLives = PlayerPrefs.GetInt("numberOfLives", 0);

        _moneyManager = GetComponent<MoneyManager>();
    }


    private void Start()
    {
        if (numberOfLives == 0)
        {
            DesactivateExtraLiveButton();
        }
        else ActivateExtraLiveButton();

        ActivateCanvasStars(numberOfLives);
    }


    private void Update()
    {
        PlayerPrefs.SetInt("numberOfLives", numberOfLives);

        if (numberOfLives ==2) shopButton.interactable = false;
        else if(!shopButton.interactable) shopButton.interactable = true;
    }

    public void UseBonus()
    {
        if (numberOfLives > 0) numberOfLives--;

        if (numberOfLives == 0) DesactivateExtraLiveButton();

        DesactivateExtraLiveButton();

        ActivateCanvasStars(numberOfLives);        
    }

    
    public void AddBonus()
    {
        if (numberOfLives < 2 && _moneyManager._availableMoney >= price)
        {
            numberOfLives++;

            ActivateCanvasStars(numberOfLives);

            ActivateExtraLiveButton();

            _moneyManager.SustractMoney(price);
        }
        else
        {
            //ACTIVAR PANEL DE NO TIENES DINERO //ADS
        }        
    }

    public void AddBonusForFree()
    {
        if (numberOfLives < 2 )
        {
            numberOfLives++;

            ActivateCanvasStars(numberOfLives);

            ActivateExtraLiveButton();
           
        }
    }

    public void ActivateExtraLiveButton()
    {
        extraLiveButton.interactable = true;
    }

    public void DesactivateExtraLiveButton()
    {
        extraLiveButton.interactable = false;
    }
            

    public void BlockUntilEndOfGame()
    {
        DesactivateExtraLiveButton();
    }

    public void DeBlockForNextGame() //Botón Play o Replay
    {
        if (numberOfLives > 0) ActivateExtraLiveButton();
    }


    public void ResetNumberOfBonus()  //usar cada 24h
    {
        numberOfLives = 0;

    }


    void ActivateCanvasStars(int number)
    {
        switch (number)
        {
            case 0:
                canvStar1.SetActive(false);
                canvStar2.SetActive(false);
                break;
            case 1:
                canvStar1.SetActive(true);
                canvStar2.SetActive(false);
                break;
            case 2:
                canvStar1.SetActive(true);
                canvStar2.SetActive(true);
                break;
            
        }      

    }
}
