using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableButton : MonoBehaviour
{

    [SerializeField] float time;

    UnityEngine.UI.Button _button;

    private void Awake()
    {
        _button = GetComponent<UnityEngine.UI.Button>();
    }
    public void InteractableChange()
    {
        StartCoroutine(InactiveButton(time));

    }

    IEnumerator InactiveButton (float seconds)
    {
        _button.interactable = false;
        yield return new WaitForSeconds(seconds);
        _button.interactable = true;
    }
}
