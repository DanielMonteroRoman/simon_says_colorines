using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevisarBonus : MonoBehaviour
{
    [SerializeField] int numberOfBonus;
    [SerializeField] bool AlreadyUsed;
    [SerializeField] bool blocked;

    [SerializeField] UnityEngine.UI.Button revisarButton;

    GameManager gameMan;


    [SerializeField] List<GameObject> stars;

    [Header("Estrellas del menú inicial")]
    [SerializeField] GameObject canvStar1, canvStar2, canvStar3;

    MoneyManager _moneyManager;

    [SerializeField] int price;

    [SerializeField] UnityEngine.UI.Button shopButton;

    private void Awake()
    {
        _moneyManager = GetComponent<MoneyManager>();

        gameMan = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();

        numberOfBonus = PlayerPrefs.GetInt("numberOfBonus", 0);
    }


    private void Start()
    {
        if (numberOfBonus == 0 || gameMan.OneColor)
        {
            DesactivateRevButton();
        }
        else ActivateRevButton();

        for(int i = 0; i < numberOfBonus; i++)
        {
            stars[i].SetActive(true);
        }

        ActivateCanvasStars(numberOfBonus);
    }


    private void Update()
    {
        PlayerPrefs.SetInt("numberOfBonus", numberOfBonus);
        if (numberOfBonus == 3) shopButton.interactable = false;
        else if (!shopButton.interactable) shopButton.interactable = true;

    }

    public void UseBonus()
    {

        if (gameMan.turn == GameManager.Turn.Player1)
        {
            if (numberOfBonus > 0) numberOfBonus--;
            SustStar();


            gameMan.RepeatPlease(); 
            gameMan.ColorToCompareFromListReset();//Resetea la secuencia que has comenzado a responder

            

            DesactivateRevButton();
            ActivateCanvasStars(numberOfBonus);
        }
        else  if (gameMan.turn == GameManager.Turn.Computer)
        {
            
        }
        

    }

    [ContextMenu("AddBonus")]
    public void AddBonus()
    {
        if (numberOfBonus < 3 && _moneyManager._availableMoney >= price)
        {
            numberOfBonus++;

            ActivateCanvasStars(numberOfBonus);

            AddStar();

            _moneyManager.SustractMoney(price);
        }
        else
        {
            //Activar mensaje de no hay dinero /Ad
        }
    }

    /// <summary>
    /// Añade el bonus sin gastar dinero, activado por la ruleta de la fortuna
    /// </summary>
    public void AddBonusForFree()
    {
        if (numberOfBonus < 3 )
        {
            numberOfBonus++;

            ActivateCanvasStars(numberOfBonus);

            AddStar();    //TODO: meter una animación         
        }
    }

    public void ActivateRevButton()
    {
        revisarButton.interactable = true;
    }

    public void DesactivateRevButton()
    {
        revisarButton.interactable = false;
    }

    public void AddStar()
    {
        foreach (GameObject star in stars)
        {
            if (!star.activeInHierarchy)
            {
                star.SetActive(true);
                break;
            }
        }

        ActivateRevButton();
    }

    public void SustStar()
    {
        foreach (GameObject star in stars)
        {
            if (star.activeInHierarchy)
            {
                star.SetActive(false);
                break;
            }
        }
        if (numberOfBonus == 0) DesactivateRevButton();
    }

    public void BlockUntilEndOfGame()
    {
        DesactivateRevButton();
    }

    public void DeBlockForNextGame()  
    {
        if(numberOfBonus>0) ActivateRevButton();
    }


    public void ResetNumberOfBonus()
    {
        numberOfBonus = 0;
    }


    void ActivateCanvasStars(int number)
    {
        switch (number)
        {
            case 0:
                canvStar1.SetActive(false);
                canvStar2.SetActive(false);
                canvStar3.SetActive(false);
                break ;
            case 1:
                canvStar1.SetActive(true);
                canvStar2.SetActive(false);
                canvStar3.SetActive(false);
                break;
            case 2:
                canvStar1.SetActive(true);
                canvStar2.SetActive(true);
                canvStar3.SetActive(false);
                break;
            case 3:
                canvStar1.SetActive(true);
                canvStar2.SetActive(true);
                canvStar3.SetActive(true);
                break;
        }

        Debug.Log("CHEQUEANDO ESTRELLAS");
        
    }
}


