using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScene : MonoBehaviour
{

    private GameObject logo;

    private string fadeOut = "fade out";

    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        
        _anim = GetComponent<Animator>();

        StartCoroutine(Animation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Animation()
    {
        

        yield return new WaitForSecondsRealtime(1);

        _anim.SetBool(fadeOut, true);

        yield return new WaitForSecondsRealtime(4f);

        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
