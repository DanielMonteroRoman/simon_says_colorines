using UnityEngine;
using UnityEngine.UI;

public class toggleFlipFlop : MonoBehaviour
{
    Toggle flipFlop;

    GameManager gameManager;
    [SerializeField] GameObject textPanel, accPanel;
    private void Start()
    {
        flipFlop = GetComponent<Toggle>();
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();

        if (gameManager.flipFlop)
        {
            flipFlop.isOn = true;
        }
        else flipFlop.isOn = false;
    }


    public void ActivateOneColorPAnel()
    {
        textPanel.SetActive(flipFlop.isOn);
        accPanel.SetActive(!flipFlop.isOn);
    }
}
