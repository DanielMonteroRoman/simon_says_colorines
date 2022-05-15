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
        scoreText.text = score.ToString()+" $";
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = 0.ToString();
    }
}
