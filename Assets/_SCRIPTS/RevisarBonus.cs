using System.Collections;
using UnityEngine;


public class RevisarBonus : MonoBehaviour
{
    [SerializeField] int numberOfBonus;
    [SerializeField] bool AlreadyUsed;
    [SerializeField] bool blocked;

    [SerializeField] UnityEngine.UI.Button revisarButton;

    GameManager gameMan;

    [Header("Estrellas del menú inicial")]
    [SerializeField] GameObject canvStar1, canvStar2, canvStar3;

    MoneyManager _moneyManager;

    [SerializeField] int price;

    [SerializeField] UnityEngine.UI.Button shopButton;

    [SerializeField] GameObject revisarImage;
    [SerializeField] GameObject prohibidoImage;

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

            revisarImage.SetActive(true);
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
            prohibidoImage.GetComponent<Animator>().SetTrigger("activa");
        }
    }

    /// <summary>
    /// Añade el bonus sin gastar dinero, activado por la ruleta de la fortuna
    /// </summary>
    /// 
    [ContextMenu("activar")]
    public void AddBonusForFree()
    {
        if (numberOfBonus < 3 )
        {
            numberOfBonus++;

            ActivateCanvasStars(numberOfBonus);

            AddStar();    //TODO: meter una animación         
        }
        else
        {
            StartCoroutine(ProhibidoSignal());            
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
        ActivateRevButton();
    }

    public void SustStar()
    {
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
        
    }


    
    IEnumerator ProhibidoSignal()
    {
        prohibidoImage.GetComponent<Animator>().SetTrigger("activa");
        yield return new WaitForSecondsRealtime(1);
        prohibidoImage.GetComponent<Animator>().SetTrigger("activa");
        yield return new WaitForSecondsRealtime(1);
        prohibidoImage.GetComponent<Animator>().SetTrigger("activa");
        yield return new WaitForSecondsRealtime(1);
        prohibidoImage.GetComponent<Animator>().SetTrigger("activa");
    }
}


