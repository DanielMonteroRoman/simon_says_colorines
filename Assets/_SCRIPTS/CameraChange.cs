using UnityEngine;

public class CameraChange : MonoBehaviour
{

    [SerializeField] public GameObject mainCam, UICam;

    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(gameManager.gameState == GameManager.GameState.Playing)           
        {
            UICam.gameObject.SetActive(false);
            mainCam.gameObject.SetActive(true);

        }
        else
        {
            UICam.gameObject.SetActive(true);
            mainCam.gameObject.SetActive(false);
        }
    }
}
