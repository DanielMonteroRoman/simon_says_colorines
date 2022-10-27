using System.Collections;
using UnityEngine;

public class DesactivateAfterFewSec : MonoBehaviour
{
    [SerializeField] float sec;
    
    private void OnEnable()
    {
        StartCoroutine(Desactivate(sec));
    }


    IEnumerator Desactivate(float sec)
    {
        yield return new WaitForSeconds(sec);
        this.gameObject.SetActive(false);
    }
}
