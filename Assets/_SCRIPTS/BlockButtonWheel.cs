using UnityEngine;
using TMPro;

public class BlockButtonWheel : MonoBehaviour
{
    [SerializeField]UnityEngine.UI.Button wheelButt;
    //[SerializeField] GameObject desactivatedWheel;

    [SerializeField]int allowedTries;
    [SerializeField]int tries;

    [SerializeField] TMP_Text triesText;


    Timer _timerWheel;   


    private void Awake()
    {
        _timerWheel=GetComponent<Timer>();
    }
    private void Start()
    {
        if (_timerWheel.isFinished) tries = 0; SaveTries(tries);
        tries = PlayerPrefs.GetInt("try", 0);
        UpdateText();

        if(tries == allowedTries) wheelButt.interactable = false;
        else wheelButt.interactable = true;
    }
    private void Update()
    {

        if (_timerWheel.isFinished && wheelButt.interactable == false)
        {
            tries = 0;
            SaveTries(tries);
            UpdateText();
            wheelButt.interactable = true;          
           
        }
        if(tries == allowedTries && wheelButt.interactable)
        {
            wheelButt.interactable = false;
        }
        
        


    }


    public void Try()
    {
        tries++;
        SaveTries(tries);
        UpdateText();

       
        if (tries == 1)
        {
             _timerWheel.IniciarTimer();
        }
    }



    void UpdateText()
    {
        triesText.text = tries.ToString() + " / " + allowedTries;
    }

    void SaveTries(int value)
    {
        PlayerPrefs.SetInt("try", value);
    }

    [ContextMenu("reset try")]
    void ResetTries()
    {
        PlayerPrefs.DeleteKey("try");
    }
}
