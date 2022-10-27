using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] TMP_Text recordText, modoText, roundText;

    
   public int round;

    private void Awake()
    {
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();

    }

    private void Update()
    {
        round = gameManager.round;
        UpdateRoundText();

       
    }

    void UpdateRoundText()
    {
       roundText.text = round.ToString();
    }

    

}
