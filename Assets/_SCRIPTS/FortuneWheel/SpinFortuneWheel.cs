using System.Collections;
using UnityEngine;
using TMPro;

public class SpinFortuneWheel : MonoBehaviour
{
    [SerializeField] int randVal;
    [SerializeField] float timeInterval;
    [SerializeField] bool isCoroutine;
    [SerializeField] int finalAngle;

    [SerializeField] TMP_Text winText;
    [SerializeField] int section;
    [SerializeField] float totalAngle;
    [SerializeField] string[] prizeName;

    ActivadorPremio activaPremio;

    [SerializeField] GameObject blockPanel;
    [SerializeField] GameObject blockButtonPanel;
    [SerializeField] UnityEngine.UI.Button menuButton;

    RepartePremios repartePremios;

    AudioSource _aSource;
    [SerializeField] AudioClip click;

    private void Awake()
    {
        activaPremio = GetComponent<ActivadorPremio>();
        repartePremios = GameObject.Find("EXTRAS MANAGER").
            GetComponent<RepartePremios>();

        _aSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        isCoroutine = true;
        totalAngle = 360 / section;
    }

    [ContextMenu("Spin")]
    public void StartWheelSpin()
    {
        if(isCoroutine)
        {
            StartCoroutine(Spin());
        }
    }

    
    IEnumerator Spin()
    {
        isCoroutine = false;
        menuButton.interactable = false;
        randVal = Random.Range(200, 300);

        timeInterval = 0.01f * Time.deltaTime * 2;

        for(int i = 0; i < randVal; i++) //To slow down wheel
        {
            Debug.Log("HOLA ROTATOR");
            transform.Rotate(0, 0, (totalAngle / 2)); //start rotate

            if (i > Mathf.RoundToInt(randVal * 0.25f))
                timeInterval = 0.5f * Time.deltaTime;
            if (i > Mathf.RoundToInt(randVal * 0.5f))
                timeInterval = 1f * Time.deltaTime;
            if (i > Mathf.RoundToInt(randVal * 0.7f))
                timeInterval = 1.5f * Time.deltaTime;
            if (i > Mathf.RoundToInt(randVal * 0.8f))
                timeInterval = 2f * Time.deltaTime;
            if (i > Mathf.RoundToInt(randVal * 0.9f))
                timeInterval = 2.5f * Time.deltaTime;
            /*if (i > Mathf.RoundToInt(randVal * 0.95f))
                timeInterval = 3f * Time.deltaTime;*/

            _aSource.PlayOneShot(click,0.5f);

            yield return new WaitForSecondsRealtime(timeInterval);
        }

        if (Mathf.RoundToInt(transform.eulerAngles.z) % totalAngle != 0)
            transform.Rotate(0,0,totalAngle/2); //when indicator stops between 2 numbers, it will add aditional step

        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z); //round off euler angle of wheel value

        for (int i = 0; i< section; i++)
        {
            if (finalAngle == i * totalAngle)
            {
                winText.text = prizeName[i];
                activaPremio.ActivaPremio(AnimationSelector(i)); //selecciona animación
                StartCoroutine(EntregaPremio(i));
            }
                        
        }
        //StartCoroutine(); Iniciar animación
        isCoroutine = true;      

        StartCoroutine(ActivateButtons());
    }

    IEnumerator ActivateButtons()
    {
        yield return new WaitForSecondsRealtime(1);
        blockPanel.SetActive(false);
        blockButtonPanel.SetActive(false);
        menuButton.interactable = true;
    }

    string AnimationSelector(int i)
    {
        switch (i)
        {
            case 0: return "dinero"; break;
            case 1: return "anuncio"; break;
            case 2: return "vida"; break;
            case 3: return "dinero"; break;
            case 4: return "dinero"; break;
            case 5: return "dinero"; break;
            case 6: return "dinero"; break;
            case 7: return "repetir"; break;
            case 8: return "dinero"; break;
            case 9: return "dinero"; break;
            case 10: return "dinero"; break;
            case 11: return "anuncio"; break;
            case 12: return "dinero"; break;
            case 13: return "dinero"; break;
            case 14: return "repetir"; break;
            case 15: return "dinero"; break;
            case 16: return "dinero"; break;
            case 17: return "vida"; break;
            case 18: return "dinero"; break;
            case 19: return "malo"; break;
            case 20: return "dinero"; break;
            case 21: return "dinero"; break;
            case 22: return "dinero"; break;
            case 23: return "repetir"; break;
                
        }
        return null;
        
    }

    IEnumerator EntregaPremio(int i)
    {
        yield return new WaitForSecondsRealtime(3.5f);

        repartePremios.SelectorDePremio(i);

    }


}
