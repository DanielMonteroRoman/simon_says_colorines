using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggleOneColor : MonoBehaviour
{
    Toggle oneColor;

    GameManager gameManager;
    [SerializeField] GameObject textPanel, accPanel;

    
    private void Start()
    {
        oneColor = GetComponent<Toggle>();
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();

        if (gameManager.OneColor)
        {
            oneColor.isOn = true;
        }
        else oneColor.isOn = false;
    }


    public void ActivateOneColorPAnel()
    {
        textPanel.SetActive(oneColor.isOn);
        accPanel.SetActive(!oneColor.isOn);
    }
}
