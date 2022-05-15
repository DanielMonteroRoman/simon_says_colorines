using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    [SerializeField]GameObject obj;

    private void OnEnable()
    {
        ActivateObj();
    }
    void ActivateObj()
    {
        obj.SetActive(true);
    }
}
