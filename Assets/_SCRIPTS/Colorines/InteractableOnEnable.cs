using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableOnEnable : MonoBehaviour
{

    ScoreManager scoreManager;

    UnityEngine.UI.Button _button;

    private void Awake()
    {
        scoreManager = GameObject.Find("SCORE MANAGER").GetComponent<ScoreManager>();
        _button = GetComponent<UnityEngine.UI.Button>();
    }
    private void OnEnable()
    {
        _button.interactable = true;
    }

    private void Update()
    {
        if (scoreManager._score == 0 & _button.interactable == true) _button.interactable = false;
    }
}
