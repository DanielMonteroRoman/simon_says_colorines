using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeNameTitle : MonoBehaviour
{
    [SerializeField] TMP_Text main, secondary;

    [SerializeField]  GameObject shopGO, optionsGO;

    bool shopTextOn, optionsTextOn, normalTextOn;

    [SerializeField] string tittle;


    private void Update()
    {
        if (shopGO.activeInHierarchy && !shopTextOn)
        {

            ChangeText("tienda", tittle);
            shopTextOn = true;
            optionsTextOn = false;
            normalTextOn = false;
        }

        if (optionsGO.activeInHierarchy && !optionsTextOn)
        {

            ChangeText("MODOS DE JUEGO", tittle);
            shopTextOn = false;
            optionsTextOn = true;
            normalTextOn = false;
        }

        if(!shopGO.activeInHierarchy && !optionsGO.activeInHierarchy)
        {
            ChangeText(tittle, "juego de memoria estilo \"simon\"");
            shopTextOn=false;
            optionsTextOn=false;
            normalTextOn = true;

        }

    }

    void ChangeText(string mainText, string secondaryText)
    {
        main.text = mainText;
        secondary.text = secondaryText;
    }
}
