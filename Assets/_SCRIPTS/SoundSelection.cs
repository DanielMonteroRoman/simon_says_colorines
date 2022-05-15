using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoundSelection : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioList;
    [SerializeField] List<string> soundNames;

    string soundName;
    [SerializeField] int soundIndex;

    public AudioClip selectedSound;

    [SerializeField] TMP_Text name;
    private void Awake()
    {
        
    }
    private void Start()
    {
        LoadIndex();
        selectedSound = audioList[soundIndex];
        soundName = soundNames[soundIndex];
        NameOfSound(soundName);
    }

    public void SelectSoundRight()
    {
        LoadIndex();

        if(soundIndex < audioList.Count - 1)
        {
            soundIndex = soundIndex + 1;
        }
        else if (soundIndex == audioList.Count - 1)
        {
            soundIndex = 0;
        }

       selectedSound =audioList[soundIndex];
        NameOfSound(soundNames[soundIndex]);
        SaveIndex();
    }

    public void SelectSoundLeft()
    {
        LoadIndex();

        if (soundIndex > 0)
        {
            soundIndex = soundIndex - 1;
        }
        else if (soundIndex == 0)
        {
            soundIndex = audioList.Count-1;
        }

        selectedSound = audioList[soundIndex];
        NameOfSound(soundNames[soundIndex]);
        SaveIndex();

    }

    public void NameOfSound(string name)
    {
        this.name.text = name;


    }

    void SaveIndex() //por alguna razón no se salva
    {
        PlayerPrefs.SetInt("index", soundIndex);

    }

    void LoadIndex()
    {
       soundIndex = PlayerPrefs.GetInt("index", 0);
    }
}
