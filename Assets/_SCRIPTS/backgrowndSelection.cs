using System.Collections.Generic;
using UnityEngine;

public class backgrowndSelection : MonoBehaviour
{
    public int index;

    [SerializeField] List<GameObject> backgroundList;

    private void Start()
    {
        LoadIndex();
        foreach (var item in backgroundList)
        {
            item.SetActive(false);
        }
        backgroundList[index].SetActive(true);
    }

    public void SelectBackgroundRight()
    {
        LoadIndex();

        if (index < backgroundList.Count - 1)
        {
            index = index + 1;
            backgroundList[index - 1].SetActive(false);
        }
        else if (index == backgroundList.Count - 1)
        {
            index = 0;
            backgroundList[backgroundList.Count - 1].SetActive(false);
        }

        backgroundList[index].SetActive(true);
        
        SaveIndex();
    }

    public void SelectBackgroundLeft()
    {
        

        if (index > 0)
        {
            index = index-1;
            backgroundList[index + 1].SetActive(false);

        }
        else if (index == 0)
        {
            index = backgroundList.Count - 1;
            backgroundList[0].SetActive(false);
        }

        backgroundList[index].SetActive(true);
        
        SaveIndex();

    }

    void SaveIndex() //por alguna razón no se salva
    {
        PlayerPrefs.SetInt("backgroundIndex", index);

    }

    void LoadIndex()
    {
       
        index = PlayerPrefs.GetInt("backgroundIndex", 0);
    }
}


