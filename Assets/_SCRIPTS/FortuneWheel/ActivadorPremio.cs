using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivadorPremio : MonoBehaviour
{
    [SerializeField] GameObject hasWonPanel;
    public bool activeAnimation = false;
    [SerializeField] float time;


    
    public void ActivaPremio(string animation)
    {

        StartCoroutine(ActiveAnimationCoroutine(animation));
         
    }

    IEnumerator ActiveAnimationCoroutine(string animation)
    {
        activeAnimation = false;

        yield return new WaitForSecondsRealtime(time);

        hasWonPanel.SetActive(true);
        hasWonPanel.GetComponent<Animator>().SetTrigger(animation);
        yield return new WaitForSecondsRealtime(5f);

        hasWonPanel.SetActive(false);

    }
}
