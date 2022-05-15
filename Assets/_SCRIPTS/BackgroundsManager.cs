using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundsManager : MonoBehaviour
{
    [SerializeField] List<GameObject> backgrownds;
    [SerializeField] int index;

    [SerializeField] GameObject optionsPanel;

    public void BackgrowndSelection()
    {
        index = optionsPanel.GetComponent<backgrowndSelection>().index;

        foreach (GameObject go in backgrownds)
        {
            go.SetActive(false);
        }

        backgrownds[index].SetActive(true);
    }
}
