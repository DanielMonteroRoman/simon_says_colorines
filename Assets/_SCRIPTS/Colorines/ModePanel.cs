using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModePanel : MonoBehaviour
{
    [SerializeField] bool active = false;

    [SerializeField] GameObject modoPanel, tiendaPanel;

    [SerializeField] int desactivationTime;

    public void ActiveValueToFalse()
    {
       // active = false;
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








}
