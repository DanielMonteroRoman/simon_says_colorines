using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleSpeed : MonoBehaviour
{
    Toggle speed;

    GameManager gameManager;

    private void Start()
    {
        speed = GetComponent<Toggle>();
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();

        if (gameManager.gameSpeed == GameManager.GameSpeed.acceleration)
        {
            speed.isOn = true;
        }
        else speed.isOn = false;
    }
}
