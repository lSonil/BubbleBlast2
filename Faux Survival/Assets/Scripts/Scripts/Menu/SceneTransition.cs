using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public Animator transitionAnim;
    public Button game;
    public Button quit;
    public string sceneName;


    public void GoBack()
    {
        StartCoroutine(LoadScene());
    }
    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadScene()
    {
        transitionAnim.gameObject.SetActive(true);
        transitionAnim.SetTrigger("fadeIn");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene()
    {
        transitionAnim.gameObject.SetActive(true);
        if (game != null)
            game.enabled = false;
        if (quit != null)
            quit.enabled = false;
        transitionAnim.SetTrigger("fadeIn");
        StartDisplay();
    }

    public Image displayImage; // UI Image component to show images
    public Sprite firstImage; // First image to display
    public Sprite secondImage; // Second image to display
    public float fadeDuration = 1f; // Duration for fading in and out
    public float timeBetweenImages1 = 3f; // Time in seconds to wait before switching images
    public float timeBetweenImages2 = 3f; // Time in seconds to wait before switching images

    private void StartDisplay()
    {
        if (displayImage == null)
        {
            Debug.LogError("Display Image is not assigned.");
            return;
        }

        if (firstImage == null || secondImage == null)
        {
            Debug.LogError("Images are not assigned.");
            return;
        }

        // Start the sequence
        StartCoroutine(DisplayImageSequence());
    }

    private IEnumerator DisplayImageSequence()
    {
        // Fade in the first image
        yield return StartCoroutine(FadeImage(firstImage, true));
        yield return new WaitForSeconds(timeBetweenImages1);

        // Fade out the first image
        yield return StartCoroutine(FadeImage(firstImage, false));

        // Fade in the second image
        yield return StartCoroutine(FadeImage(secondImage, true));
        yield return new WaitForSeconds(timeBetweenImages2);

        // Fade out the second image
        yield return StartCoroutine(FadeImage(secondImage, false));

        // Change to the next scene
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Next scene name is not assigned.");
        }
    }

    private IEnumerator FadeImage(Sprite imageSprite, bool fadeIn)
    {
        displayImage.sprite = imageSprite;
        Color color = displayImage.color;
        float alphaStart = fadeIn ? 0f : 1f;
        float alphaEnd = fadeIn ? 1f : 0f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(alphaStart, alphaEnd, elapsedTime / fadeDuration);
            color.a = alpha;
            displayImage.color = color;
            yield return null;
        }

        // Ensure the final alpha value is set
        color.a = alphaEnd;
        displayImage.color = color;
    }
}
