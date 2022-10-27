using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, GameOver, Menu };
    public GameState gameState = GameState.Playing;

    public enum Turn { Computer, Player1 }
    public Turn turn = Turn.Computer;

    public enum Modes { normal, Reverse }
    public Modes modes = Modes.normal;

    public enum GameSpeed { constant, acceleration };
    public GameSpeed gameSpeed;

    [SerializeField] public bool OneColor = false, flipFlop = false;

    public bool prize;

    [SerializeField] private bool changeing;

    [SerializeField] public int round = 1, record;


    [SerializeField]
    List<GameObject> existentElements, currentElementList, flipFlopList;

    [SerializeField] GameObject newElement;


    [SerializeField] int ColorToCompareFromList;

    [SerializeField] GameObject gameOverPanel, menuPanel, confetiParticles;

    [SerializeField] bool computerPlaying;

    public bool blocker; // bloquea en el scripts del colorBehaviour la iluminación
    private bool flipFlopBlocker; //bloquea la elección de GO para el FlipFlop
   [SerializeField] private bool repeatBlocker; //Se encarga de bloquear elección de nuevo color(repetición)


    AudioSource _aSource;
    [SerializeField] AudioClip perdida;


    [Range(0, 2)] public float betweenColorsTime, restartTime;

    float lastColorTime;

    [SerializeField] GameObject logoTurn;
    Animator animatorLogo;
    string CPUTurn = "CPUTurn", restart = "restart", playerTurn = "PlayerTurn", flipFlopTurn = "flipFlopTurn";


    [SerializeField] TMP_Text recordNumberText, recordModeText;

    FlipFlopColors flipFlopColors;

    public int numberOfBalls;

   ScoreManager scoreMan;

    [SerializeField] GameObject flipFlopPanel;


   // RecogerDatos recogeDato; // BORRAR CUANDO ACABE LA PRUEBA

    [SerializeField] UnityEngine.UI.Button exitButton;

    
    private void Awake()
    {
        Application.targetFrameRate = 30;

        animatorLogo = logoTurn.GetComponent<Animator>();

        restartTime = betweenColorsTime;

        //prize = true;

        flipFlopColors = GameObject.Find("FlipFloper").GetComponent<FlipFlopColors>();

       scoreMan = GameObject.Find("SCORE MANAGER").GetComponent<ScoreManager>();

       //recogeDato = GameObject.Find("RECOGER DATOS").GetComponent<RecogerDatos>(); //BORRAR CUANDO ACABE LA PRUEBA

        
    }

    private void Start()
    {
        _aSource = GetComponent<AudioSource>();

        LoadData();
    }

    private void Update()
    {
        if (gameState == GameState.Playing && !repeatBlocker)
        {
            if (turn == Turn.Computer && !computerPlaying)
            {
                PlayComputerTurn();
            }
        }

        if (betweenColorsTime > 0.85f && betweenColorsTime < 1.05f)
        {
            lastColorTime = betweenColorsTime - 0.3f; //no sé por qué en este intervalo no se ve el apagado entre luz y luz, por eso cambio este valor a 0.3f
        }
        else lastColorTime = betweenColorsTime - 0.1f;

        if (round % 3 == 0 && round != 0)
        {
            changeing = true;
        }

        numberOfBalls = existentElements.Count;
    }

    private void PlayComputerTurn()
    {
        if (flipFlop && changeing)
        {
            StartCoroutine(InactiveExitButton());

            //elección de colores
            flipFlopList = FlipFlopColorChoice();
            flipFlopBlocker = true;

            // ACTIVA LOS PUNTOS DEL FLIP FLOP
            scoreMan.UpdateFlipValue();

            //animación FLIP e intercambio
            FlipAnimationActivation(flipFlopList);
            StartCoroutine(FlopAnimation(flipFlopList));
            //Animaciones

            AddNewElementToComputerList();

            changeing = false;

        }
        else
        {            
            AddNewElementToComputerList();
            StartCoroutine(ListActivation());  //HAY QUE MIRAR CÓSAS EN ESTE MÉTODO
        }
        computerPlaying = true;
    }

    [ContextMenu("Prueba")]
    public void RepeatPlease()
    {
        turn = Turn.Computer;
        UpdateLogoTurn();
        repeatBlocker = true;
        StartCoroutine(ListActivation());
    }

    /// <summary>
    /// Es el turno del ordenador, recuerda toda la lista de colores en orden si no está en modo "a ciegas (oneColor)".
    /// </summary>
    /// <returns></returns>
    IEnumerator ListActivation()
    {
        blocker = true;
        UpdateLogoTurn();

        if (OneColor && !repeatBlocker)
        {
            yield return new WaitForSecondsRealtime(betweenColorsTime);
            newElement.gameObject.GetComponent<NewColorBehaviour>().IluminationAcces();
        }
        else
        {
            foreach (GameObject element in currentElementList)
            {
                yield return new WaitForSecondsRealtime(betweenColorsTime);
                element.gameObject.GetComponent<NewColorBehaviour>().IluminationAcces();
            }
        }

        turn = Turn.Player1;

        computerPlaying = false;
        StartCoroutine(WaitForLastIlumination());

        repeatBlocker = false;
    }

    /// <summary>
    /// Elige una bola de entre las qe están activas
    /// </summary>
    /// <returns></returns>
    int RandomElement()
    {
        int number;
        number = UnityEngine.Random.Range(0, existentElements.Count);

        return number;
    }


    /// <summary>
    /// Se introduce una nueva bola elegida al azar a la lista
    /// </summary>
    void AddNewElementToComputerList()
    {
        newElement = existentElements[RandomElement()];

        currentElementList.Add(newElement);

    }



    /// <summary>
    /// Compara los colores que el jugador está activando con la lista de colores
    /// </summary>
    /// <param name="element"> se pasa la bola que el jugador está apretando</param>

    public void VeryfyColors(GameObject element)
    {

        if (modes == Modes.Reverse)
        {
            currentElementList.Reverse();

            if (element == currentElementList[ColorToCompareFromList])
            {
                ColorToCompareFromList++;


                currentElementList.Reverse();
            }
            else GameOver();
        }
        else
        {
            if (element == currentElementList[ColorToCompareFromList])
            {
                ColorToCompareFromList++;

            }
            else GameOver();
        }


        if (ColorToCompareFromList == currentElementList.Count)
        {
            blocker = true;

            if (!(gameState == GameState.GameOver)) scoreMan.UpdateScore();

            NextTurn();

        }
    }


    /// <summary>
    /// Se deja un espacio de tiempo entre la última jugada y la siguiente ronda
    /// </summary>
    /// <returns></returns>

    IEnumerator WaitForLastIlumination()
    {
        yield return new WaitForSecondsRealtime(lastColorTime);
        blocker = false;
        UpdateLogoTurn();
    }


    /// <summary>
    /// Se inicia la siguiente ronda
    /// </summary>
    void NextTurn()
    {
        turn = Turn.Computer;

        ColorToCompareFromListReset();
        blocker = false;
        round++;

        //if (modes == Modes.timeDown) TimeDown();

        if (gameSpeed == GameSpeed.acceleration && !OneColor) TimeDown();
    }


    /// <summary>
    /// ESTE CÓDIGO SE EJECUTA AL DARLE AL BOTON START EN EL MENÚ, LLAMA AL MÉTODO FIRST PLAY
    /// </summary>
    public void Play()
    {
        RestartBetweenColorsTime();
        
        gameState = GameState.Playing;

        currentElementList.Clear(); //1UP

        round = 1; //1UP
        betweenColorsTime = restartTime; //1UP

        existentElements.Clear();

        changeing = false;
                
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSecondsRealtime(1f);
        gameState = GameState.Playing;
    }

    /// <summary>
    /// EJECUTA TODA LA LÓGICA DEL GAME OVER, SONIDOS, PANELES, RESETEOS
    /// </summary>
    void GameOver()
    {        
       // recogeDato.RecogidaDeDatos();
        Debug.Log("BORRA TODO LO DE LA RECOGIDA DE DATOS");

        scoreMan.ExternalScoreUpdater();
        _aSource.PlayOneShot(perdida, 0.5f);

        gameState = GameState.GameOver; 
        turn = Turn.Computer;

        gameOverPanel.SetActive(true);

        ColorToCompareFromList = 0;

        animatorLogo.SetTrigger(restart);         

        flipFlopPanel.SetActive(false);

        changeing = false;

        if(modes==Modes.Reverse) currentElementList.Reverse();
    }

    /// <summary>
    /// RESETEA EL COLOR TO COMPARE AL FINALIZAR TU TURNO
    /// </summary>
    public void ColorToCompareFromListReset()
    {
        ColorToCompareFromList = 0;
    }

    /// <summary>
    /// ACELERA SEGÚN PASAN LAS RONDAS. SOLO SE LLAMA EN MODO TIME DOWN
    /// </summary>
    void TimeDown()
    {
        if (betweenColorsTime >= 0.23f)
        {
            betweenColorsTime = betweenColorsTime - 0.08f;
        }
        else betweenColorsTime = 0.20f;
    }



    /// <summary>
    /// sale de la aplicación
    /// </summary>
    public void Salir()
    {
        StartCoroutine(WaitForExit());

    }

    /// <summary>
    /// Cambia de color el logo a rojo cuando es el turno de la CPU y verde cuando lo 
    /// es del player.
    /// </summary>
    void UpdateLogoTurn()
    {
        if (turn == Turn.Computer) animatorLogo.SetTrigger(CPUTurn);
        else if (turn == Turn.Player1) animatorLogo.SetTrigger(playerTurn);
    }


    IEnumerator WaitForExit()
    {
        yield return new WaitForSecondsRealtime(0.30f);
        Application.Quit();

    }


    void SaveData()
    {
        PlayerPrefs.SetInt("saveRecord", record);
    }

    void LoadData()
    {
        record = PlayerPrefs.GetInt("saveRecord", 0);
    }

    void ResetData()
    {
        PlayerPrefs.DeleteKey("saveRecord");
        LoadData();

    }



    IEnumerator ConfetiMaker()
    {
        confetiParticles.SetActive(true);

        prize = false;

        yield return new WaitForSecondsRealtime(20);

        confetiParticles.SetActive(false);
    }



    public void ExtraLivePlay()
    {
        gameState = GameState.Playing;

        RepeatPlease();
        
        //bloquear la elección de nuevo color
    }


    //--------------MÉTODOS PARA EL FLIP FLOP------------------


    List<GameObject> FlipFlopColorChoice()
    {
        int a = 0; int b = 0;

        while (a == b)
        {
            a = RandomElement();
            b = RandomElement();
        }

        List<GameObject> GO = new List<GameObject>();

        GO.Add(existentElements[a]);
        GO.Add(existentElements[b]);

        return GO;

    }

    void FlipAnimationActivation(List<GameObject> colorList)
    {
        foreach (GameObject color in colorList)
        {
            color.GetComponent<Animator>().SetTrigger("Flip");
        }

        flipFlopColors.GO1 = colorList[0];
        flipFlopColors.GO2 = colorList[1];
    }

    /// <summary>
    /// Animación de flop y cambio de posición
    /// </summary>
    /// <returns></returns>
    IEnumerator FlopAnimation(List<GameObject> colorList)
    {
        animatorLogo.SetTrigger(flipFlopTurn);

        foreach (GameObject color in colorList)
        {
            color.GetComponent<NewColorBehaviour>().FlipSound();
        }

        yield return new WaitForSecondsRealtime(5f);

        flipFlopColors.change = true;

        foreach (GameObject color in colorList)
        {
            color.GetComponent<NewColorBehaviour>().SoundsFlop();
        }

        foreach (GameObject color in colorList)
        {
            color.GetComponent<Animator>().SetTrigger("Flop");
        }

        yield return new WaitForSecondsRealtime(3.5f);

        animatorLogo.SetTrigger(CPUTurn);

        StartCoroutine(ListActivation());
    }



    // -------------CAMBIO DE MODO POR BOTÓN DE LA UI-----------

    public void NormalMode()
    {
        modes = Modes.normal;
    }
    public void ReverseMode()
    {
        modes = Modes.Reverse;
    }

    public void OneColorMode()
    {
        OneColor = !OneColor;
    }

    public void FlipFlopMode()
    {
        flipFlop = !flipFlop;
    }

    public void ConstantSpeed()
    {
        gameSpeed = GameSpeed.constant;
    }
    public void AccelerationSpeed()
    {
        gameSpeed = GameSpeed.acceleration;
    }

    public void GeneraEscenario(List<GameObject> colors)
    {
        foreach (GameObject color in colors)
        {
            existentElements.Add(color);
        }
    }

    public void ChangeBetweenColorsTime(float value)
    {
        betweenColorsTime = value;
    }

    public void RestartBetweenColorsTime()
    {
        if (gameSpeed == GameSpeed.acceleration) betweenColorsTime = 1f; //1.5f;

        restartTime = betweenColorsTime;

    }

    public void ButtonGameOver()
    {
        GameOver();
    }

    public void ButtonExitInGame()
    {
        scoreMan.ExternalScoreUpdater();

        gameState = GameState.GameOver;
        turn = Turn.Computer;

        ColorToCompareFromList = 0;
        currentElementList.Clear(); 

        round = 1; 
        betweenColorsTime = restartTime;

        animatorLogo.SetTrigger(restart);

        existentElements.Clear(); 

        flipFlopPanel.SetActive(false); 

        changeing = false; 

        StopAllCoroutines();

        menuPanel.SetActive(true);


        flipFlopBlocker = false;
        flipFlopList.Clear();

        scoreMan.AbortFlipFlop();

        computerPlaying = false;

    }

    IEnumerator InactiveExitButton()
    {
        exitButton.interactable = false;
        yield return new WaitForSecondsRealtime(7);
        exitButton.interactable = true;
    }

}
