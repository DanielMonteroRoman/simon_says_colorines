using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeNameTitle : MonoBehaviour
{
    [SerializeField] GameObject tiendaTextGO, modosTextGO, tituloGO, simonStyleGO;

    [SerializeField]  GameObject shopGO, optionsGO;

    bool shopTextOn, optionsTextOn, normalTextOn;

    private void Update()
    {
        if (shopGO.activeInHierarchy && !shopTextOn)
        {
            tiendaTextGO.SetActive(true);
            modosTextGO.SetActive(false);
            tituloGO.SetActive(false);
            simonStyleGO.SetActive(false);
            
            shopTextOn = true;
            optionsTextOn = false;
            normalTextOn = false;
        }

        if (optionsGO.activeInHierarchy && !optionsTextOn)
        {

            tiendaTextGO.SetActive(false);
            modosTextGO.SetActive(true);
            tituloGO.SetActive(false);
            simonStyleGO.SetActive(false);


            shopTextOn = false;
            optionsTextOn = true;
            normalTextOn = false;
        }

        if(!shopGO.activeInHierarchy && !optionsGO.activeInHierarchy && !normalTextOn)
        {

            tiendaTextGO.SetActive(false);
            modosTextGO.SetActive(false);
            tituloGO.SetActive(true);
            simonStyleGO.SetActive(true);

            shopTextOn =false;
            optionsTextOn=false;
            normalTextOn = true;

        }

    }

}
