using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBlock : MonoBehaviour
{

    Animator _anim;

    string activa = "activa";

    void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        _anim.SetTrigger(activa);
    }
}
