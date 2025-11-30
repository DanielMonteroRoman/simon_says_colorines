using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System;


public class PlayFabLogIn : MonoBehaviour
{

    [SerializeField] List<GameObject> filasFirstGeneral, filasYouGeneral, filasFirstWeek, filasYouWeek;

    [SerializeField] bool hasConnection;

    [SerializeField] GameObject noWifi;

    [SerializeField] GameObject copas, medallas;

    [SerializeField] GameObject nameWindow;
    public TMP_InputField nameInput;

    [SerializeField] TMP_Text panelCopa, copa, panelMedalla,  goldText, silverText, bronzeText;

    public TMP_Text userNameText;

    ScoreManager scoreManager;

    int scoreInicial;

    int currentVersion;
    
    [Tooltip("CAMBIAR LA VERSIÓN"), SerializeField] string localAppVersion; //IMPORTANTE, HAY QUE CAMBIAR ESTA VERSIÓN CADA ACTUALIZACIÓN
    string updatedAppVersion;
    [SerializeField] TMP_Text appVersionText;

    [SerializeField] GameObject updatePanel;

    int myPos, myWeekPos;

    string myPlayFabID;

    int apiCall;
   


    private void Awake()
    {
       scoreManager = GameObject.Find("SCORE MANAGER").GetComponent<ScoreManager>();
       scoreInicial = scoreManager._bigHScore;

        currentVersion = PlayerPrefs.GetInt("currentVersion", 0);

        myPlayFabID = PlayerPrefs.GetString("ID", "");

        
    }
    public void Start()
    {

        ChangeVersionText();

        goldText.text = "x " + PlayerPrefs.GetInt("gold", 0).ToString();
        silverText.text = "x " + PlayerPrefs.GetInt("silver", 0).ToString();
        bronzeText.text = "x " + PlayerPrefs.GetInt("bronze", 0).ToString();

        LogIn();

        StartCoroutine(CheckInternetConnection());

        userNameText.text = PlayerPrefs.GetString("Name");


    }

    public void LogIn()
    {
        

        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = "42";
        }

#if UNITY_ANDROID
        var androidRequest = new LoginWithAndroidDeviceIDRequest
        {
            AndroidDeviceId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithAndroidDeviceID(androidRequest, OnLoginSuccess, OnLoginFailure);

        apiCall++;
        Debug.Log("Llamadas a PlayFab: " + apiCall);

#elif UNITY_IOS
        var iosrequest = new LoginWithIOSDeviceIDRequest
        {
            DeviceId = SystemInfo.deviceUniqueIdentifier, 
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithIOSDeviceID(iosrequest, OnLoginSuccess, OnLoginFailure);

        apiCall++;
        Debug.Log("Llamadas a PlayFab: " + apiCall);
#else
        var request = new LoginWithCustomIDRequest 
        { 
            CustomId = "GettingStartedGuide", 
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        apiCall++;
        Debug.Log("Llamadas a PlayFab: " + apiCall);
#endif
    }

    private void OnLoginSuccess(LoginResult result)
    {
        if (myPlayFabID == "") GetIDPlayFab();

        if (currentVersion == 0) GetCurrentVersion();
        Debug.Log("Congratulations, you made your first successful API call!");
        noWifi.SetActive(false);

        String name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;

        if (name == null) nameWindow.SetActive(true);
        else
        {
            nameWindow.SetActive(false);
        }
        SendLeaderboard(scoreInicial);

        GetTitleData(); //RECIBE LA VERSIÓN DE LA APLICACIÓN DESDE PLAYFAB
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
        noWifi.SetActive(true);

        updatedAppVersion = localAppVersion;
    }

    //ENVÍO Y REGOGIDA DE DATOS DEL RANKING GENERAL.


    public void SendLeaderboard(int score)
    {
        try
        {
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                new StatisticUpdate
                {
                    StatisticName = "General LeaderBoard Colorines",
                    Value = score
                }
                }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
            
            apiCall++;
            Debug.Log("Llamadas a PlayFab: " + apiCall);
        }
        catch (Exception e)
        {
            noWifi.SetActive(true);
            Debug.Log("Error de conexión: " + e);
            StartCoroutine(CheckInternetConnection());
            LogIn();
            //poner botón de conexión
        };

        if (!hasConnection) noWifi.SetActive(true);
    }

    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Succesfull leaderboard sent");
        noWifi.SetActive(false);
        GetLeaderBoard();
        GetWeekLeaderBoard();
        Invoke("GetLastVersionWeekLeaderBoard", 2);
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Failied leaderboard: " + error);
        CheckInternetConnection();
    }

    public void GetLeaderBoard()
    {
        try
        {
            var request3 = new GetLeaderboardAroundPlayerRequest
            {
                StatisticName = "General LeaderBoard Colorines",
                MaxResultsCount = 1
            };
            PlayFabClientAPI.GetLeaderboardAroundPlayer(request3, OnLeaderboardMyPos, OnError);

            apiCall++;
            Debug.Log("Llamadas a PlayFab: " + apiCall);

            var request = new GetLeaderboardRequest
            {
                StatisticName = "General LeaderBoard Colorines",
                StartPosition = 0,
                MaxResultsCount = 10
            };
            PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);

            apiCall++;
            Debug.Log("Llamadas a PlayFab: " + apiCall);

            var request2 = new GetLeaderboardAroundPlayerRequest
            {
                StatisticName = "General LeaderBoard Colorines",
                MaxResultsCount = 3
            };
            PlayFabClientAPI.GetLeaderboardAroundPlayer(request2, OnLeaderboardAroundGet, OnError);
            apiCall++;
            Debug.Log("Llamadas a PlayFab: " + apiCall);

        }
        catch (Exception e)
        {
            noWifi.SetActive(true);
            Debug.Log("Error de conexión: " + e);
            StartCoroutine(CheckInternetConnection());
            LogIn();
        }
    }

    public void OnLeaderboardMyPos(GetLeaderboardAroundPlayerResult response)
    {
        foreach (var item in response.Leaderboard)
        {
            myPos = item.Position;
            GiveGeneralPrize(myPos, item.DisplayName);
        }

        int i = 0;
    }

    public void OnLeaderboardGet(GetLeaderboardResult result)
    {
        int i = 0;
        foreach (var item in result.Leaderboard)
        {
            try
            {
                filasFirstGeneral[i].transform.GetChild(1).
                GetComponent<TMP_Text>().text = (item.Position + 1).ToString();
                if (item.DisplayName != null)
                {
                    filasFirstGeneral[i].transform.GetChild(2).
                    GetComponent<TMP_Text>().text = item.DisplayName.ToString();
                }
                else if (item.DisplayName == null)
                {
                    filasFirstGeneral[i].transform.GetChild(2).
                    GetComponent<TMP_Text>().text = item.PlayFabId.ToString();
                }
                filasFirstGeneral[i].transform.GetChild(3).
                    GetComponent<TMP_Text>().text = item.StatValue.ToString();

               

                if (item.PlayFabId == myPlayFabID) //(item.DisplayName == PlayerPrefs.GetString("Name") && item.Position == myPos)
                {
                    
                    filasFirstGeneral[i].transform.GetChild(0).gameObject.SetActive(true);

                }
                else filasFirstGeneral[i].transform.GetChild(0).gameObject.SetActive(false); 

                i++;
            }
            catch (Exception e) { }
        }
    }

    public void OnLeaderboardAroundGet(GetLeaderboardAroundPlayerResult response)
    {
        int i = 0;

        foreach (var item in response.Leaderboard)
        {
            try
            {
                filasYouGeneral[i].transform.GetChild(1).
                    GetComponent<TMP_Text>().text = (item.Position + 1).ToString();
                filasYouGeneral[i].transform.GetChild(2).
                    GetComponent<TMP_Text>().text = item.DisplayName.ToString();
                filasYouGeneral[i].transform.GetChild(3).
                    GetComponent<TMP_Text>().text = item.StatValue.ToString();

                if (item.PlayFabId == myPlayFabID)//(item.DisplayName == PlayerPrefs.GetString("Name") && item.Position == myPos)
                {
                    filasYouGeneral[i].transform.GetChild(0).gameObject.SetActive(true);

                }
                else filasYouGeneral[i].transform.GetChild(0).gameObject.SetActive(false);

               

                i++;
            }
            catch (Exception ex) { }
        }
    }



    //ENVÍO Y REGOGIDA DE DATOS DEL RANKING SEMANAL.


    public void SendWeeklyLeaderboard(int score)
    {
        if (hasConnection)
        {
            try
            {
                var request = new UpdatePlayerStatisticsRequest
                {
                    Statistics = new List<StatisticUpdate>
                    {
                         new StatisticUpdate
                         {
                            StatisticName = "Weekly Leaderboard Colorines",
                             Value = score
                         }
                    }
                };
                PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);

                apiCall++;
                Debug.Log("Llamadas a PlayFab: " + apiCall);

            }
            catch (Exception e)
            {
                noWifi.SetActive(true);
                Debug.Log("Error de conexión: " + e);
                StartCoroutine(CheckInternetConnection());
                LogIn();
                //poner botón de conexión
            };
        }
        else noWifi.SetActive(true);
    }


    public void GetWeekLeaderBoard()
    {
        try
        {
            var request3 = new GetLeaderboardAroundPlayerRequest
            {
                StatisticName = "Weekly Leaderboard Colorines",
                MaxResultsCount = 1
            };
            PlayFabClientAPI.GetLeaderboardAroundPlayer(request3, OnLeaderboardWeekMyPos, OnError);

            apiCall++;
            Debug.Log("Llamadas a PlayFab: " + apiCall);

            var request = new GetLeaderboardRequest
            {
                StatisticName = "Weekly Leaderboard Colorines",
                StartPosition = 0,
                MaxResultsCount = 10,

            };
            PlayFabClientAPI.GetLeaderboard(request, OnWeekLeaderboardGet, OnError);

            apiCall++;
            Debug.Log("Llamadas a PlayFab: " + apiCall);

            var request2 = new GetLeaderboardAroundPlayerRequest
            {
                StatisticName = "Weekly Leaderboard Colorines",
                MaxResultsCount = 3
            };
            PlayFabClientAPI.GetLeaderboardAroundPlayer(request2, OnWeekLeaderboardAroundGet, OnError);

            apiCall++;
            Debug.Log("Llamadas a PlayFab: " + apiCall);

        }
        catch (Exception e)
        {
            noWifi.SetActive(true);
            Debug.Log("Error de conexión: " + e);
            StartCoroutine(CheckInternetConnection());
            LogIn();
        }
    }

    public void OnLeaderboardWeekMyPos(GetLeaderboardAroundPlayerResult response)
    {
        int a = new int();

        foreach (var item in response.Leaderboard)
        {
            myWeekPos = item.Position;

            
            a++;
            

        }
    }

    public void OnWeekLeaderboardGet(GetLeaderboardResult result)
    {
        if (currentVersion < result.Version)
        {
            Debug.Log("SÍ VOY A ACTUALIZAR LAS MEDALLAS PORQUE CURRENT VERSION: " + currentVersion
                + "Y RESULT VERSION: " + result.Version);

            currentVersion = result.Version;
            PlayerPrefs.SetInt("currentVersion", currentVersion);

            UpdateNumberOfMedals();

            scoreManager.ResetWeeklyValues();


        }
        else 
        {
            Debug.Log("NO ESTOY ACTUALIZANDO MEDALLAS PORQUE CURRENT VERSION: " + currentVersion 
                +"Y RESULT VERSION: " + result.Version);
            

        } 




        int i = 0;
        foreach (var item in result.Leaderboard)
        {
            try
            {
                filasFirstWeek[i].transform.GetChild(1).
                GetComponent<TMP_Text>().text = (item.Position + 1).ToString();
                filasFirstWeek[i].transform.GetChild(2).
                    GetComponent<TMP_Text>().text = item.DisplayName.ToString();
                filasFirstWeek[i].transform.GetChild(3).
                    GetComponent<TMP_Text>().text = item.StatValue.ToString();

               

                if (item.PlayFabId == myPlayFabID) 
                {
                   

                    filasFirstWeek[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                else filasFirstWeek[i].transform.GetChild(0).gameObject.SetActive(false); 


                i++;
            }
            catch (Exception e) { }

        }
    }

    public void OnWeekLeaderboardAroundGet(GetLeaderboardAroundPlayerResult response)
    {
        int i = 0;
        foreach (var item in response.Leaderboard)
        {
            try
            {
                filasYouWeek[i].transform.GetChild(1).
                GetComponent<TMP_Text>().text = (item.Position + 1).ToString();
                filasYouWeek[i].transform.GetChild(2).
                    GetComponent<TMP_Text>().text = item.DisplayName.ToString();
                filasYouWeek[i].transform.GetChild(3).
                    GetComponent<TMP_Text>().text = item.StatValue.ToString();

                if (item.PlayFabId == myPlayFabID) 
                {
                    filasYouWeek[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                else filasYouWeek[i].transform.GetChild(0).gameObject.SetActive(false);

                i++;
            }
            catch (Exception ex) { }
        }
    }

    //PREMIOS

    void GiveGeneralPrize(int prize, string userName)
    {
        if (prize == 0)
        {
            copas.SetActive(true);
            copas.transform.GetChild(0).transform.GetChild(1).
                transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            copas.transform.GetChild(0).transform.GetChild(1).
                transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            copas.transform.GetChild(0).transform.GetChild(1).
                transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (prize == 1)
        {
            copas.SetActive(true);
            copas.transform.GetChild(0).transform.GetChild(1).
                transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            copas.transform.GetChild(0).transform.GetChild(1).
                transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            copas.transform.GetChild(0).transform.GetChild(1).
                transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        }
        else if (prize == 2)
        {
            copas.SetActive(true);
            copas.transform.GetChild(0).transform.GetChild(1).
                transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            copas.transform.GetChild(0).transform.GetChild(1).
                transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
            copas.transform.GetChild(0).transform.GetChild(1).
                transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
        }
        else copas.SetActive(false);

        panelCopa.text = userName;
        panelMedalla.text = userName;
        copa.text = userName;

       
    }


    // COMPROBAR SI HAY CONEXIÓN A INTERNET

    IEnumerator CheckInternetConnection()
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");

        yield return request.SendWebRequest();

        if (request.error != null)
        {
            hasConnection = false;
        }
        else
        {
            hasConnection = true;
        }
    }


    /// <summary>
    /// Añade el nombre de usuario
    /// </summary>
    public void SubmitButton()
    {
        string nombreUsuarioTemporal;

        if (nameInput.text == null || nameInput.text == "")
        {
            nombreUsuarioTemporal = "NoName" + UnityEngine.Random.Range(0, 999999);  //HE AÑADIDO QUE SI NO HAY NOMBRE SE AUTOCOMPLETE
        }
        else
        {
            nombreUsuarioTemporal = nameInput.text;
        }
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nombreUsuarioTemporal
            // DisplayName = nameInput.text,

        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);

        apiCall++;
        Debug.Log("Llamadas a PlayFab: " + apiCall);

        PlayerPrefs.SetString("Name", nombreUsuarioTemporal);

        GetLeaderBoard();
        GetLastVersionWeekLeaderBoard();
        GetWeekLeaderBoard();


    }


    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        nameWindow.SetActive(false);

        userNameText.text = PlayerPrefs.GetString("Name");
        panelCopa.text = PlayerPrefs.GetString("Name");
        panelMedalla.text = PlayerPrefs.GetString("Name");
        copa.text = PlayerPrefs.GetString("Name");
    }


    public void GetLastVersionWeekLeaderBoard()
    {
        try
        {
            var request = new GetLeaderboardAroundPlayerRequest
            {
                StatisticName = "Weekly Leaderboard Colorines",
                MaxResultsCount = 1,
                Version = currentVersion - 1
            };
            PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnWeekLastVersioneaderboardGetAround, OnError);
            
           
            apiCall++;
            Debug.Log("Llamadas a PlayFab: " + apiCall);

        }
        catch (Exception e)
        {
            noWifi.SetActive(true);
            Debug.Log("Error de conexión: " + e);
            StartCoroutine(CheckInternetConnection());
            LogIn();
        }

        
    }

    void OnWeekLastVersioneaderboardGetAround(GetLeaderboardAroundPlayerResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            Debug.Log("SEGÚN EL LEADERBOARD GET AROUND MI POSICIÓN FUE: "+item.Position + "...id:" 
                + item.PlayFabId);

            if (item.Position == 0 && item.StatValue > 0)
            {
                medallas.SetActive(true);
                medallas.transform.GetChild(0).transform.GetChild(1).
                    transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                medallas.transform.GetChild(0).transform.GetChild(1).
                    transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                medallas.transform.GetChild(0).transform.GetChild(1).
                    transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);

            }
            else if (item.Position == 1 && item.StatValue > 0)
            {
                medallas.SetActive(true);
                medallas.transform.GetChild(0).transform.GetChild(1).
                    transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                medallas.transform.GetChild(0).transform.GetChild(1).
                    transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                medallas.transform.GetChild(0).transform.GetChild(1).
                    transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
            }
            else if (item.Position == 2 && item.StatValue > 0)
            {
                medallas.SetActive(true);
                medallas.transform.GetChild(0).transform.GetChild(1).
                    transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                medallas.transform.GetChild(0).transform.GetChild(1).
                    transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
                medallas.transform.GetChild(0).transform.GetChild(1).
                    transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                medallas.SetActive(false);
            }
        }
    }

    public void ResetCurrent()
    {
        currentVersion = 0;
        PlayerPrefs.DeleteKey("currentVersion");
    }


    void GetCurrentVersion()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "Weekly Leaderboard Colorines",
            MaxResultsCount = 1,
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, WeekGiveMeVersion, OnError);

        apiCall++;
        Debug.Log("Llamadas a PlayFab: " + apiCall);
    }

    void WeekGiveMeVersion(GetLeaderboardAroundPlayerResult result)
    {
        currentVersion = result.Version;
        PlayerPrefs.SetInt("currentVersion", currentVersion);
    }


    void UpdateNumberOfMedals()
    {
        try
        {
            var request = new GetLeaderboardAroundPlayerRequest
            {
                StatisticName = "Weekly Leaderboard Colorines",
                MaxResultsCount = 1,
                Version = currentVersion - 1
            };
            PlayFabClientAPI.GetLeaderboardAroundPlayer(request, GiveMeMedals, OnErrorMedals);
            
            apiCall++;
            Debug.Log("Llamadas a PlayFab: " + apiCall);
        }
        catch (Exception e) { }
    }

    void OnErrorMedals(PlayFabError error)
    {

        Debug.Log(currentVersion);

        currentVersion--;
        PlayerPrefs.SetInt("currentVersion", currentVersion);

        Debug.Log("ESTOY ENTRANDO EN ERROR MEDALS");
        Debug.Log(currentVersion);
       
    }

    void GiveMeMedals(GetLeaderboardAroundPlayerResult result)
    {
        Debug.Log(currentVersion);

        foreach (var item in result.Leaderboard)
        {
            if (item.Position == 0) PlayerPrefs.SetInt("gold", PlayerPrefs.GetInt("gold", 0) + 1); //suma gold
            else if (item.Position == 1) PlayerPrefs.SetInt("silver", PlayerPrefs.GetInt("silver", 0) + 1); //suma silver
            else if (item.Position == 2) PlayerPrefs.SetInt("bronze", PlayerPrefs.GetInt("bronze", 0) + 1); //suma bronce
        }

        goldText.text = "x " + PlayerPrefs.GetInt("gold", 0).ToString();
        silverText.text = "x " + PlayerPrefs.GetInt("silver", 0).ToString();
        bronzeText.text = "x " + PlayerPrefs.GetInt("bronze", 0).ToString();

        Debug.Log("ESTOY ENTRANDO EN DAME MEDALS");
        Debug.Log(currentVersion);
    }


    //VERSIÓN DE LA APLICACIÓN Y POP UP DE "ACTUALIZAR"
    void CompareAppVersions()
    {
        if (localAppVersion != updatedAppVersion)
        {
            updatePanel.SetActive(true);
        }
        else updatePanel.SetActive(false);
        
    }

    public void GetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), OnTitleDataRecieved, OnError);
        apiCall++;
        Debug.Log("Llamadas a PlayFab: " + apiCall);
    }

    void OnTitleDataRecieved(GetTitleDataResult result)
    {
        if (result.Data == null || !result.Data.ContainsKey("updateVersion"))
        {
            updatedAppVersion = localAppVersion;

            Debug.Log("Null data updateVersion");
        }

        else updatedAppVersion = result.Data["updateVersion"];

        CompareAppVersions(); //COMPARA LA VERSIÓN DISPONIBLE CON LA ACTUAL.
    }

    public void ResetMedals()
    {

        PlayerPrefs.DeleteKey("gold");
        PlayerPrefs.DeleteKey("silver");
        PlayerPrefs.DeleteKey("bronze");


        goldText.text = "x " + PlayerPrefs.GetInt("gold", 0).ToString();
        silverText.text = "x " + PlayerPrefs.GetInt("silver", 0).ToString();
        bronzeText.text = "x " + PlayerPrefs.GetInt("bronze", 0).ToString();


    }


    void GetIDPlayFab()
    {
        
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "Weekly Leaderboard Colorines",
            MaxResultsCount = 1,

        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, GiveMeID, OnErrorMedals);
        
        apiCall++;
        Debug.Log("Llamadas a PlayFab: " + apiCall);
    }


    void GiveMeID(GetLeaderboardAroundPlayerResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            myPlayFabID = item.PlayFabId;
        }
    }


    private void ChangeVersionText()
    {
        appVersionText.text = localAppVersion.ToString();
    }
}