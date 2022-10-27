using System.Collections.Generic;
using UnityEngine;

public class HilosManager : MonoBehaviour
{

    [SerializeField] LineRenderer _line;

    [SerializeField] List<Vector2> lineList;

    //[SerializeField] Vector3[] nodePositions;

    [SerializeField] EdgeCollider2D _edge2D;

    [SerializeField] PolygonCollider2D _polColl2D;

    private void Start()
    {
        _line = GetComponent<LineRenderer>();
       // _edge2D = GetComponent<EdgeCollider2D>();
        _polColl2D = GetComponent<PolygonCollider2D>();
               
    }


    private void Update()
    {

        GeneratePointsFromLRenderer();
       // CreateEdge2D();
        CreatePolygonColl2D();
        DeletePointsValues();


    }

   
    void GeneratePointsFromLRenderer()
    {
        Vector3[] nodePositions = new Vector3[2];

        _line.GetPositions(nodePositions);

        foreach (Vector3 node in nodePositions)
        {
            lineList.Add(node);
        }

        
    }

    private void CreatePolygonColl2D()
    {
        lineList.Add(new Vector2((lineList[0].x)+ 0.01f, lineList[0].y));
        _polColl2D.SetPath(0, lineList);
    }


    void CreateEdge2D()
    {
        _edge2D.SetPoints(lineList);
    }

    void DeletePointsValues()
    {
        lineList.Clear();
    }


    //_________________________________DETECCIONES_____________________________//

    private void OnTriggerStay2D (Collider2D collision)
    {
        Debug.Log("STAY TRIGGER" + collision.tag);
        if (collision.gameObject.CompareTag("Colores"))
        {
            _line.startColor = Color.red;
            _line.endColor = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("EXIT TRIGGER" + collision.tag);
        if (collision.gameObject.CompareTag("Colores"))
        {
            _line.startColor = Color.green;
            _line.endColor = Color.green;
        }
    }

   /* private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("STAY COLISION");
        if (collision.gameObject.CompareTag("Colores"))
        {
            _line.startColor = Color.red;
            _line.endColor = Color.red;
        }
    }

    private void OnCollisionExit2D (Collision2D collision)
    {
        Debug.Log("EXIT COLISION");
        if (collision.gameObject.CompareTag("Colores"))
        {
            _line.startColor = Color.green;
            _line.endColor = Color.green;
        }
    }*/

}
