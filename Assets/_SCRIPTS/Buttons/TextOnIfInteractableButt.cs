using UnityEngine;

public class TextOnIfInteractableButt : MonoBehaviour
{
    [SerializeField] GameObject text;

    UnityEngine.UI.Button boton;

    private void Awake()
    {
        boton = this.GetComponent<UnityEngine.UI.Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boton.interactable==true && !text.activeInHierarchy)
        {
            text.SetActive(true);
        }

        if (boton.interactable == false && text.activeInHierarchy)
        {
            text.SetActive(false);
        }

    }
}
