using UnityEngine;
using System;
using TMPro;

public class rebajasManager : MonoBehaviour
{
   [SerializeField] bool onSale;

    Unlocker unlock;

    TimeSpan diferencia;

    [SerializeField] int price, additionalSeconds;

    MoneyManager _moneyMan;

    UnityEngine.UI.Button salesButt;
    
    [SerializeField] float hora, minutos;
    float msToWait;
    public bool isFinished;

    [SerializeField] string retencionName="retencionRebajas";
    ulong inicioTimer;

    [SerializeField]float segundosRestantes = 0.0f;

    [SerializeField] TMP_Text textTime;

    private void Awake()
    {
        minutos = PlayerPrefs.GetFloat("minutos", 6);

        unlock = GameObject.Find("UNLOCK MANAGER").GetComponent<Unlocker>();

        _moneyMan = GetComponent<MoneyManager>();

        if (PlayerPrefs.HasKey(retencionName))
        {
            long aux = long.Parse(PlayerPrefs.GetString(retencionName)); // si el valor ya es menor que 0, negativo, ha finalizado.
            if (aux < 0) isFinished = true;
            else
            {
                inicioTimer = (ulong)aux;
                ConvertirTimeToMs();
                StartSales();
            }
        }
        else
        {
            isFinished = true;            
        }
    }


    [ContextMenu("iniciar timer")]
    public void IniciarTimer()
    {
        ConvertirTimeToMs();
        inicioTimer = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString(retencionName, inicioTimer.ToString());
        isFinished = false;
    }

    void ConvertirTimeToMs()
    {
        float aux = (hora * 60.0f) + minutos;
        msToWait = aux * 60000.0f;
        Debug.Log(minutos);
    }

    private void Update()
    {
        if (!isFinished)
        {
            segundosRestantes = SaberTotalSegundos();
            if (TimerFinalizado())
            {
                FinishSales();
            }

            string auxTimer = "";

            auxTimer += (((int)segundosRestantes - ((int)segundosRestantes / 3600) * 3600) / 60).ToString("00") + ":";

            auxTimer += (Mathf.CeilToInt(segundosRestantes % 60)).ToString("00");

            textTime.text = auxTimer;
        }
        else
        {
            textTime.text = "00:00";
            PlayerPrefs.SetFloat("minutos", 6);
            minutos = 6;
        }
    }

    float SaberTotalSegundos()
    {
        ulong diff = ((ulong)DateTime.Now.Ticks - inicioTimer);
        ulong aux = diff / TimeSpan.TicksPerMillisecond;

        return (float)(msToWait - aux) / 1000.0f;
    }

    public bool TimerFinalizado()
    {
        if (segundosRestantes < 0)
        {
            isFinished = true;
            PlayerPrefs.SetString(retencionName, "-1");
            return true;
        }
        return false;
    }
    
       

    public void SalesButton()
    {
        if (_moneyMan._availableMoney > price)
        {
            if (!onSale)
            {
                StartSales();

            }
            else
            {
                AddTimeToSale();
            }


            _moneyMan.SustractMoney(price);
            IniciarTimer();
        }
        
    }


    private void StartSales()
    {
        
        unlock.SalesOn();
        onSale = true;
        
    }
    private void AddTimeToSale()
    {
        minutos = (segundosRestantes / 60) + 6;
        IniciarTimer();
        PlayerPrefs.SetFloat("minutos", minutos);
        
        //aumnetar 6  minutos al temporizador
    }

    void FinishSales()
    {
        unlock.SalesOff();
        onSale = false;

    }

    [ContextMenu("resetRebajas")]
    void ResetTime()
    {
        PlayerPrefs.DeleteKey(retencionName);
    }



}
