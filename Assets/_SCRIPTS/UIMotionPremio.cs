using System.Collections;
using UnityEngine;

public class UIMotionPremio : MonoBehaviour
{
    RectTransform _rTransf;

    [SerializeField] GameObject targetGO;

    [SerializeField] Vector3 initialPos, target, currentPos;

    [SerializeField] float speed;

    [SerializeField] bool activator;

    [SerializeField] float before = 3f, after = 1f;


    private void Awake()
    {
        _rTransf=GetComponent<RectTransform>();        
        initialPos =_rTransf.localPosition;  //posición inicial del objeto
        target = (targetGO.GetComponent<RectTransform>().position);
            
    }

    private void OnEnable()
    {
        transform.localPosition = initialPos;
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
        yield return new WaitForSecondsRealtime(before);

        activator = true;

        yield return new WaitForSecondsRealtime(after);

        activator = false;

    }
}
