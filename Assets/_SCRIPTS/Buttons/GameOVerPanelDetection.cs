using System.Collections;
using UnityEngine;

public class GameOVerPanelDetection : MonoBehaviour
{
    GameManager gameManager;

    Animator _anim;

    string backToMenu = "BackToMenu";

    [SerializeField] GameObject menuPanel;


    void Awake()
    {
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();

        _anim = GetComponent<Animator>();
    }    

    IEnumerator BackToMenu()
    {
        yield return new WaitForSecondsRealtime(3);
        menuPanel.SetActive(true);
        _anim.SetTrigger(backToMenu);
        StartCoroutine(WaitForEnable());

    }


    IEnumerator WaitForEnable()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        this.gameObject.SetActive(false);
    }


    public void GOPanelOff()
    {
        _anim.SetTrigger(backToMenu);
        StartCoroutine(WaitForEnable());
    }

    public void BackMenuPanel()
    {
        menuPanel.SetActive(true);
        _anim.SetTrigger(backToMenu);
        StartCoroutine(WaitForEnable());
    }
}
