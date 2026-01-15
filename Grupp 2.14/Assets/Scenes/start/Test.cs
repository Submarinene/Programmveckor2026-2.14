using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Test : MonoBehaviour
{
    [SerializeField] private Animator sceneTransitionAnimator;
    [SerializeField] private string endTransitionTrigger = "FadeIn";
    [SerializeField] private float transitionDuration = 1.0f;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject quitButton;

    public void LoadCutscene()
    {
        StartCoroutine(TransitionToCutscene());
    }

    void Start()
    {
        // Hide buttons on startup
        if (startButton != null)
            startButton.SetActive(false);
        if (quitButton != null)
            quitButton.SetActive(false);
            StartCoroutine(StartUpTransition());
    }

    private IEnumerator TransitionToCutscene()
    {
        // Hide buttons before starting fade
        if (startButton != null)
            startButton.SetActive(false);
        if (quitButton != null)
            quitButton.SetActive(false);

        // Trigger the end transition (fade to black)
        if (sceneTransitionAnimator != null)
        {
            sceneTransitionAnimator.SetTrigger(endTransitionTrigger);
            yield return new WaitForSeconds(transitionDuration);
        }

        SceneManager.LoadScene("Startup Cutscene");
    }

    private IEnumerator StartUpTransition()
    {
        if(sceneTransitionAnimator != null)
        {
            yield return new WaitForSeconds(transitionDuration);
            if (startButton != null)
                startButton.SetActive(true);
            if (quitButton != null)
                quitButton.SetActive(true);
        }
    }
}
