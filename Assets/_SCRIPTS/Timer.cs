using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]  float hora, minutos;
    float msToWait;
    public bool isFinished;

    [SerializeField] string retencionName="retencion";
    ulong inicioTimer;

    float segundosRestantes = 0.0f;

    [SerializeField]TMP_Text textTime;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(retencionName))
        {
            long aux = long.Parse(PlayerPrefs.GetString(retencionName)); // si el valor ya es menor que 0, negativo, ha finalizado.
            if (aux < 0) isFinished = true;
            else
            {
                inicioTimer =(ulong)aux;
                ConvertirTimeToMs();
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
        msToWait = aux* 60000.0f;
    }

    private void Update()
    {
        if (!isFinished)
        {
            segundosRestantes = SaberTotalSegundos();
            if (TimerFinalizado())
            {
                textTime.text = "00h 00m 00";
            }

            string auxTimer = "";

            //auxTimer += ((int)segundosRestantes / 3600).ToString() + "h ";
            //segundosRestantes -= ((int)segundosRestantes / 3600) * 3600;

            auxTimer += (((int)segundosRestantes - ((int)segundosRestantes / 3600) * 3600) / 60).ToString() + "m ";

            auxTimer += (Mathf.CeilToInt(segundosRestantes % 60)).ToString();

            textTime.text = auxTimer;
        }
        else textTime.text = "00m 00";
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

    [ContextMenu("reset time")]
    void ResetTries()
    {
        PlayerPrefs.DeleteKey(retencionName);
    }

}
