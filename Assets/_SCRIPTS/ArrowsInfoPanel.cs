using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsInfoPanel : MonoBehaviour
{
    [SerializeField] List<GameObject> infoPanelList;

    [SerializeField] GameObject rightButt, leftButt;

    [SerializeField] int index;

    private void OnEnable  ()
    {
        index = 0;

        foreach(GameObject go in infoPanelList)
        {
            go.SetActive(false);
        }

        infoPanelList[0].SetActive(true);

       leftButt.GetComponent<UnityEngine.UI.Button>().interactable = false;
       rightButt.GetComponent<UnityEngine.UI.Button>().interactable = true;
        //rightButt.SetActive(true);
        //leftButt.SetActive(false);
    }


    public void RightButton()
    {
        if (index < infoPanelList.Count - 1)
        {
            if (index == 0)
            {
                Debug.Log("APARECE BOTÓN IZQUIERDO");
                leftButt.GetComponent<UnityEngine.UI.Button>().interactable = true;
                //leftButt.SetActive(true);
            }
            index = index + 1;

            infoPanelList[index].SetActive(true);
            infoPanelList[index-1].SetActive(false);

            if (index == infoPanelList.Count - 1)
            {
                //rightButt.SetActive(false);
                rightButt.GetComponent<UnityEngine.UI.Button>().interactable = false;
            }

        }
        
    }
    public void LeftButton()
    {
        if (index > 0)
        {
            if (index == infoPanelList.Count - 1)
            {
                rightButt.GetComponent<UnityEngine.UI.Button>().interactable = true;
               // rightButt.SetActive(true);
            }

            index = index - 1;

            infoPanelList[index].SetActive(true);
            infoPanelList[index + 1].SetActive(false);

            if (index == 0)
            {
                leftButt.GetComponent<UnityEngine.UI.Button>().interactable = false;
                //leftButt.SetActive(false);
            }

        }
    }
}

