using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private GameObject[] cutsceneImages;
    
    [Header("Timing")]
    [SerializeField] private float fadeInDuration = 1.0f;
    [SerializeField] private float displayDuration = 2.0f;
    [SerializeField] private float pageFlipDuration = 0.8f;
    [SerializeField] private float imageDelay = 0.3f;
    
    [Header("Scene Transition")]
    [SerializeField] private Animator sceneTransitionAnimator;
    [SerializeField] private string endTransitionTrigger = "FadeIn";
    [SerializeField] private float transitionDuration = 1.0f;
    [SerializeField] private string startSceneName = "start";

    void Start()
    {
        // Hide all images initially
        foreach (GameObject img in cutsceneImages)
        {
            if (img != null)
            {
                img.SetActive(false);
                CanvasGroup canvasGroup = img.GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = img.AddComponent<CanvasGroup>();
                }
                canvasGroup.alpha = 0f;
            }
        }
        
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        for (int i = 0; i < cutsceneImages.Length; i++)
        {
            if (cutsceneImages[i] != null)
            {
                // Activate and fade in the image
                cutsceneImages[i].SetActive(true);
                CanvasGroup canvasGroup = cutsceneImages[i].GetComponent<CanvasGroup>();
                RectTransform rectTransform = cutsceneImages[i].GetComponent<RectTransform>();
                
                // Reset scale and position
                rectTransform.localScale = Vector3.one;
                yield return FadeTo(canvasGroup, 1f, fadeInDuration);
                
                // Display the image
                yield return new WaitForSeconds(displayDuration);
                
                // Page flip effect (if not the last image)
                if (i < cutsceneImages.Length - 1)
                {
                    yield return StartCoroutine(PageFlip(rectTransform, canvasGroup));
                }
                else
                {
                    // For the last image, just fade out
                    yield return FadeTo(canvasGroup, 0f, fadeInDuration);
                    cutsceneImages[i].SetActive(false);
                }
                
                // Delay between images
                if (i < cutsceneImages.Length - 1)
                {
                    yield return new WaitForSeconds(imageDelay);
                }
            }
        }
        
        // After all images, transition to start scene
        yield return StartCoroutine(TransitionToStartScene());
    }

    private IEnumerator PageFlip(RectTransform rectTransform, CanvasGroup canvasGroup)
    {
        Vector3 originalScale = rectTransform.localScale;
        float elapsed = 0f;

        while (elapsed < pageFlipDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / pageFlipDuration);
            
            // Create page flip effect by scaling on X axis
            float scaleX = Mathf.Lerp(1f, 0f, t);
            rectTransform.localScale = new Vector3(scaleX, originalScale.y, originalScale.z);
            
            // Fade out as the page "flips"
            canvasGroup.alpha = 1f - t;
            
            yield return null;
        }

        // Reset for next image
        rectTransform.localScale = originalScale;
        canvasGroup.alpha = 0f;
    }

    private IEnumerator FadeTo(CanvasGroup canvasGroup, float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }

    private IEnumerator TransitionToStartScene()
    {
        // Trigger the end transition (fade to black)
        if (sceneTransitionAnimator != null)
        {
            sceneTransitionAnimator.SetTrigger(endTransitionTrigger);
            yield return new WaitForSeconds(transitionDuration);
        }

        // Load the start scene
        SceneManager.LoadScene(startSceneName);
    }
}
