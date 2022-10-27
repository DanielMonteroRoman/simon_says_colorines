using UnityEngine;

public class notification : MonoBehaviour
{
    Animator _anim;

    string notif = "notification";

    [SerializeField] bool noti;

    Unlocker unlocker;

    [SerializeField] GameObject confeti;

    private void Awake()
    {
        _anim = GetComponent<Animator>();

        unlocker = GameObject.Find("UNLOCK MANAGER").GetComponent<Unlocker>();
               
    }

    private void OnEnable()
    {
        noti = unlocker.modeBut;

    }

    private void Update()
    {
        if(noti == true)
        {
            NotificationOn();
        }        
    }

    public void NotificationOn()
    {
        _anim.SetBool(notif, true);

        noti = false;

        confeti.SetActive(true);
    }

    public void NotificationOff()
    {
        _anim.SetBool(notif,false);
        confeti.SetActive(false);
    }
}
