using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleReverse : MonoBehaviour
{
    Toggle reverse;

    GameManager gameManager;

    private void Start()
    {
        reverse = GetComponent<Toggle>();
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();

        if (gameManager.modes == GameManager.Modes.Reverse)
        {
            reverse.isOn = true;
        }
        else reverse.isOn = false;
    }
}
