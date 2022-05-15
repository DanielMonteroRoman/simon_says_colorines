using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    Animator _anim;
    string apretado = "Highlighted", noApretado = "Normal";
    private UnityEngine.UI.Button _boton;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _boton = GetComponent<UnityEngine.UI.Button>();
        
    }

    private void OnEnable()
    {
        //_boton.interactable = true;
    }

    public void PressedButton()
    {
        _anim.SetTrigger(apretado);
        _anim.SetTrigger(noApretado);
    }
    


}
