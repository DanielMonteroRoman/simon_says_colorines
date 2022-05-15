using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BlockButtonWheel : MonoBehaviour
{
    [SerializeField]UnityEngine.UI.Button wheelButt;
    //[SerializeField] GameObject desactivatedWheel;

    [SerializeField]int allowedTries;
    [SerializeField]int tries;

    [SerializeField] TMP_Text triesText;


    Timer _timerWheel;   


    private void Awake()
    {
        _timerWheel=GetComponent<Timer>();
    }
    private void Start()
    {    

        tries = PlayerPrefs.GetInt("try", 0);
        UpdateText();

        if(tries == allowedTries) wheelButt.interactable = false;
        else wheelButt.interactable = true;
    }
    private void Update()
    {

        if (_timerWheel.isFinished && wheelButt.interactable == false)
        {
            tries = 0;
            
            UpdateText();
            wheelButt.interactable = true;          
           
        }
        
        


    }


    public void Try()
    {
        tries++;
        SaveTries(tries);
        UpdateText();

        if (tries == allowedTries && wheelButt.interactable)
        {
            wheelButt.interactable = false;
            
        }
        if (tries == 1)
        {
             _timerWheel.IniciarTimer();
        }
    }



    void UpdateText()
    {
        triesText.text = tries.ToString() + " / " + allowedTries;
    }

    void SaveTries(int value)
    {
        PlayerPrefs.SetInt("try", value);
    }

    [ContextMenu("reset try")]
    void ResetTries()
    {
        PlayerPrefs.DeleteKey("try");
    }
}
