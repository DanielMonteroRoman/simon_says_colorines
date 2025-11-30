using UnityEngine;


public class BlockRebajasButton : MonoBehaviour
{
    UnityEngine.UI.Button button;
    Unlocker unlocker;

    private void Awake()
    {
        unlocker = GameObject.Find("UNLOCK MANAGER").GetComponent<Unlocker>();
        button = GetComponent<UnityEngine.UI.Button>();
    }
    private void Update()
    {
       if(unlocker.level==Unlocker.Level.Eight && button.interactable==true)
            button.interactable = false;
    }
}
