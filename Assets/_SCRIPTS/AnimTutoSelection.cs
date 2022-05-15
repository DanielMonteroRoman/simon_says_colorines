using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTutoSelection : MonoBehaviour
{
    [SerializeField] int numAnim;

    Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _anim.SetInteger("indice", numAnim);
    }

}
