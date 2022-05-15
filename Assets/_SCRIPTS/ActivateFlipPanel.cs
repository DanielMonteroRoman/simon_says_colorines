using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFlipPanel : MonoBehaviour
{

    [SerializeField] GameObject candado,blockPanel, activePanel;

   
    void OnEnable()
    {
        if(candado.activeInHierarchy == true)
        {
            blockPanel.SetActive(true);
            activePanel.SetActive(false);
        }
        else if(candado.activeInHierarchy == false)
        {
            blockPanel.SetActive(false);
            activePanel.SetActive(true);
        }
    }
       
}
