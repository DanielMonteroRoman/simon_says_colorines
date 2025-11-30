using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AvoidVoidInput : MonoBehaviour
{
    [SerializeField] TMP_InputField input;

    [SerializeField] UnityEngine.UI.Button aceptar, rechazar;

    string userName;

    [SerializeField] GameObject pFabGO;
    PlayFabLogIn pFab;


    private void Awake()
    {
        pFab = pFabGO.GetComponent<PlayFabLogIn>();
    }
    private void Start()
    {
        userName = pFab.userNameText.text;

        

        if (userName == "" || userName == null)
        {
            rechazar.interactable = false;
            aceptar.interactable = false;
            
        }
        else
        {
            aceptar.interactable=false;
            rechazar.interactable = true;
        }
    }

    public void Verificar()
    {
        if (userName == "" || userName == null)
        {
            rechazar.interactable = false;

            if (input.text == null || input.text == "")
            {
                aceptar.interactable = false;                
            }
            else
            {
                aceptar.interactable = true;
                
            }
        }
        else
        {
            aceptar.interactable = false;

            if (input.text == null || input.text == "")
            {
                aceptar.interactable = false;
            }
            else
            {
                aceptar.interactable = true;

            }
        }
       
        
    }

}
