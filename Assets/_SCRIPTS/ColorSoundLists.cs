using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSoundLists : MonoBehaviour
{

    public List<AudioClip> currentList;

    [SerializeField] List<AudioClip> claxon, pixel, organChords,  flute, organ2;

    [Range(0,4)]
    int selector = 0;

    public float soundVolume=1;


    private void Awake()
    {
        currentList = claxon;
    }
    public void RightButton()
    {
        if (selector < 4) selector++;
        else if (selector == 4) selector = 0;
        
        soundVolume = SetVolume();
        currentList= SoundSelector(selector);
    }

    public void LeftButton()
    {
        if (selector > 0) selector--;
        else if (selector == 0) selector = 4;

        soundVolume = SetVolume();
        currentList = SoundSelector(selector);
        currentList = SoundSelector(selector);
    }

    List<AudioClip> SoundSelector(int selection)
    {
        List<AudioClip> listaLocal= new List<AudioClip>();

        switch (selection)
        {
            case 0: listaLocal = claxon;
                break;

            case 1:
                listaLocal = pixel;
                break;

            case 2:
                listaLocal = organChords;
                break;

            case 3:
                listaLocal = flute;
                break;

            case 4:
                listaLocal = organ2;
                break;

                      
        }
        return listaLocal;
                
    }

    float SetVolume()
    {
        if (selector == 1) return 2.5f; 
        else if (selector == 2) return 1;
        else if (selector == 4 ) return 0.5f;
                
        else return 1;
    }



}
