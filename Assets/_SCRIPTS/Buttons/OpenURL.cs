using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
   

 
    

    public void ButtonLinks(string link)
    {
        Application.OpenURL(link);
    }
}
