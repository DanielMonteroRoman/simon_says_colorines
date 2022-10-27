using UnityEngine;

public class EleccionDePanel : MonoBehaviour
{
    [SerializeField]GameObject ExtraPanel, notEnoughMoney;
    
    MoneyManager _moneyMan;
    
    private void Awake()
    {
        _moneyMan=GameObject.Find("EXTRAS MANAGER").
            GetComponent<MoneyManager>();   
    }


    public void Choice(int price)
    {
        if (price <= _moneyMan._availableMoney)
        {
            ExtraPanel.SetActive(true);
        }
        else notEnoughMoney.SetActive(true);
    }
}
