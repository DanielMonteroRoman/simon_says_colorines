using UnityEngine;

public class ToggleAcceleration : MonoBehaviour
{
    bool desactivation = false;
    public void SpeedDesactivation()
    {
        desactivation = !desactivation;
        UpdateState(desactivation);
    }

    public void UpdateState(bool desactivation)
    {       
        this.gameObject.SetActive(!desactivation);
    }


}
