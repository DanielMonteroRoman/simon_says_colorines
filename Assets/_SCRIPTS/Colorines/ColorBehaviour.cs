using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ColorBehaviour : MonoBehaviour
{
    [SerializeField] Light light;
    [SerializeField,Range(0,2)] float iluminationTime;
    Animator _anim;
    string apretar = "apretar";

    GameManager gameManager;

    AudioSource _aSource;
    [SerializeField] AudioClip sound;

    [SerializeField] float soundVolume;

    bool bloqueoMouseUp=true;

    [SerializeField] float timePressed;

    ParticleSystem particles;

    private void Awake()
    {
        light = transform.GetChild(0).gameObject.GetComponent<Light>();
        _anim = GetComponent<Animator>();
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
        _aSource = GetComponent<AudioSource>();

        particles = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        iluminationTime = gameManager.betweenColorsTime-0.1f;
        
    }


    //TODO: que se ilumine al apretar y ocurra todo esto al soltar
    void  OnMouseDown()
    {
        if (gameManager.gameState == GameManager.GameState.Playing && bloqueoMouseUp)
        {
            if (gameManager.turn == GameManager.Turn.Player1 && !gameManager.blocker)
            {
                bloqueoMouseUp = false;
                Ilumine();
                particles.Play();
                Timer(true);
            }
        }       
        
    }
    
    private void OnMouseUp()
    {
        if(gameManager.gameState == GameManager.GameState.Playing && !bloqueoMouseUp)
        {
            if (gameManager.turn == GameManager.Turn.Player1 && !gameManager.blocker)
            {
                Timer(false);

                if (timePressed>=0.2f) DontIlumine();
                else StartCoroutine(DontIlumineWithExitTime());

               
                bloqueoMouseUp = true;                           
                
                gameManager.VeryfyColors(this.gameObject);
                
            }
        }
        
        
    }


    IEnumerator Ilumination()
    {
        if (light.enabled == false)
        {
            light.enabled = true;

            _anim.SetBool(apretar, true);

            _aSource.PlayOneShot(sound, soundVolume);

            yield return new WaitForSecondsRealtime(iluminationTime);

            light.enabled = false;

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
        if (!gameManager.blocker)
        {
            _aSource.PlayOneShot(sound, soundVolume);
            light.enabled = true;

            _anim.SetBool(apretar, true);
        }
        
    }
    
    void DontIlumine()
    {
        _aSource.Stop();

        light.enabled = false;

        _anim.SetBool(apretar, false);
    }

    private IEnumerator DontIlumineWithExitTime()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        _aSource.Stop();

        light.enabled = false;

        _anim.SetBool(apretar, false);
    }

    void Timer(bool active)
    {
        if (active)
        {
            timePressed = Time.time;
        }
        else if (!active) timePressed = Time.time -timePressed;
    }

    


}
