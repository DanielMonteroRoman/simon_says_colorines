using System.Collections;
using UnityEngine;

public class ReplayButton : MonoBehaviour
{
   [SerializeField] UnityEngine.UI.Button menuButton;
       

    public void BotonDePlay()
    {
        StartCoroutine(WaitForButtonAnim());
        BlockOtherButtons();
    }


    private IEnumerator WaitForButtonAnim()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        menuButton.interactable = true;

    }

    private void BlockOtherButtons()
    {
        menuButton.interactable = false;
    }
}
