using System.Collections;
using UnityEngine;

public class disminutionSize : MonoBehaviour
{
    RectTransform _rTransform;

    Vector3 initizlSize;

    [SerializeField] float speed;
    [SerializeField, Range(0,3)] float time;

    [SerializeField] GameObject clinkSoundGO;

    private void Awake()
    {
        _rTransform = GetComponent<RectTransform>();

        initizlSize = _rTransform.localScale;
    }

    private void OnEnable()
    {
        _rTransform.localScale = initizlSize;

        StartCoroutine(Inactivate());
    }

    private void Update()
    {
       _rTransform.localScale = transform.localScale * speed;
    }

    IEnumerator Inactivate()
    {
        yield return new WaitForSecondsRealtime(time);

        clinkSoundGO.SetActive(true);

        this.gameObject.SetActive(false);
    }
}
