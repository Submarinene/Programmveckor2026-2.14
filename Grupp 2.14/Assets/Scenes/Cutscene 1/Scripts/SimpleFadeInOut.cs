using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SimpleFadeInOut : MonoBehaviour
{
    [Header("Timing")]
    [SerializeField] private float fadeInDuration = 2.0f;
    [SerializeField] private float holdDuration = 3.0f;
    [SerializeField] private float fadeOutDuration = 2.0f;

    [Header("Events")]
    public UnityEvent onFadeInComplete;
    public UnityEvent onFadeOutComplete;

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
        yield return FadeTo(0f, fadeInDuration);
        onFadeInComplete?.Invoke();

        yield return new WaitForSeconds(holdDuration);

        yield return FadeTo(1f, fadeOutDuration);
        onFadeOutComplete?.Invoke();
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
