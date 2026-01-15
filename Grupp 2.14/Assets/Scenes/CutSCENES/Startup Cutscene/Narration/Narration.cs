using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NarrationClip
{
    [TextArea(2, 4)]
    public string narrationText;
    public AudioClip audioClip;
    [Tooltip("Delay before this narration starts (in seconds)")]
    public float startDelay = 0f;
    [Tooltip("Wait for audio to finish before next narration")]
    public bool waitForAudio = true;
    [Tooltip("Additional delay after audio finishes")]
    public float additionalDelay = 0.5f;
}

public class Narration : MonoBehaviour
{
    [Header("Narration Settings")]
    [SerializeField] private NarrationClip[] narrationClips;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private bool skipWithSpace = true;
    
    [Header("Audio Settings")]
    [SerializeField] [Range(0f, 1f)] private float volume = 1f;
    [SerializeField] [Range(0.1f, 3f)] private float pitch = 1f;
    [SerializeField] private bool loopLastClip = false;
    
    [Header("Subtitle Settings")]
    [SerializeField] private bool showSubtitles = true;
    [SerializeField] private GameObject subtitlePanel;
    [SerializeField] private TMPro.TextMeshProUGUI subtitleText;
    [SerializeField] private float subtitleFadeTime = 0.5f;
    
    [Header("Debug")]
    [SerializeField] private bool debugMode = true;
    
    private int currentClipIndex = 0;
    private bool isPlaying = false;
    private bool narrationComplete = false;
    private Coroutine narrationCoroutine;
    
    void Start()
    {
        InitializeAudioSource();
        
        if (playOnStart)
        {
            StartNarration();
        }
    }
    
    void Update()
    {
        if (skipWithSpace && Input.GetKeyDown(KeyCode.Space) && isPlaying)
        {
            SkipToNextClip();
        }
        
        if (debugMode && Input.GetKeyDown(KeyCode.N))
        {
            StartNarration();
        }
    }
    
    void InitializeAudioSource()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
    }
    
    public void StartNarration()
    {
        if (narrationClips == null || narrationClips.Length == 0)
        {
            Debug.LogWarning("No narration clips assigned!", this);
            return;
        }
        
        currentClipIndex = 0;
        narrationComplete = false;
        
        if (narrationCoroutine != null)
        {
            StopCoroutine(narrationCoroutine);
        }
        
        narrationCoroutine = StartCoroutine(PlayNarrationSequence());
    }
    
    public void StopNarration()
    {
        if (narrationCoroutine != null)
        {
            StopCoroutine(narrationCoroutine);
            narrationCoroutine = null;
        }
        
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        
        isPlaying = false;
        HideSubtitles();
    }
    
    public void SkipToNextClip()
    {
        if (currentClipIndex < narrationClips.Length - 1)
        {
            currentClipIndex++;
            if (narrationCoroutine != null)
            {
                StopCoroutine(narrationCoroutine);
            }
            narrationCoroutine = StartCoroutine(PlayNarrationSequence());
        }
        else
        {
            StopNarration();
            narrationComplete = true;
        }
    }
    
    private IEnumerator PlayNarrationSequence()
    {
        isPlaying = true;
        
        for (int i = currentClipIndex; i < narrationClips.Length; i++)
        {
            currentClipIndex = i;
            NarrationClip currentClip = narrationClips[i];
            
            if (debugMode)
                Debug.Log($"Playing narration {i + 1}/{narrationClips.Length}: {currentClip.narrationText}");
            
            // Wait for start delay
            if (currentClip.startDelay > 0)
            {
                yield return new WaitForSeconds(currentClip.startDelay);
            }
            
            // Show subtitles if enabled
            if (showSubtitles && !string.IsNullOrEmpty(currentClip.narrationText))
            {
                yield return StartCoroutine(ShowSubtitles(currentClip.narrationText));
            }
            
            // Play audio clip if available
            if (currentClip.audioClip != null)
            {
                yield return StartCoroutine(PlayAudioClip(currentClip.audioClip, i == narrationClips.Length - 1 && loopLastClip));
                
                // Wait for audio to finish if required
                if (currentClip.waitForAudio)
                {
                    yield return new WaitForSeconds(currentClip.audioClip.length);
                }
            }
            
            // Additional delay after clip
            if (currentClip.additionalDelay > 0)
            {
                yield return new WaitForSeconds(currentClip.additionalDelay);
            }
        }
        
        isPlaying = false;
        narrationComplete = true;
        HideSubtitles();
        
        if (debugMode)
            Debug.Log("Narration sequence complete!");
    }
    
    private IEnumerator PlayAudioClip(AudioClip clip, bool loop)
    {
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
        
        if (!loop)
        {
            yield return new WaitWhile(() => audioSource.isPlaying);
        }
    }
    
    private IEnumerator ShowSubtitles(string text)
    {
        if (subtitlePanel == null || subtitleText == null)
        {
            Debug.LogWarning("Subtitle components not assigned!", this);
            yield break;
        }
        
        // Fade in subtitles
        subtitlePanel.SetActive(true);
        subtitleText.text = text;
        
        CanvasGroup canvasGroup = subtitlePanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = subtitlePanel.AddComponent<CanvasGroup>();
        }
        
        float elapsed = 0f;
        while (elapsed < subtitleFadeTime)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / subtitleFadeTime);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }
    
    private void HideSubtitles()
    {
        if (subtitlePanel == null) return;
        
        CanvasGroup canvasGroup = subtitlePanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = subtitlePanel.AddComponent<CanvasGroup>();
        }
        
        StartCoroutine(FadeOutSubtitles(canvasGroup));
    }
    
    private IEnumerator FadeOutSubtitles(CanvasGroup canvasGroup)
    {
        float elapsed = 0f;
        float startAlpha = canvasGroup.alpha;
        
        while (elapsed < subtitleFadeTime)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / subtitleFadeTime);
            yield return null;
        }
        
        canvasGroup.alpha = 0f;
        subtitlePanel.SetActive(false);
    }
    
    // Public methods for external control
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        if (audioSource != null)
            audioSource.volume = volume;
    }
    
    public void SetPitch(float newPitch)
    {
        pitch = Mathf.Clamp(newPitch, 0.1f, 3f);
        if (audioSource != null)
            audioSource.pitch = pitch;
    }
    
    public bool IsPlaying()
    {
        return isPlaying;
    }
    
    public bool IsComplete()
    {
        return narrationComplete;
    }
    
    public int GetCurrentClipIndex()
    {
        return currentClipIndex;
    }
    
    public NarrationClip GetCurrentClip()
    {
        if (currentClipIndex >= 0 && currentClipIndex < narrationClips.Length)
            return narrationClips[currentClipIndex];
        return null;
    }
}
