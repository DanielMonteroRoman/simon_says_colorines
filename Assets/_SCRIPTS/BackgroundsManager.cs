using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundsManager : MonoBehaviour
{
    [SerializeField] List<GameObject> backgrownds;
    [SerializeField] int index;

    [SerializeField] GameObject optionsPanel;

    private void Start()
    {
       
        BackgrowndSelection();
    }
    public void BackgrowndSelection()
    {
        index = PlayerPrefs.GetInt("backgroundIndex", 0);

        foreach (GameObject go in backgrownds)
        {
            go.SetActive(false);
        }

        backgrownds[index].SetActive(true);
    }
}
