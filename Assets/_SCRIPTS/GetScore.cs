using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetScore : MonoBehaviour
{

   [SerializeField] TMP_Text scoreText;
    int score;

    ScoreManager scoreMan;


    private void Awake()
    {
        scoreMan = GameObject.Find("SCORE MANAGER").GetComponent<ScoreManager>();
    }
    private void OnEnable()
    {
        score = scoreMan._score;
        UpdateScoreText();
    }
    
    void UpdateScoreText()
    {
        if (this.gameObject.name == "money")
        {
            scoreText.text = score.ToString()+"$";
        }
        else scoreText.text = score.ToString();
    }

    public void MoneyTimesThreeText()
    {
        if (this.gameObject.name == "money")
        {
            scoreText.text = (score*3).ToString() + "$";
        }
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = 0.ToString();
    }
}
