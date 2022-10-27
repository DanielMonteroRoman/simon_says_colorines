using System.Collections.Generic;
using UnityEngine;


public class SoundSelection : MonoBehaviour
{
    [SerializeField] List<GameObject> soundImages;

    string soundName;
    [SerializeField] int soundIndex;
     
   
   
    public void SelectSoundRight()
    {      

        if(soundIndex < soundImages.Count - 1)
        {
            soundIndex ++;

            soundImages[soundIndex].gameObject.SetActive(true);
            soundImages[soundIndex - 1].gameObject.SetActive(false);
        }
        else if (soundIndex == soundImages.Count - 1)
        {
            soundIndex = 0;

            soundImages[0].gameObject.SetActive(true);
            soundImages[soundImages.Count-1].gameObject.SetActive(false);
        }

    }

    public void SelectSoundLeft()
    {       

        if (soundIndex > 0)
        {
            soundIndex --;
            
            soundImages[soundIndex].gameObject.SetActive(true);
            soundImages[soundIndex+1].gameObject.SetActive(false);

        }
        else if (soundIndex == 0)
        {
            soundIndex = soundImages.Count-1;

            soundImages[soundImages.Count-1].gameObject.SetActive(true);
            soundImages[0].gameObject.SetActive(false);
        }              
        
    }

    
}
