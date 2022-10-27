using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OrdenaElementos : MonoBehaviour
{

    [SerializeField] GameObject color;

    [SerializeField] GameObject[] arrayColors;

    public List<GameObject> colors;

    [SerializeField]
    Vector3
        pos0 = new Vector3(6.16f, 3.15f, 0f),
        pos1 = new Vector3(-5.95f, -0.52f, 0f),
        pos2 = new Vector3(-5.58f, 3.12f, 0f),
        pos3 = new Vector3(6.17f, -0.52f, 0f),
        pos4 = new Vector3(-2.45f, 1.06f, 0f),
        pos5 = new Vector3(2.63f, 0.97f, 0f),
        pos6 = new Vector3(0.03f, -1.49f, 0f),
        pos7 = new Vector3(0.03f, 3.5f, 0f);
        

    GameManager gameManager;
        

    private void Awake()
    {
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
                
    }
    
    private void ColorsPlacement(int number)
    {
        switch (number)
        {
            case 2:
                colors[0].transform.position = pos1;
                colors[1].transform.position = pos3;
                break;
            case 3:
                colors[0].transform.position = pos1;
                colors[1].transform.position = pos3;
                colors[2].transform.position = pos6;
                break;
            case 4:
                colors[0].transform.position = pos0;
                colors[1].transform.position = pos1;
                colors[2].transform.position = pos2;
                colors[3].transform.position = pos3;
                break;
            case 5:
                colors[0].transform.position = pos0;
                colors[1].transform.position = pos1;
                colors[2].transform.position = pos2;
                colors[3].transform.position = pos3;
                colors[4].transform.position = pos6;
                break;
            case 6:
                colors[0].transform.position = pos0;
                colors[1].transform.position = pos1;
                colors[2].transform.position = pos2;
                colors[3].transform.position = pos3;
                colors[4].transform.position = pos4;
                colors[5].transform.position = pos5;
                break;
            case 7:
                colors[0].transform.position = pos0;
                colors[1].transform.position = pos1;
                colors[2].transform.position = pos2;
                colors[3].transform.position = pos3;
                colors[4].transform.position = pos4;
                colors[5].transform.position = pos5;
                colors[6].transform.position = pos6;
                break;
            case 8:
                colors[0].transform.position = pos0;
                colors[1].transform.position = pos1;
                colors[2].transform.position = pos2;
                colors[3].transform.position = pos3;
                colors[4].transform.position = pos4;
                colors[5].transform.position = pos5;
                colors[6].transform.position = pos6;
                colors[7].transform.position = pos7;
                break;
            case 9:
                colors[0].transform.position = new Vector3(6.44f, 2.83f, 0f);
                colors[1].transform.position = new Vector3(-5.99f, -0.34f, 0f);
                colors[2].transform.position = new Vector3(-5.82f, 3.21f, 0f);
                colors[3].transform.position = new Vector3(6.69f, -0.67f, 0f);
                colors[4].transform.position = new Vector3(-2.69f, -1.34f, 0f);
                colors[5].transform.position = new Vector3(3.28f, -1.35f, 0f);
                colors[6].transform.position = new Vector3(-2.24f, 3.38f, 0f);
                colors[7].transform.position = new Vector3(2.76f, 3.44f, 0f);
                colors[8].transform.position = new Vector3(0.32f, 0.83f, 0f);
                break;


        }
            
    }

    void ListConstruction()
    {
        arrayColors = GameObject.FindGameObjectsWithTag("Colores");

        foreach (GameObject i in arrayColors)
        {
            colors.Add(i);
        }              
    }

    public void startButton()
    {
        ListConstruction();
        colors = OrderElementsOfTheList(colors);
        ColorsPlacement(colors.Count);
        gameManager.GeneraEscenario(colors);
        RestoreArrayAndList();
    }

     List<GameObject> OrderElementsOfTheList(List<GameObject> list)
     {
        list = list.OrderBy(gameObject=>gameObject.name).ToList<GameObject>();
       
        return list;       
     }

    void RestoreArrayAndList()
    {
        
        colors.Clear();
        
    }



    


    

   
}



    
    
    



