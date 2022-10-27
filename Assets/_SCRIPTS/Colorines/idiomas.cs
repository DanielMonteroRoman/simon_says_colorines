using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;



public class idiomas : MonoBehaviour
{
     int index;

    List<Locale> locales;

   
    public void RightButton()
    {            
        if(index < 3)
        {                   
            index += 1;
            
        }
        else if (index == 3)
        {                   
           index = 0;                             
        }

      LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

       Debug.Log(index);
    }


    public void LeftButton()
    {          
       if (index > 0)
       {
            index -= 1;    
       }
       else if (index == 0)
       {
            index = 3;
       }

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        
        Debug.Log(index);
    }

      
}
