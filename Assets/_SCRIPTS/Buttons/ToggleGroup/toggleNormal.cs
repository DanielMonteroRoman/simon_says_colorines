using UnityEngine;
using UnityEngine.UI;

public class toggleNormal : MonoBehaviour
{
    Toggle normal;

    GameManager gameManager;

    private void Start()
    {
        normal = GetComponent<Toggle>();
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();

        if (gameManager.modes == GameManager.Modes.normal)
        {
            normal.isOn = true;
        }
        else normal.isOn=false;        
    }
}
