using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeNameTitle : MonoBehaviour
{
    [SerializeField] TMP_Text main, secondary;

    [SerializeField]  GameObject shopGO, optionsGO;

    bool shopTextOn, optionsTextOn, normalTextOn;


    private void Update()
    {
        if (shopGO.activeInHierarchy && !shopTextOn)
        {

            ChangeText("tienda", "COLORINES");
            shopTextOn = true;
            optionsTextOn = false;
            normalTextOn = false;
        }

        if (optionsGO.activeInHierarchy && !optionsTextOn)
        {

            ChangeText("opciones", "COLORINES");
            shopTextOn = false;
            optionsTextOn = true;
            normalTextOn = false;
        }

        if(!shopGO.activeInHierarchy && !optionsGO.activeInHierarchy)
        {
            ChangeText("COLORINES", "juego de memoria estilo \"simon\"");
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
