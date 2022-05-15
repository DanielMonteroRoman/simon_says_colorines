using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombillasCabecera : MonoBehaviour
{
    string activar ="activar";
    Animator _anim;

   [SerializeField] bool blocker;

    [SerializeField] int value;

    private void Awake()
    {
        _anim = GetComponent<Animator>();

        
    }

    private void OnEnable()
    {
        blocker = false;

        Debug.Log("BLOCKER FALSE");
    }
    private void Update()
    {
        if (!blocker) StartCoroutine(LightsOff());
        
    }

    IEnumerator LightsOff()
    {
        blocker = true;
        
        yield return new WaitForSecondsRealtime(Random.Range(0,value));

        _anim.SetTrigger(activar);

        blocker = false;
    }



}
