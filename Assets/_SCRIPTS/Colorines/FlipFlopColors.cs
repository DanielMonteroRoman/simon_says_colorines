using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipFlopColors : MonoBehaviour
{
    public GameObject GO1, GO2;

    public bool change;

    void Update()
    {
        if (change)
        {
            Debug.Log("Has apretado");
            ChangePositions(GO1, GO2);
        }
    }

    public void ChangePositions(GameObject a, GameObject b)
    {
        Debug.Log("Cambiando posiciones");

        Vector3 aPos = a.transform.position;
        Vector3 bPos = b.transform.position;

        a.transform.position = bPos;
        b.transform.position = aPos;

        change = false;

    }

}
