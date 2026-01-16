using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class FadeController : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float fadeOutDuration = 2.0f;
    [SerializeField] private float holdDuration = 3.0f;
    [SerializeField] private float fadeInDuration = 2.0f;
    [SerializeField] private string nextSceneName;

    [Header("Scene Transition")]
    [SerializeField] private Animator sceneTransitionAnimator;
    [SerializeField] private string endTransitionTrigger = "FadeIn";
    [SerializeField] private float transitionDuration = 1.0f;

    [Header("Events")]
    public UnityEvent onFadeOutComplete;
    public UnityEvent onFadeInComplete;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 1f;
    }

    void Start()
    {
        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        // Fade out black panel (reveal curtain + scene)
        yield return FadeTo(0f, fadeOutDuration);
        onFadeOutComplete?.Invoke();

        // Hold while the scene is visible
        yield return new WaitForSeconds(holdDuration);

        // Fade black panel back in (cover only the scene)
        yield return FadeTo(1f, fadeInDuration);
        onFadeInComplete?.Invoke();

        // Trigger the end transition (fade to black)
        if (sceneTransitionAnimator != null)
        {
            sceneTransitionAnimator.SetTrigger(endTransitionTrigger);
            yield return new WaitForSeconds(transitionDuration);
        }

        // Load the next scene
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledTime;
            float t = Mathf.Clamp01(elapsed / duration);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
