using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Unlocker : MonoBehaviour
{
    
    public enum Level { Zero, One, Two, Three, Four, Five, Six, Seven, Eight };
    public Level level;

    [SerializeField] GameObject cinco,
        seis, siete, ocho, nueve, invertido, aCiegas, flipFlop,
        block5, block6, block7, block8, block9, blockInvert,
        blockACiegas, blockFlipFlop;

    [SerializeField] int one, two, three, four, five, six, seven, eight;

    int highScore;
    ScoreManager scoreManager;

    [SerializeField] GameObject modeButton;

    [SerializeField] GameObject toggle5, toggle6, toggle7, toggle8, toggle9, toggleInve, toggleOneCol, toggleFlip;

    [SerializeField] public bool modeBut, unbl5, unbl6, unbl7, unbl8, unbl9, unblInv, unbOne, unbFlip;
    
    [SerializeField] TMP_Text level1, level2, level3, level4, level5, level6, level7, level8;

    [SerializeField] TMP_Text nextUnblock;

    [SerializeField] GameObject notification;
    notification notif;

    public bool salesOn;

    private void Awake()
    {
        scoreManager = GameObject.Find("SCORE MANAGER").GetComponent<ScoreManager>();
        notif = notification.GetComponent<notification>();

        LoadLevel();
    }
    
    void Start()
    {        
        VerifyLevel();

        SetTextValues();             

    }

   
    void Update()
    {       
        highScore = scoreManager.highScore;

        if(level==Level.Zero && highScore>= one)
        {
            level = Level.One;
            VerifyLevel();

            unbl5 = true;
            modeBut = true;

           SaveLevel();
            if (salesOn) SalesOn();
        }

        if (level == Level.One && highScore >= two)
        {
            level = Level.Two;
            VerifyLevel();
            
            unblInv = true;
            modeBut = true;          

            SaveLevel();
            if (salesOn) SalesOn();
        }

        if (level == Level.Two && highScore >= three)
        {
            level = Level.Three;
            VerifyLevel();

            unbl6 = true;
            modeBut = true;
            
            SaveLevel();
            if (salesOn) SalesOn();
        }

        if (level == Level.Three && highScore >= four)
        {
            level = Level.Four;
            VerifyLevel();

            unbl7 = true;
            modeBut = true;
                        
            SaveLevel();
            if (salesOn) SalesOn();
        }

        if (level == Level.Four && highScore >= five)
        {
            level = Level.Five;
            VerifyLevel();
            
            unbOne = true;
            modeBut = true;

           SaveLevel();
            if (salesOn) SalesOn();
        }

        if (level == Level.Five && highScore >= six)
        {
            level = Level.Six;
            VerifyLevel();

            unbl8 = true;
            modeBut = true;

            SaveLevel();
            if (salesOn) SalesOn();
        }

        if (level == Level.Six && highScore >= seven)
        {
            level = Level.Seven;
            VerifyLevel();

            unbFlip = true;
            modeBut = true;
            
            SaveLevel();
            if (salesOn) SalesOn();
        }

        if (level == Level.Seven && highScore >= eight)
        {
            level = Level.Eight;
            VerifyLevel();

            unbl9 = true;
            modeBut = true;

            SaveLevel();
            if (salesOn) SalesOn();
        }      
        
        if(level == Level.Eight && highScore >= eight)
        {
            nextUnblock.text = highScore.ToString();
        }
        
    }


    public void VerifyLevel()
    {
        switch (level)
        {
            case Level.Zero:
                blockInvert.SetActive(true);
                invertido.SetActive(false);
                block5.SetActive(true);
                cinco.SetActive(false);
                block6.SetActive(true);
                seis.SetActive(false);
                block7.SetActive(true);
                siete.SetActive(false);
                blockACiegas.SetActive(true);
                aCiegas.SetActive(false);
                ocho.SetActive(false);
                block8.SetActive(true);
                flipFlop.SetActive(false);
                blockFlipFlop.SetActive(true);
                nueve.SetActive(false);
                block9.SetActive(true);

                nextUnblock.text = one.ToString();
                break;

            case Level.One:
                block5.SetActive(false);
                cinco.SetActive(true);

                nextUnblock.text = two.ToString();
                break;

            case Level.Two:
                blockInvert.SetActive(false);
                invertido.SetActive(true);
                block5.SetActive(false);
                cinco.SetActive(true);

                nextUnblock.text = three.ToString();
                break ;

            case Level.Three:
                blockInvert.SetActive(false);
                invertido.SetActive(true);
                block5.SetActive(false);
                cinco.SetActive(true);
                block6.SetActive(false);
                seis.SetActive(true);

                nextUnblock.text = four.ToString();
                break;

            case Level.Four:
                blockInvert.SetActive(false);
                invertido.SetActive(true);
                block5.SetActive(false);
                cinco.SetActive(true);
                block6.SetActive(false);
                seis.SetActive(true);
                block7.SetActive(false);
                siete.SetActive(true);

                nextUnblock.text = five.ToString();
                break;

            case Level.Five:
                blockInvert.SetActive(false);
                invertido.SetActive(true);
                block5.SetActive(false);
                cinco.SetActive(true);
                block6.SetActive(false);
                seis.SetActive(true);
                block7.SetActive(false);
                siete.SetActive(true);
                blockACiegas.SetActive(false);
                aCiegas.SetActive(true);

                nextUnblock.text = six.ToString();
                break;

            case Level.Six:
                blockInvert.SetActive(false);
                invertido.SetActive(true);
                block5.SetActive(false);
                cinco.SetActive(true);
                block6.SetActive(false);
                seis.SetActive(true);
                block7.SetActive(false);
                siete.SetActive(true);
                blockACiegas.SetActive(false);
                aCiegas.SetActive(true);
                ocho.SetActive(true);
                block8.SetActive(false);

                nextUnblock.text = seven.ToString();
                break;

            case Level.Seven:
                blockInvert.SetActive(false);
                invertido.SetActive(true);
                block5.SetActive(false);
                cinco.SetActive(true);
                block6.SetActive(false);
                seis.SetActive(true);
                block7.SetActive(false);
                siete.SetActive(true);
                blockACiegas.SetActive(false);
                aCiegas.SetActive(true);
                ocho.SetActive(true);
                block8.SetActive(false);
                flipFlop.SetActive(true);
                blockFlipFlop.SetActive(false);

                nextUnblock.text = eight.ToString();
                break;

            case Level.Eight:
                blockInvert.SetActive(false);
                invertido.SetActive(true);
                block5.SetActive(false);
                cinco.SetActive(true);
                block6.SetActive(false);
                seis.SetActive(true);
                block7.SetActive(false);
                siete.SetActive(true);
                blockACiegas.SetActive(false);
                aCiegas.SetActive(true);
                ocho.SetActive(true);
                block8.SetActive(false);
                flipFlop.SetActive(true);
                blockFlipFlop.SetActive(false);
                nueve.SetActive(true);
                block9.SetActive(false);
                break;
        }
    }
    public void UpdateHighScore(int highscore)
    {
        highScore = highscore;        
    }

  /*  void UpdateLevel()
    {
        switch (level)
        {
            case Level.Zero:
                level = Level.One;
                break;
            case Level.One:
                level = Level.Two;
                break;
            case Level.Two:
                level = Level.Three;
                break;
            case Level.Three:
                level = Level.Four;
                break;
            case Level.Four:
                level = Level.Five;
                break;
            case Level.Five:
                level = Level.Six;
                break;
            case Level.Six:
                level = Level.Seven;
                break;
            case Level.Seven:
                level = Level.Eight;
                break;
        }

        modeButton.GetComponent<Animator>().SetTrigger("notification");
    }*/

    public void ActivateUnblockAnim()
    {
        toggleInve.GetComponent<unlockAnim>().activator = unblInv;
        toggleOneCol.GetComponent<unlockAnim>().activator = unbOne;
        toggleFlip.GetComponent<unlockAnim>().activator = unbFlip;
        toggle5.GetComponent<unlockAnim>().activator = unbl5;
        toggle6.GetComponent<unlockAnim>().activator = unbl6;
        toggle7.GetComponent<unlockAnim>().activator = unbl7;
        toggle8.GetComponent<unlockAnim>().activator = unbl8;
        toggle9.GetComponent<unlockAnim>().activator = unbl9;

        unbl5 = false;
        unbl6 = false;
        unbl7= false;   
        unbl8= false;
        unbl9 = false;
        unblInv = false;
        unbOne = false;
        unbFlip = false;
        modeBut = false;
    }

    void SaveLevel()
    {
        switch (level)
        {
            case(Level.Zero):
                PlayerPrefs.SetInt("nivel", 0);
                break;
            case (Level.One):
                PlayerPrefs.SetInt("nivel", 1);
                break;
            case (Level.Two):
                PlayerPrefs.SetInt("nivel", 2);
                break;
            case (Level.Three):
                PlayerPrefs.SetInt("nivel", 3);
                break;
            case (Level.Four):
                PlayerPrefs.SetInt("nivel", 4);
                break;
            case (Level.Five):
                PlayerPrefs.SetInt("nivel", 5);
                break;
            case (Level.Six):
                PlayerPrefs.SetInt("nivel", 6);
                break;
            case (Level.Seven):
                PlayerPrefs.SetInt("nivel", 7);
                break;
            case (Level.Eight):
                PlayerPrefs.SetInt("nivel", 8);
                break;
        }        
    }

    void LoadLevel()
    {
        switch (PlayerPrefs.GetInt("nivel"))
        {
            case 0: level = Level.Zero; break;
            case 1: level = Level.One; break;
            case 2: level = Level.Two; break;
            case 3: level = Level.Three; break;
            case 4: level = Level.Four; break;
            case 5: level = Level.Five; break;
            case 6: level = Level.Six; break;
            case 7: level = Level.Seven; break;
            case 8: level = Level.Eight; break;
                
        }   
    }


    public void RestLevel()
    {
        level= Level.Zero;
        PlayerPrefs.SetInt("nivel", 0);

    }

    void SetTextValues()
    {
        level1.text = one.ToString() +"Pt";
        level2.text = two.ToString() + "Pt";
        level3.text = three.ToString() + "Pt";
        level4.text = four.ToString() + "Pt";
        level5.text = five.ToString() + "Pt";
        level6.text = six.ToString() + "Pt";
        level7.text = seven.ToString() + "Pt";
        level8.text = eight.ToString() + "Pt";
       
    } 

    [ContextMenu("Rebajas")]
    public void SalesOn()
    {
        salesOn = true;
        switch (level)
        {
            case (Level.Zero):
                one = Mathf.FloorToInt(one*0.75f);
                nextUnblock.text = one.ToString();
                if (highScore>=one)
                {                   
                    notif.NotificationOn();

                }
                break;
            case (Level.One):
                two = Mathf.FloorToInt(two*0.75f);
                nextUnblock.text = two.ToString();
                if (highScore >= two)
                {                    
                    notif.NotificationOn();
                }
                break;
            case (Level.Two):
                three = Mathf.FloorToInt(three * 0.75f);
                nextUnblock.text = three.ToString();
                if (highScore >= three)
                {                    
                    notif.NotificationOn();
                }
                break;
            case (Level.Three):
                four =  Mathf.FloorToInt(four * 0.75f);
                nextUnblock.text=four.ToString();
                if (highScore >= four)
                {                    
                    notif.NotificationOn();
                }
                break;
            case (Level.Four):
                five =  Mathf.FloorToInt(five * 0.75f);
                nextUnblock.text = five.ToString();
                if (highScore >= five)
                {                    
                    notif.NotificationOn();
                }
                break;
            case (Level.Five):
                six =  Mathf.FloorToInt(six * 0.75f);
                nextUnblock.text = six.ToString();
                if (highScore >= six)
                {                    
                    notif.NotificationOn();
                }
                break;
            case (Level.Six):
                seven =  Mathf.FloorToInt(seven * 0.75f);
                nextUnblock.text = seven.ToString();
                if (highScore >= seven)
                {                    
                    notif.NotificationOn();
                }
                break;
            case (Level.Seven):
                eight =  Mathf.FloorToInt(eight * 0.75f);
                if (highScore >= seven)
                {
                    notif.NotificationOn();
                    
                }

                break;              
        }

        SetTextValues();
    }

    [ContextMenu("RebajasOff")]
    public void SalesOff()
    {
        salesOn = false;
        switch (level)
        {
            case (Level.Zero):
                one = Mathf.CeilToInt(one / 0.75f);
                nextUnblock.text = one.ToString();
                break;
            case (Level.One):
                two = Mathf.CeilToInt(two / 0.75f);
                nextUnblock.text = two.ToString();
                break;
            case (Level.Two):
                three = Mathf.CeilToInt(three / 0.75f);
                nextUnblock.text = three.ToString();
                break;
            case (Level.Three):
                four = Mathf.CeilToInt(four / 0.75f);
                nextUnblock.text = four.ToString();
                break;
            case (Level.Four):
                five = Mathf.CeilToInt(five / 0.75f);
                nextUnblock.text = five.ToString();
                break;
            case (Level.Five):
                six = Mathf.CeilToInt(six / 0.75f);
                nextUnblock.text = six.ToString();
                break;
            case (Level.Six):
                seven = Mathf.CeilToInt(seven / 0.75f);
                nextUnblock.text = seven.ToString();
                break;
            case (Level.Seven):
                eight = Mathf.CeilToInt(eight / 0.75f);
                nextUnblock.text = eight.ToString();
                break;
        }

        SetTextValues();
    }



}
