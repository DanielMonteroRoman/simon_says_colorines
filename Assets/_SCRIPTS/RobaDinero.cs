using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobaDinero : MonoBehaviour
{
    [SerializeField] GameObject originGO, targetGO;

   [SerializeField] Vector3 initialPos, target, currentPos;

    [SerializeField]bool activator;

    RectTransform _rTransf;

    [SerializeField] float speed;


    private void Awake()
    {
        _rTransf = GetComponent<RectTransform>();
        initialPos = originGO.GetComponent<RectTransform>().position;
        _rTransf.position = initialPos;
        target = targetGO.GetComponent<RectTransform>().position;
        
    }
    private void OnEnable()
    {
        _rTransf.position = initialPos;
        StartCoroutine(Motion());

        
    }

    private void Update()
    {
        currentPos = _rTransf.position;
        
        if (activator)
        {
            _rTransf.position = Vector3.MoveTowards(_rTransf.position,
                target, speed * Time.deltaTime);

        }
    }

    IEnumerator Motion()
    {
        yield return new WaitForSecondsRealtime(1f);

        activator = true; Debug.Log("HOLA CORRUTINA");

        yield return new WaitForSecondsRealtime(2f);

        activator = false;

    }
}
