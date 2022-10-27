using UnityEngine;

public class OpenURL : MonoBehaviour
{         
    public void ButtonLinks(string link)
    {
        Application.OpenURL(link);
    }
}
