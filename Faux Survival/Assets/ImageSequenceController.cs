using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ImageSequenceController : MonoBehaviour
{
    public Image displayImage; // UI Image component to show images
    public Sprite firstImage; // First image to display
    public Sprite secondImage; // Second image to display
    public float fadeDuration = 1f; // Duration for fading in and out
    public float timeBetweenImages = 3f; // Time in seconds to wait before switching images
    public string nextSceneName; // Name of the scene to load

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
        yield return new WaitForSeconds(timeBetweenImages);

        // Fade out the first image
        yield return StartCoroutine(FadeImage(firstImage, false));

        // Fade in the second image
        yield return StartCoroutine(FadeImage(secondImage, true));
        yield return new WaitForSeconds(timeBetweenImages);

        // Fade out the second image
        yield return StartCoroutine(FadeImage(secondImage, false));

        // Change to the next scene
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
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
