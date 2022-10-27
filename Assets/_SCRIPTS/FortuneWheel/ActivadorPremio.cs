using System.Collections;
using UnityEngine;

public class ActivadorPremio : MonoBehaviour
{
    [SerializeField] GameObject hasWonPanel;
    public bool activeAnimation = false;
    [SerializeField] float time;

    [SerializeField] GameObject ckinckSoundGO;
    
    public void ActivaPremio(string animation)
    {
        StartCoroutine(ActiveAnimationCoroutine(animation));

        StartCoroutine(AnimationSoundAcitvation(animation));
        
    }

    IEnumerator ActiveAnimationCoroutine(string animation)
    {
        activeAnimation = false;

        yield return new WaitForSecondsRealtime(time);

        hasWonPanel.SetActive(true);
        hasWonPanel.GetComponent<Animator>().SetTrigger(animation);
        yield return new WaitForSecondsRealtime(5f);

        hasWonPanel.SetActive(false);

    }


    IEnumerator AnimationSoundAcitvation(string animation)
    {
        yield return new WaitForSecondsRealtime(time+3.5f);

        if (animation == "dinero" || animation == "malo") ckinckSoundGO.SetActive(true);

    }
}
