using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModePanel : MonoBehaviour
{
    [SerializeField] bool active = false;

    [SerializeField] GameObject modoPanel, tiendaPanel;

    [SerializeField] int desactivationTime;

    [Header("POP UP")]

    [SerializeField] private int playCounter;
   
    private string playCount = "playCount";
    [Space]
    [SerializeField] private GameObject lookYourScorePanel;

    
    [SerializeField] private GameObject buyMyGame;

    



    private void OnEnable()
    {
        playCounter = PlayerPrefs.GetInt(playCount, 0);

        if(playCounter == 1)
        {
            lookYourScorePanel.SetActive(true);
            
        }
        else if(playCounter == 5 || playCounter==20)
        {
            buyMyGame.SetActive(true);
        }
    }

    public void DesActivationPanel()
    {

      StartCoroutine(DesactivatePanel());
    }


    IEnumerator DesactivatePanel()
    {

        yield return new WaitForSecondsRealtime(desactivationTime);
        modoPanel.gameObject.SetActive(false);
        
    }

    public void DesActivationShopPanel()
    {

        StartCoroutine(DesactivateShopPanel());
    }


    IEnumerator DesactivateShopPanel()
    {

        yield return new WaitForSecondsRealtime(desactivationTime);
        tiendaPanel.gameObject.SetActive(false);

    }


    /// <summary>
    ///  cuenta el número de veces que se le ha dado a play para abrir popUps
    /// </summary>
    public void PlayCounter()
    {
        playCounter++;
        PlayerPrefs.SetInt(playCount, playCounter);
    }





}
