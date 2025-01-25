using System.Collections;
using UnityEngine;

public class SceneFadeOut : MonoBehaviour
{
    public Animator transitionAnim;

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        transitionAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1.5f);
        transitionAnim.gameObject.SetActive(false);
    }
}
