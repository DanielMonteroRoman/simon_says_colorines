using UnityEngine;
using System;

public class unlockAnim : MonoBehaviour
{
    Animator _anim;

    public bool activator;

    void Awake()
    {
         _anim=GetComponent<Animator>();
        activator = false;
    }

    private void Start()
    {       
        Unlocked(activator);
    }

    
    public void Unlocked(bool block)
    {
        _anim.SetBool("unlocked",block);
        activator = false;
    }

    public void normal()
    {
        try
        {
            _anim.SetBool("unlocked", false);
        }
        catch(Exception e) 
        { 
            Debug.LogException(e);
            Debug.Log("NULL REFERENCE Y UNASIGNED (ANIMATOR) EXCEPTION CAPTADA"); 
        }
        
    }
}
