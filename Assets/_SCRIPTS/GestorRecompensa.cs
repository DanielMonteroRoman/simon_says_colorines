using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GestorRecompensa : MonoBehaviour
{
    [SerializeField]int recompensa;    

    MoneyManager moneyManager;

    [SerializeField] TMP_Text availableMoneyText;

    [SerializeField] float horas, minutos, recompensaInicial;
    [SerializeField] float segundos;
    [SerializeField] float msToWait;
    bool isFinished;

    [SerializeField] string retencionName="retencionName";
    ulong inicioTimer;

    [SerializeField]float segundosRestantes = 0.0f;

    [SerializeField]TMP_Text textTime;

    private void Awake()
    {
        moneyManager = GameObject.Find("EXTRAS MANAGER").
            GetComponent<MoneyManager>();

        if (PlayerPrefs.HasKey(retencionName))
        {
            long aux = long.Parse(PlayerPrefs.GetString(retencionName)); 
            if (aux < 0) isFinished = true;  // si el valor ya es menor que 0, negativo, ha finalizado.
            else
            {
                inicioTimer = (ulong)aux;
                ConvertirTimeToMs();
               
            }
        }
        else
        {
            isFinished = true;
            recompensa = 1000;
        }
    }

    public void Start()
    {
        recompensa = PlayerPrefs.GetInt("recompensaDiaria", 1000);
        segundos = minutos * 60 + horas * 3600;
    }

    [ContextMenu("inicializartimer")]
    public void IniciarTimer()
    {
        ConvertirTimeToMs();
        inicioTimer = (ulong)DateTime.Now.Ticks;
        PlayerPrefs.SetString(retencionName, inicioTimer.ToString());
        isFinished = false;
    }

    void ConvertirTimeToMs()
    {
        float aux = (horas * 60.0f) + minutos;
        msToWait = aux * 60000.0f;
    }

    private void Update()
    {
        
        if (!isFinished)
        {
            recompensa = Mathf.FloorToInt(recompensaInicial * ((segundos - segundosRestantes) / segundos));
            availableMoneyText.text = recompensa.ToString() + " $";

            segundosRestantes = SaberTotalSegundos();
            if (TimerFinalizado())
            {
                recompensa = 1000;
            }

            string auxTimer = "";

            auxTimer += ((int)segundosRestantes / 3600).ToString() + "h ";
           
            auxTimer += (((int)segundosRestantes-((int)segundosRestantes / 3600) * 3600) / 60).ToString() + "m ";

            auxTimer += (Mathf.CeilToInt(segundosRestantes % 60)).ToString() + " ";

            textTime.text = auxTimer;
        }
        else if (isFinished)
        {
            recompensa = 1000;
            textTime.text = "00h 00m 00";
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
        if (segundosRestantes <= 0)
        {
            isFinished = true;
            PlayerPrefs.SetString(retencionName, "-1");
            return true;
        }
        return false;
    }

    //SI NO HAY ANUNCIO
    public void AddRecompensa()
    {
        moneyManager.AddMoney(recompensa);

        IniciarTimer();

    }

    //DESPUÉS DE UN ANUNCIO
    public void AddTripleRecompensa()
    {
        moneyManager.AddMoney(recompensa*3);
        
        IniciarTimer();
    }

    [ContextMenu("reset")]

    void ResetPref()
    {
        PlayerPrefs.DeleteKey(retencionName);
    }

    


}
