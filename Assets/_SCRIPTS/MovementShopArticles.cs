using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementShopArticles : MonoBehaviour
{
    RectTransform _rTransf;

    [SerializeField] GameObject targetGO;

    [SerializeField] Vector3 initialPos, target, currentPos, initialScale;

    [SerializeField] float speed;

    [SerializeField] bool activator;

    [SerializeField] float sizeSpeed;


    private void Awake()
    {
        this.gameObject.SetActive(true);
        _rTransf = GetComponent<RectTransform>();
        initialPos = _rTransf.localPosition;  //posición inicial del objeto
        target = (targetGO.GetComponent<RectTransform>().position);
        initialScale = _rTransf.localScale;

    }

    private void OnEnable()
    {
        transform.localPosition = initialPos;
        activator = false;
        _rTransf.localScale = initialScale;
        

    }

    private void Update()
    {
        currentPos = _rTransf.position;

        if (activator)
        {
            _rTransf.position = Vector3.MoveTowards(_rTransf.position,
                target, speed * Time.deltaTime);
            if (_rTransf.localScale.x > 0)
            {
                _rTransf.localScale = _rTransf.localScale - (new Vector3(1, 1, 0)).normalized * sizeSpeed;
            }
            else this.gameObject.SetActive(false);
            

        }
    }

    public void Motion()
    {
        activator = true;        

    }

}
