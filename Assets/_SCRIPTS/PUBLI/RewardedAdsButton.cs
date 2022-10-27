using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] UnityEngine.UI.Button _showAdButton;
    [SerializeField] UnityEngine.UI.Button _showAdButton2;
    [SerializeField] UnityEngine.UI.Button _showAdButton3;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms


    [SerializeField,Range(0, 2)] public int reward;

    [Header("COSAS DE LAS RECOMPENSA 0")]

    [SerializeField] GameObject imagenMovilDineroTienda;
    [SerializeField] GameObject NumeroDeColoresPanel;

    [Header("COSAS DE LAS RECOMPENSA 1")]

    [SerializeField] GameObject spiningObject;
    [SerializeField] GameObject panelBloqueoRuleta;
    [SerializeField] GameObject panelBloqueoBotonRuleta;
    [SerializeField] GameObject velocidadPanel;

    [Header("COSAS DE LAS RECOMPENSA 2")]

    [SerializeField] GameObject imagenMovilDineroGOver;
    [SerializeField] GameObject money;
    [SerializeField] GameObject timesThreeButton;
    [SerializeField] GameObject continueButton;

    int orderNumber=0;

   InterstitialAdsButton _interstAds;
    void Awake()
    {
        _interstAds = GetComponent<InterstitialAdsButton>();
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        //Disable the button until the ad is ready to show:
        _showAdButton.interactable = false;
       _showAdButton2.interactable = false;
       
        _showAdButton3.interactable = false;
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        
        orderNumber++;

        Advertisement.Load(_adUnitId, this);
       
            
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId + "DANI" + orderNumber);
        orderNumber++;

        if (adUnitId.Equals(_adUnitId))
        {
            
            // Configure the button to call the ShowAd() method when clicked:
            _showAdButton.onClick.AddListener(ShowAd);
            _showAdButton2.onClick.AddListener(ShowAd);
            _showAdButton3.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            _showAdButton.interactable = true;
            _showAdButton2.interactable = true;

           //_showAdButton3.interactable = true; //lo quito porque lo activa cuando debe estar off

           
            
        }
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Disable the button:
        _showAdButton.interactable = false;
        _showAdButton2.interactable = false;
        _showAdButton3.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);

    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed" + ".DANI" + orderNumber + "The reward: " +reward);
            orderNumber++;

            // Grant a reward.

            //Debug.Log("DANI: Has ganasdo una vida" + "DANI" + orderNumber);
            //orderNumber++;

            switch (reward)
            {
                case 0:
                    NumeroDeColoresPanel.GetComponent<GestorRecompensa>().AddTripleRecompensa();
                    imagenMovilDineroTienda.SetActive(true);
                        break;

                case 1:
                    spiningObject.GetComponent<SpinFortuneWheel>().StartWheelSpin();
                    panelBloqueoRuleta.SetActive(true);
                    panelBloqueoBotonRuleta.SetActive(true);
                    velocidadPanel.GetComponent<BlockButtonWheel>().Try();
                    break;  

                case 2: 
                    imagenMovilDineroGOver.SetActive(true);
                    timesThreeButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
                    money.GetComponent<GetScore>().MoneyTimesThreeText();
                    timesThreeButton.GetComponent<AddMoneyTimesThree>().AddThreeTimes();
                    continueButton.GetComponent<UnityEngine.UI.Button>().interactable=false;
                    break;
            }

            _showAdButton.onClick.RemoveAllListeners();
            _showAdButton2.onClick.RemoveAllListeners();
            _showAdButton3.onClick.RemoveAllListeners();
            // Load another ad:
            Advertisement.Load(_adUnitId, this);

            _interstAds.AdButton();
        }
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // Clean up the button listeners:
        _showAdButton.onClick.RemoveAllListeners();
        _showAdButton2.onClick.RemoveAllListeners();
        _showAdButton3.onClick.RemoveAllListeners();
    }


    public void RewardChoice(int number)
    {
        reward = number;
       
    }
}
