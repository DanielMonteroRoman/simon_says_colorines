using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverInfo : MonoBehaviour
{
    ScoreManager scoreManager;

    [SerializeField] TMP_Text roundText;

    private void Awake()
    {
        scoreManager = GameObject.Find("SCORE MANAGER").GetComponent<ScoreManager>();
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        roundText.text = ((scoreManager.round)-1).ToString();
    }
}
