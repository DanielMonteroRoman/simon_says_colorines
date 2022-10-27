using System.Collections;
using UnityEngine;



public class NewColorBehaviour : MonoBehaviour
{
    //[SerializeField] Light light;
    [SerializeField,Range(0,2)] float iluminationTime;
    Animator _anim;
    string apretar = "apretar";

    GameManager gameManager;

    AudioSource _aSource;
    [SerializeField] AudioClip sound;

    [SerializeField] float soundVolume;

   [SerializeField] bool bloqueoMouseUp=true;

    [SerializeField] float timePressed;

    [SerializeField] ParticleSystem pushParticles;

    public AudioClip vibration, bajada, subida;

    ColorSoundLists colorSLists;

    [SerializeField] int IDNumber;

    
    private void Awake()
    {
        pushParticles = transform.GetChild(4).gameObject.GetComponent<ParticleSystem>();
        _anim = GetComponent<Animator>();
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
        _aSource = GetComponent<AudioSource>();

        colorSLists = GameObject.Find("ColorSounds").GetComponent<ColorSoundLists>();

        
    }

    private void Update()
    {
       if (gameManager.betweenColorsTime >0.85f && gameManager.betweenColorsTime < 1.05f)
        {          
            iluminationTime = gameManager.betweenColorsTime - 0.3f; //no sé por qué en este intervalo no se ve el apagado entre luz y luz, por eso cambio este valor a 0.3f
        }
        else iluminationTime = gameManager.betweenColorsTime-0.1f;
        
    }
    
    void  OnMouseDown()
    {
        if (gameManager.gameState == GameManager.GameState.Playing && bloqueoMouseUp)
        {
            if (gameManager.turn == GameManager.Turn.Player1 && !gameManager.blocker)
            {
                bloqueoMouseUp = false;
                Ilumine();
                pushParticles.Play();
                Timer(true);
            }
        }       
        
    }
    
    private void OnMouseUp()
    {      
        if (gameManager.gameState == GameManager.GameState.Playing && !bloqueoMouseUp)
        {
            if (gameManager.turn == GameManager.Turn.Player1 && !gameManager.blocker)
            {
                pushParticles.Stop();

                Timer(false);

                if (timePressed >= 0.1f)
                {
                    DontIlumine();
                    gameManager.VeryfyColors(this.gameObject);
                }
                else
                {
                    gameManager.blocker = true;

                    StartCoroutine(DontIlumineWithExitTime());
                }
                bloqueoMouseUp = true; 
            }
        }             
    }


    IEnumerator Ilumination()
    {
       if (!_anim.GetBool(apretar))
        {
            _anim.SetBool(apretar, true);

            _aSource.PlayOneShot(colorSLists.currentList[IDNumber], colorSLists.soundVolume);

            yield return new WaitForSecondsRealtime(iluminationTime);

            _anim.SetBool(apretar, false);

            _aSource.Stop();

        }
       

    }
        
    public void IluminationAcces()
    {
        StartCoroutine(Ilumination());
    }

    
    void Ilumine()
    {   
        //if (!gameManager.blocker)
        {
            _aSource.PlayOneShot(colorSLists.currentList[IDNumber], colorSLists.soundVolume);
           
            _anim.SetBool(apretar, true);
        }
        
    }
    

    /// <summary>
    /// Apaga la luz iniciando la animación de apagado instantaneamente
    /// </summary>
    void DontIlumine()
    {
        _aSource.Stop();

        _anim.SetBool(apretar, false);

       gameManager.blocker = false;


    }


    /// <summary>
    /// Si la pulsación ha sido muy corta (menos de 0.2sec) inicia la animación de apagado
    /// dejando un margen de 0.2 segundos.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DontIlumineWithExitTime()
    {
        yield return new WaitForSecondsRealtime(0.05f);
        _aSource.Stop();

        _anim.SetBool(apretar, false);

       gameManager.blocker = false;
        gameManager.VeryfyColors(this.gameObject);

    }

    void Timer(bool active)
    {
        if (active)
        {
            timePressed = Time.time;
        }
        else if (!active) timePressed = Time.time -timePressed;
    }



    /// <summary>
    /// sonidos de la animación de FLIP FLOP que ordena el Game Manager
    /// </summary>




    public void FlipSound()
    {
        StartCoroutine(FlipSoundsCorr());
    }

    IEnumerator FlipSoundsCorr() 
    {
        _aSource.PlayOneShot(vibration,5);

        yield return new WaitForSecondsRealtime(3.3f);

        _aSource.PlayOneShot(bajada, 0.5f);
    }

    public void SoundsFlop()
    {
        _aSource.PlayOneShot(subida, 0.5f);
        //StartCoroutine(FlopSoundsCorr());
    }

    IEnumerator FlopSoundsCorr()
    {
        

        yield return new WaitForSecondsRealtime(1.75f);

        
    }
    


}
