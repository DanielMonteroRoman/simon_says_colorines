using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;

    [SerializeField] int numberOfGames;
    [SerializeField] int numberOfGamesAllowed;

    float fixedDeltaTime;

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;

        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // Show the loaded content in the Ad Unit:
        
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }



    public void AddNumberOfGames() // aumenta en uno el número de juegos
    {
        numberOfGames++;
    }
    public void ShowAdIfGamesEnded() // para gameoverpanel, enseña anuncio si se han acabado los intentos
    {
        if (numberOfGames >= numberOfGamesAllowed)
        {
            ShowAd();
            numberOfGames = 0;
        }
    }

    public void AdButton() //si se ha visto un anuncio voluntario resetea el conteo
    {
        numberOfGames = 0;
    }

    public void OnUnityAdsShowStart(string adUnitId) 
    {
        Time.timeScale = 0.0f;
        Debug.Log("tiempo congelado");

    }
    public void OnUnityAdsShowClick(string adUnitId) 
    {
        Time.timeScale = 1.0f;
        Debug.Log("tiempo DESCONGELADO");
        LoadAd();
    }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) 
    {
        Time.timeScale = 1.0f;
        Debug.Log("tiempo DESCONGELADO");
        LoadAd();
    }
}
