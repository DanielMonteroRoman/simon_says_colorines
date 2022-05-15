using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;

    public void BotonDePlay()
    {
        StartCoroutine(WaitForButtonAnim());
        
    }


    private IEnumerator WaitForButtonAnim()
    {
        yield return new WaitForSecondsRealtime(0f);
        menuPanel.SetActive(false);
        
    }   
}
