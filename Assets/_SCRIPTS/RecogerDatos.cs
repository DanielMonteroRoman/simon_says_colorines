using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecogerDatos : MonoBehaviour
{

    [SerializeField] bool oneColor;
    [SerializeField] bool reverse;
    [SerializeField] bool flipFlop;

    int numberOfBalls;

   [SerializeField] int uno, dos, tres, cuatro, cinco, seis, siete, ocho; //opciones de juego para guardar el número de partidas.

    GameManager gameManager;

    [SerializeField] TMP_Text text1, text2, text3, text4, text5, text6, text7, text8;


    private void Awake()
    {
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
    }

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        EscribeLosDatos();
    }


    public void RecogidaDeDatos()
    {
        if (!flipFlop)
        {
            if (!oneColor)
            {
                switch (reverse)
                {
                    case false:
                        uno += 1;
                        
                        PlayerPrefs.SetInt("uno", uno);
                     break;

                    case true:
                        dos += 1;
                        {
                            PlayerPrefs.SetInt("dos", dos);
                        }
                        break;
                }
            }
            else if (oneColor)
            {
                switch (reverse)
                {
                    case false:
                        tres += 1;
                        {
                            PlayerPrefs.SetInt("tres", tres);
                        }
                        break;

                    case true:
                        cuatro += 1;
                        {
                            PlayerPrefs.SetInt("cuatro", cuatro);
                        }
                        break;
                }
            }

           
        }
        else if (flipFlop)
        {
            if (!oneColor)
            {
                switch (reverse)
                {
                    case false:
                        cinco += 1;
                        {
                            PlayerPrefs.SetInt("cinco", cinco);
                        }
                        break;

                    case true:
                        seis += 1;
                        {
                            PlayerPrefs.SetInt("seis", seis);
                        }
                        break;
                }
            }
            else if (oneColor)
            {
                switch (reverse)
                {
                    case false:
                        siete += 1;
                        {
                            PlayerPrefs.SetInt("siete", siete);
                        }
                        break;

                    case true:
                        ocho += 1;
                        {
                            PlayerPrefs.SetInt("ocho", ocho);
                        }
                        break;
                }
            }
            
        }        
    }
    public void UpdateOptions()  // PARA EL BOTÓN DE START O DE REJUGAR
    {
        StartCoroutine(WaitToUpdate());

       
    }

    IEnumerator WaitToUpdate()
    {
        yield return new WaitForSeconds(0.1f);
        
        oneColor = gameManager.OneColor;
        numberOfBalls = gameManager.numberOfBalls;
        
        if (gameManager.modes == GameManager.Modes.Reverse)
        {
            reverse = true;
        }
        else if (gameManager.modes == GameManager.Modes.normal)
        {
            reverse = false;
        }

        if (gameManager.flipFlop == true) flipFlop = true;
        else flipFlop = false;
        
    }

    public void EscribeLosDatos()
    {
        text1.text = PlayerPrefs.GetInt("uno").ToString();
        text2.text = PlayerPrefs.GetInt("dos").ToString();
        text3.text = PlayerPrefs.GetInt("tres").ToString();
        text4.text = PlayerPrefs.GetInt("cuatro").ToString();
        text5.text = PlayerPrefs.GetInt("cinco").ToString();
        text6.text = PlayerPrefs.GetInt("seis").ToString();
        text7.text = PlayerPrefs.GetInt("siete").ToString();
        text8.text = PlayerPrefs.GetInt("ocho").ToString();
    }

    public void LoadData()
    {
       uno = PlayerPrefs.GetInt("uno");
       dos = PlayerPrefs.GetInt("dos");
       tres = PlayerPrefs.GetInt("tres");
       cuatro = PlayerPrefs.GetInt("cuatro");
       cinco = PlayerPrefs.GetInt("cinco");
       seis = PlayerPrefs.GetInt("seis");
       siete = PlayerPrefs.GetInt("siete");
       ocho = PlayerPrefs.GetInt("ochoS");
    }
}
