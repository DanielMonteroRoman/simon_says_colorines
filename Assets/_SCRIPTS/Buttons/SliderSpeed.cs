using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderSpeed : MonoBehaviour
{

   [SerializeField] TMP_Text _sliderText;

    Slider _slider;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
        _slider = GetComponent<Slider>();
        
    }

    public void OnSliderChanged(float value)       
    {       
        _sliderText.text = value.ToString("F1");

        gameManager.ChangeBetweenColorsTime(value);

    }

    public void RepositionSlider()
    {
        _slider.value = 0.9f;
    }
   
}
