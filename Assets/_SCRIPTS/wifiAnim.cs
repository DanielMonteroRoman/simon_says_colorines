using System.Collections;
using UnityEngine;

public class wifiAnim : MonoBehaviour
{
    
    public void WifiAnimation()
    {
        StartCoroutine(WifiAnimationCorrutine());
    }

    public IEnumerator WifiAnimationCorrutine()
    {
        this.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(3f);
        this.gameObject.SetActive(true);
    }
}
