using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeControler : MonoBehaviour
{
    DateTime tiempo;

    private void Start()
    {
        tiempo = DateTime.Now;
    }

    public static void SaveTheDate()
    {
        PlayerPrefs.SetString("FechaCierre", DateTime.Now.ToString());
    }




}
