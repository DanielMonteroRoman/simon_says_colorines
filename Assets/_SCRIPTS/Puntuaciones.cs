using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntuaciones : MonoBehaviour
{
    [SerializeField] TMP_Text columns;

    ScoreManager scoreMan;

    private void Awake()
    {
        scoreMan = GameObject.Find("SCORE MANAGER").GetComponent<ScoreManager>();
    }

    private void OnEnable()
    {

        Debug.Log(scoreMan.highScore);
        TextSelection();
    }

    void TextSelection()
    {
        if (scoreMan.highScore < 500)
        {
            columns.text = "                          ¿?";
        }
        
        else if (scoreMan.highScore >= 500)
        {
            columns.text = " Sin Flip-Flop   Con Flip-Flop";
        }
    }

    
}
