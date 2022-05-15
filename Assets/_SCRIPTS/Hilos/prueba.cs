using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]


public class prueba : MonoBehaviour
{
    EdgeCollider2D _edgeColl2D;
    LineRenderer _line;

   [SerializeField] List<GameObject> lista;

   [SerializeField] int numberOfPoints;

   [SerializeField] GameObject point, lineObject;

    [SerializeField] List<LineRenderer> lineList;

    [SerializeField] List<EdgeCollider2D> edgeList;

    List<Vector2> posiciones = new List<Vector2> 
    { 
       /* new Vector3(1.3f,0.13f,0), 
        new Vector3(3f,2f, 0),
        new Vector3(-0.2f,0.8f,0),
        new Vector3(0.5f,2f, 0),
        new Vector3(1.5f,1f, 0),*/

        new Vector2(1.3f,0.13f),
        new Vector2(3f,2f),
        new Vector2(-0.2f,0.8f),
        new Vector2(0.5f,2f),
        new Vector2(1.5f,1f),
    };
    


    void Start()
    {
         //_edgeColl2D = GetComponent<EdgeCollider2D>();

        SpawnPoints(numberOfPoints);
        lista= GenerateList();
        //GenerateLineList(lista);
        GenerateLineList2(lista);
        

       
    }

    // Update is called once per frame
    void Update()
    {
        GenerateFigure(lista);
       // GenerateEdgeColl2d();
    }


    void SpawnPoints(int numberOfElements)
    {
        for (int i=0; i< numberOfElements; i++)
        {
            Instantiate(point, posiciones[i], Quaternion.identity);            
        }        
    }

   List<GameObject> GenerateList()
    {
       foreach(GameObject point in GameObject.FindGameObjectsWithTag("Puntos"))
       {
            lista.Add(point);
       }

        return lista;
    }


    void GenerateLineList2(List<GameObject> lista)
    {
        foreach (GameObject i in lista)
        {
            int n = 0;
            GameObject linea = Instantiate(lineObject);

            lineList.Add(linea.GetComponent<LineRenderer>());
            
            n++;
        }
    }



    void GenerateLineList(List<GameObject> lista)
    {
        foreach (GameObject i in lista)
        {
            int n = 0;
            lineList.Add(i.transform.GetChild(0).GetComponent<LineRenderer>());
            
            n++;
        }
    }



    void GenerateFigure(List<GameObject> list)
    {
        
        for (int i=0; i <=list.Count-1; i++)
        {
            if (i < list.Count-1)
            {   
                lineList[i].useWorldSpace=true;

                lineList[i].SetPosition(0, list[i].transform.position);
                lineList[i].SetPosition(1,list[i+1].transform.position);

                lineList[i].startWidth = 0.02f;                

                               
            }
            else if (i == list.Count-1)
            {          
                lineList[i].useWorldSpace = true;
                
                lineList[i].SetPosition(0, list[i].transform.position);
                lineList[i].SetPosition(1, list[0].transform.position);

                lineList[i].startWidth = 0.02f;                
            }
            
            
        }
    }


    //NO SE ESTÁ USANDO
    void GenerateEdgeColl2d()
    {
        posiciones.Clear();
        foreach(GameObject point in lista)
        {
            posiciones.Add(point.transform.position);            
        }
        posiciones.Add(lista[0].transform.position);
        _edgeColl2D.SetPoints(posiciones);
    }

    //ESTO NO FUNCIONA:
   /* private void OnCollisionStay2D(Collision2D collision)
    {        
        if (collision.gameObject.CompareTag("Colores")) ;
        {
            _line.startColor = Color.red;
            _line.endColor = Color.red;
        }
        
    }*/








}
