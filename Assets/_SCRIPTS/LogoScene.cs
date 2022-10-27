using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoScene : MonoBehaviour
{
    private string fadeOut = "fade out";

    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        
        _anim = GetComponent<Animator>();

        StartCoroutine(Animation());
    }
        

    private IEnumerator Animation()
    {
        

        yield return new WaitForSecondsRealtime(1);

        _anim.SetBool(fadeOut, true);

        yield return new WaitForSecondsRealtime(2f);

        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
