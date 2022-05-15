using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    public int highScore;
        
    private void Awake()
    {
        highScore = LoadDataMethod();
    }


    public void SaveData(int highscore)
    {
        PlayerPrefs.SetInt("saveRecord", highscore);
    }

    int LoadDataMethod()
    {
        highScore = PlayerPrefs.GetInt("saveRecord", 0);

        return highScore;
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteKey("saveRecord");
        highScore = 0;
        SaveData(0);             
        
    }

}
