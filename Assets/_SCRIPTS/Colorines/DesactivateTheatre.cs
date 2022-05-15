using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivateTheatre : MonoBehaviour
{

    Animator _Anim;

    [SerializeField] float time;

    // Start is called before the first frame update
    void Awake()
    {
        _Anim = GetComponent<Animator>();
    }

    
    public void StartAnimation()
    {
        _Anim.SetTrigger("desactivar");
    }

    public void DesActivate()
    {
        StartCoroutine(DesactivateAfterAnim());
    }

    IEnumerator DesactivateAfterAnim()
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
}
