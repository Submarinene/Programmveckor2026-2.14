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
    [SerializeField] private float slideTransitionDuration = 0.8f;
    [SerializeField] private float slideDistance = 1920f; // Adjust based on screen width
    
    [Header("Scene Transition")]
    [SerializeField] private Animator sceneTransitionAnimator;
    [SerializeField] private string endTransitionTrigger = "FadeIn";
    [SerializeField] private float transitionDuration = 1.0f;
    [SerializeField] private string startSceneName = "start";

    private int currentImageIndex = 0;
    private bool isTransitioning = false;

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
        
        // Show first image
        if (cutsceneImages.Length > 0)
        {
            StartCoroutine(ShowImage(0));
        }
    }

    void Update()
    {
        // Check for spacebar input to advance to next image
        if (Input.GetKeyDown(KeyCode.Space) && !isTransitioning)
        {
            AdvanceToNextImage();
        }
    }

    private void AdvanceToNextImage()
    {
        if (currentImageIndex < cutsceneImages.Length - 1)
        {
            StartCoroutine(SlideToNextImage());
        }
        else
        {
            // Last image, transition to start scene
            StartCoroutine(TransitionToStartScene());
        }
    }

    private IEnumerator ShowImage(int index)
    {
        if (index >= cutsceneImages.Length) yield break;
        
        GameObject currentImage = cutsceneImages[index];
        currentImage.SetActive(true);
        CanvasGroup canvasGroup = currentImage.GetComponent<CanvasGroup>();
        RectTransform rectTransform = currentImage.GetComponent<RectTransform>();
        
        // Position image at center
        rectTransform.anchoredPosition = Vector2.zero;
        
        // Fade in
        yield return FadeTo(canvasGroup, 1f, fadeInDuration);
    }

    private IEnumerator SlideToNextImage()
    {
        isTransitioning = true;
        
        GameObject currentImage = cutsceneImages[currentImageIndex];
        GameObject nextImage = cutsceneImages[currentImageIndex + 1];
        
        CanvasGroup currentCanvas = currentImage.GetComponent<CanvasGroup>();
        CanvasGroup nextCanvas = nextImage.GetComponent<CanvasGroup>();
        
        RectTransform currentRect = currentImage.GetComponent<RectTransform>();
        RectTransform nextRect = nextImage.GetComponent<RectTransform>();
        
        // Setup next image to the right of current image
        nextImage.SetActive(true);
        nextCanvas.alpha = 1f;
        nextRect.anchoredPosition = new Vector2(slideDistance, 0);
        
        // Slide current image to left and next image to center
        float elapsed = 0f;
        Vector2 currentStartPos = currentRect.anchoredPosition;
        Vector2 nextStartPos = nextRect.anchoredPosition;
        
        while (elapsed < slideTransitionDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / slideTransitionDuration);
            
            // Slide current image left
            currentRect.anchoredPosition = Vector2.Lerp(currentStartPos, new Vector2(-slideDistance, 0), t);
            
            // Slide next image to center
            nextRect.anchoredPosition = Vector2.Lerp(nextStartPos, Vector2.zero, t);
            
            yield return null;
        }
        
        // Final positions
        currentRect.anchoredPosition = new Vector2(-slideDistance, 0);
        nextRect.anchoredPosition = Vector2.zero;
        
        // Hide current image
        currentImage.SetActive(false);
        currentCanvas.alpha = 0f;
        currentRect.anchoredPosition = Vector2.zero;
        
        currentImageIndex++;
        isTransitioning = false;
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
