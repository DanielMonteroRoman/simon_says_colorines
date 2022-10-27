using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SoundControler : MonoBehaviour
{
    [SerializeField] AudioClip clickSound;

    AudioSource _aSource;

    [SerializeField] Slider musicVolume, SFXVolume;

    [SerializeField] AudioMixer masterMixer;

    [SerializeField] GameObject musicOff, sfxOff;

    [SerializeField] Toggle muteToggle;   


    float initialVolumeValue;
    float initialSFXVolume;

    
    private void Awake()
    {
        _aSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        musicVolume.value = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
        SFXVolume.value = PlayerPrefs.GetFloat("SFXVolume", 0.7f);

        initialVolumeValue = PlayerPrefs.GetFloat("MusicVolume", 0.3f);
        initialSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.7f);

       
        SetMusicVolume(initialVolumeValue);
        SetFXVolume(initialSFXVolume);

    }


    private void Update()
    {
        if (musicVolume.value <= 0.0002) musicOff.SetActive(true);
        else musicOff.SetActive(false);

        if (SFXVolume.value <= 0.0002) sfxOff.SetActive(true);
        else sfxOff.SetActive(false);
        
    }


    public void PlayClickSound()
    {
        _aSource.PlayOneShot(clickSound);
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume.value);
        PlayerPrefs.SetFloat("MasterVolume", SFXVolume.value);
    }

    public void SetFXVolume(float SFXVolume)
    {
        masterMixer.SetFloat("sFXVolume", Mathf.Log10(SFXVolume)*20);              
    } 
    
    public void SetMusicVolume(float musicVolume)
    {
        masterMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume)*20);
    }

    
    public void Mute(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }
    public void MuteToggleToFalse()
    {
        muteToggle.isOn = false;
    }


    public void Reset()
    {
        
    }

}
