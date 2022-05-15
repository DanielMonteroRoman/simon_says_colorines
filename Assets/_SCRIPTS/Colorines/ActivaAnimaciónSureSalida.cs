using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivaAnimaciónSureSalida : MonoBehaviour
{

    Animator _anim;
    [SerializeField]float time;
    string cerrar = "cerrar";

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }


    public void ActivaAnim()
    {
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        yield return new WaitForSecondsRealtime(time);

        _anim.SetTrigger(cerrar);
    }
}
