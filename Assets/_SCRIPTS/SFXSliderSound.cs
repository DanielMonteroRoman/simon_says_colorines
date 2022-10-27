using UnityEngine;

public class SFXSliderSound : MonoBehaviour
{
    AudioSource _asource;
    [SerializeField] AudioClip _clip;
 
    private void Awake()
    {
        _asource = GetComponent<AudioSource>();
    }
    private void OnMouseUp ()
    {
        _asource.PlayOneShot(_clip);
    }
    private void OnMouseDrag()
    {
        _asource.Play();
    }

    private void OnMouseExit()
    {
        _asource.Play();
    }
    private void OnMouseUpAsButton()
    {
        _asource.PlayOneShot(_clip);
    }


}
