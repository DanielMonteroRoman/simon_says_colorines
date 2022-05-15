using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] int round, score, numberOfBalls, pointsToAdd;
    public int _score;

    [SerializeField] int flip = 1;
    GameManager gameManager;

    public int highScore;

    [Space]
    [Header("OPCIONES DE JUEGO")]

    
    [SerializeField] bool acceleration;
    [SerializeField] bool oneColor;
    [SerializeField] bool reverse;
    [SerializeField] bool flipFlop;

    [Header("TEXTOS")]

    [SerializeField] TMP_Text scoreText;

    [SerializeField] TMP_Text highScoreText;

    [SerializeField] TMP_Text numberOfColorsText;

    [SerializeField] TMP_Text FlipRound;

    [SerializeField] GameObject flipFlopPanel;

    string upgrade = "upgrade";

    LoadData loadData;

    //VARIABLES PARA EL PANEL DE PUNTUACIONES LOCALES
    //PUEDO ACCEDER A LA VARIABLE DE PLAYERPREF CREADA AQUÍ DESDE EL 
    //SCRIPT DEL PANEL DE PUNTUACIONES?

    [SerializeField] TMP_Text norm, rev, one, revOne, total, 
        normF, revF, oneF, revOneF, totalF, bigScore;

    //VARIABLE PARA SUBIR A LA NUBE LA PUNTUACIÓN 
    int bigHighScore;
    public int _bigHScore;

    MoneyManager moneyMan;

    

    private void Awake()
    {
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
        loadData = GameObject.Find("GAME DATA").GetComponent<LoadData>();
        moneyMan = GameObject.Find("EXTRAS MANAGER").GetComponent<MoneyManager>();  

        highScore = loadData.highScore;

    }

    private void Start()
    {

        highScoreText.text = highScore.ToString();

        WriteTheScores();

        UpdatePartialHighScores(0);        
    }

   
    void Update()
    {
        round = gameManager.round;

        if (score > highScore)
        {
            UpdateHighScore();
            UpdateHighScoreText();
        }
       
    }

    int CalculatePointsToAdd()
    {
        int extraPoints = 0;
        int pointsCalculated;

        if (oneColor) extraPoints += 10;
        if (acceleration) extraPoints += 1;
        if (reverse) extraPoints += 5;

        pointsCalculated = extraPoints + numberOfBalls;

        return pointsCalculated;
    }

    public void UpdateScore()
    {
        score = score + flip*(pointsToAdd + round);

        UpdateScoreText(score);

        _score=score;

        moneyMan.AddMoney(flip * pointsToAdd + round);
               
    }

    public void UpdateHighScore()
    {
        highScore = score;

        loadData.highScore = highScore;

        loadData.SaveData(highScore);

    }


    public void ExternalScoreUpdater()
    {
        UpdatePartialHighScores(score);
        WriteTheScores();
    }

    /// <summary>
    /// Guarda los récords de los diferentes modos.
    /// </summary>
   public void UpdatePartialHighScores(int puntos)
    {             
        if (!flipFlop)
        {
            if (!oneColor)
            {
                switch (reverse)
                {
                    case false:
                        if(puntos> PlayerPrefs.GetInt("normScore"))
                        {
                            PlayerPrefs.SetInt("normScore", puntos);
                        }                        
                        break;

                    case true:
                        if(puntos> PlayerPrefs.GetInt("revScore"))
                        {
                            PlayerPrefs.SetInt("revScore", puntos);
                        }                        
                        break;
                }
            }
            else if (oneColor)
            {
                switch (reverse)
                {
                    case false:
                        if (puntos > PlayerPrefs.GetInt("oneColScore"))
                        {
                            PlayerPrefs.SetInt("oneColScore", puntos);
                        }                            
                        break;

                    case true:
                        if (puntos > PlayerPrefs.GetInt("revOneScore"))
                        {
                            PlayerPrefs.SetInt("revOneScore", puntos);
                        }                            
                        break;
                }
            }

            PlayerPrefs.SetInt("total", PlayerPrefs.GetInt("normScore") +
                PlayerPrefs.GetInt("revScore") +
                PlayerPrefs.GetInt("oneColScore") +
                PlayerPrefs.GetInt("revOneScore"));       
            
            
        }
        else if (flipFlop)
        {
            if (!oneColor)
            {
                switch (reverse)
                {
                    case false:
                        if (puntos > PlayerPrefs.GetInt("normFScore"))
                        {
                            PlayerPrefs.SetInt("normFScore", puntos);
                        }                            
                        break;

                    case true:
                        if (puntos > PlayerPrefs.GetInt("revFScore"))
                        {
                            PlayerPrefs.SetInt("revFScore", puntos);
                        }
                        break;
                }
            }
            else if (oneColor)
            {
                switch (reverse)
                {
                    case false:
                        if (puntos > PlayerPrefs.GetInt("oneColFScore"))
                        {
                            PlayerPrefs.SetInt("oneColFScore", puntos);
                        }                            
                        break;

                    case true:
                        if (puntos > PlayerPrefs.GetInt("revOneFScore"))
                        {
                            PlayerPrefs.SetInt("revOneFScore", puntos);
                        }                            
                        break;
                }
            }
            PlayerPrefs.SetInt("totalF", PlayerPrefs.GetInt("normFScore") +
                PlayerPrefs.GetInt("revFScore") +
                PlayerPrefs.GetInt("oneColFScore") +
                PlayerPrefs.GetInt("revOneFScore"));
        }

        _bigHScore = (PlayerPrefs.GetInt("total") + PlayerPrefs.GetInt("totalF"));
        bigHighScore = _bigHScore;

        PlayerPrefs.SetInt("bigHighScore", bigHighScore);

        Debug.Log(PlayerPrefs.GetInt("normScore") + ";" +
        PlayerPrefs.GetInt("revScore") + ";" +
        PlayerPrefs.GetInt("oneColScore") + ";" +
        PlayerPrefs.GetInt("total") + ";" +
        PlayerPrefs.GetInt("revOneScore") + ";" + "flip-flop: "+

        PlayerPrefs.GetInt("normFScore") + ";" +
        PlayerPrefs.GetInt("revFScore") + ";" +
        PlayerPrefs.GetInt("oneColFScore") + ";" +
        PlayerPrefs.GetInt("totalF") + ";" +
        PlayerPrefs.GetInt("revOneFScore"));
    }


    public void UpdateOptions()  // PARA EL BOTÓN DE START O DE REJUGAR
    {
        StartCoroutine(WaitToUpdate());

        score = 0;
        UpdateScoreText(0);
        _score = score;
        

    }

    IEnumerator WaitToUpdate()
    {
        yield return new WaitForSeconds(0.1f);
        round = 0;
        oneColor = gameManager.OneColor;
        numberOfBalls = gameManager.numberOfBalls;
        flip = 1;
        if (gameManager.gameSpeed == GameManager.GameSpeed.acceleration)
        {
            acceleration = true;
        }
        else if (gameManager.gameSpeed == GameManager.GameSpeed.constant)
        {
            acceleration = false;
        }

        if (gameManager.modes == GameManager.Modes.Reverse)
        {
            reverse = true;
        }
        else if (gameManager.modes == GameManager.Modes.normal)
        {
            reverse = false;
        }

        if (gameManager.flipFlop == true) flipFlop = true;
        else flipFlop = false;

        ActivateFlipFlopPanel(flipFlop);

        numberOfColorsText.text = numberOfBalls.ToString();

        FlipRound.text = "x" + flip.ToString();

        pointsToAdd = CalculatePointsToAdd();
    }

    void ActivateFlipFlopPanel(bool activate)
    {
        flipFlopPanel.SetActive(activate);
    }

    void UpdateScoreText(int scoreToAdd)
    {
        //ANIMACIÓN QUE SE SUME CADA UNIDAD

        scoreText.text = score.ToString();
        scoreText.text = score.ToString();
    }

    void UpdateHighScoreText()
    {

        highScoreText.text = scoreText.text;
    }

    public void UpdateFlipValue()
    {
        flip +=1;
        FlipRound.text = "x" + flip.ToString();
        flipFlopPanel.GetComponent<Animator>().SetTrigger(upgrade);

    }

    public void AbortFlipFlop()
    {
        flip += 1;
        FlipRound.text = "x" + flip.ToString();
        flipFlopPanel.GetComponent<Animator>().SetTrigger("restart");
    }

    public void RestarHighScoreText()
    {
        highScoreText.text = 0.ToString();
        
    }

    public void RestartHighScore()
    {
        highScore = 0;
    }
       

    public void WriteTheScores()
    {
        norm.text = PlayerPrefs.GetInt("normScore", 0).ToString();
        rev.text = PlayerPrefs.GetInt("revScore", 0).ToString();
        one.text = PlayerPrefs.GetInt("oneColScore", 0).ToString();
        revOne.text = PlayerPrefs.GetInt("revOneScore", 0).ToString();
        total.text = PlayerPrefs.GetInt("total", 0).ToString();

        //if (highScore >= 300)
        {
            normF.text = PlayerPrefs.GetInt("normFScore", 0).ToString();
            revF.text = PlayerPrefs.GetInt("revFScore", 0).ToString();
            oneF.text = PlayerPrefs.GetInt("oneColFScore", 0).ToString();
            revOneF.text = PlayerPrefs.GetInt("revOneFScore", 0).ToString();
            totalF.text = PlayerPrefs.GetInt("totalF", 0).ToString();
        }

        bigScore.text = PlayerPrefs.GetInt("bigHighScore").ToString();
        
    }

    public void ResetValues()
    {
        PlayerPrefs.DeleteKey("normScore");
        PlayerPrefs.DeleteKey("revScore");
        PlayerPrefs.DeleteKey("oneColScore");
        PlayerPrefs.DeleteKey("total");
        PlayerPrefs.DeleteKey("revOneScore");
        
        PlayerPrefs.DeleteKey("normFScore");
        PlayerPrefs.DeleteKey("revFScore");
        PlayerPrefs.DeleteKey("oneColFScore");
        PlayerPrefs.DeleteKey("totalF");
        PlayerPrefs.DeleteKey("revOneFScore");

        PlayerPrefs.DeleteKey("bigHighScore");

        _bigHScore = 0;
        bigHighScore = 0;

        Debug.Log(PlayerPrefs.GetInt("normScore") +";"+
        PlayerPrefs.GetInt("revScore") + ";" +
        PlayerPrefs.GetInt("oneColScore") + ";" +
        PlayerPrefs.GetInt("total") + ";" +
        PlayerPrefs.GetInt("revOneScore") + ";" +

        PlayerPrefs.GetInt("normFScore") + ";" +
        PlayerPrefs.GetInt("revFScore") + ";" +
        PlayerPrefs.GetInt("oneColFScore") + ";" +
        PlayerPrefs.GetInt("totalF") + ";" +
        PlayerPrefs.GetInt("revOneFScore"));
        
    }
}
