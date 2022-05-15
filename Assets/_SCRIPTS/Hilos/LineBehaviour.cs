using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBehaviour : MonoBehaviour
{
    public LineRenderer _line;

    [SerializeField] GameObject[] points;

    [SerializeField] GameObject point;

    [SerializeField] int numberOfPoints;

    List<Vector3> positions6Level1 = new List<Vector3>() 
    {   
        new Vector3(1.4f, 2, 0),
        new Vector3(-1.3f, 2, 0),
        new Vector3(-1.4f, 1, 0),
        new Vector3(-0.5f, 1, 0),
        new Vector3(0.7f, 2.2f, 0),
        new Vector3(1.3f, 1, 0)
    };

    // Start is called before the first frame update
    void Awake()
    {
        _line = GetComponent<LineRenderer>();                
    }

    private void Start()
    {
        CreatePoints(numberOfPoints);
        points = GameObject.FindGameObjectsWithTag("Puntos");
        CreateLines(numberOfPoints);         
    }

    // Update is called once per frame
    void Update()
    {
        DrawFigure();
    }


    /// <summary>
    /// Crea los puntos que luego serán los vértices de las figuras.
    /// </summary>
    /// <param name="numberOfPoints"> nuímero de puntos y de vértices que tendrá la figura.</param>
   void CreatePoints(int numberOfPoints)
   {
        for(int i=1; i<=numberOfPoints; i++)
        {
            Instantiate(point, positions6Level1[i-1], Quaternion.identity); 
        }
   }

    void DrawFigure()
    {
        int i=0;
        foreach (GameObject point in points)
        {
            _line.SetPosition(i, points[i].transform.position);
            i++;
        }
    }


    /// <summary>
    /// Crea los vértices en las posiciones de los puntos creados en CreatePoints y 
    /// </summary>
   void CreateLines(int number) 
   { 
        for(int i= 0; i< number; i++)
        {
            _line.positionCount++;
           // _line.SetPosition(i, points[i].transform.position);
        }
        _line.loop = true;
   }

}
