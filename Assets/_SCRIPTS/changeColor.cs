using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class changeColor : MonoBehaviour
{
    TMP_Text reloj;
    private void Awake()
    {
        reloj = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (reloj.text == "00:00" && reloj.color != Color.white) reloj.color = Color.white;
        if (reloj.text != "00:00" && reloj.color == Color.white) reloj.color = new Color(1, 0.85f, 0, 1);
     }
}
