using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlumineOptions : MonoBehaviour
{
    [SerializeField] float timePressed;

    AudioSource _aSource;
    [SerializeField] AudioClip sound;
   

    Animator _anim;

    string apretar = "apretar";

    SoundSelection soundSele;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _aSource = GetComponent<AudioSource>();

        soundSele =GameObject.Find("SoundControler").GetComponent<SoundSelection>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        sound = soundSele.selectedSound;
    }

    void OnMouseDown()
    {       
        Ilumine();
        Timer(true);
    }

    private void OnMouseUp()
    {
        Timer(false);
       if (timePressed >= 0.1f)
       {
         DontIlumine();         
       }
       else
       {
          StartCoroutine(DontIlumineWithExitTime());
       }      


    }

    void Ilumine()
    {
        //if (!gameManager.blocker)
        {
            _aSource.PlayOneShot(sound);

            _anim.SetBool(apretar, true);
        }

    }
    void DontIlumine()
    {
        _aSource.Stop();

        _anim.SetBool(apretar, false);

       
    }

    private IEnumerator DontIlumineWithExitTime()
    {
        yield return new WaitForSecondsRealtime(0.05f);
        _aSource.Stop();

        _anim.SetBool(apretar, false);
               

    }

    void Timer (bool active)
    {
        if (active)
        {
            timePressed = Time.time;
        }
        else if (!active) timePressed = Time.time - timePressed;
    }   
}
