using System;
using System.Collections;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;

    [SerializeField] GameObject bigBottonPopUp;

    public void BotonDePlay()
    {
        StartCoroutine(WaitForButtonAnim());        
    }


    private IEnumerator WaitForButtonAnim()
    {
        yield return new WaitForSecondsRealtime(0f);
        menuPanel.SetActive(false);        
    }   

    public void PopUpCounter()
    {
        PlayerPrefs.SetInt("NumberOfPlays", PlayerPrefs.GetInt("NumberOfPlays", 0) + 1);

        int numberOfplays = PlayerPrefs.GetInt("NumberOfPlays");

        if (numberOfplays == 20 || numberOfplays == 50 || numberOfplays == 100)
            bigBottonPopUp.SetActive(true);
    }

    public void PopUpInactivation() 
    {
        PlayerPrefs.SetInt("NumberOfPlays", 101);
    }
}
