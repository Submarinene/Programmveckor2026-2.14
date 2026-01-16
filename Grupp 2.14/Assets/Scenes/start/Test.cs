using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Test : MonoBehaviour
{
    [SerializeField] private Animator sceneTransitionAnimator;
    [SerializeField] private string endTransitionTrigger = "FadeIn";
    [SerializeField] private float transitionDuration = 1.0f;
    [SerializeField] private GameObject Title;
    public void LoadCutscene()
    {
        StartCoroutine(TransitionToCutscene());
    }

    void Start()
    {
        if(Title != null)
        {
            Title.SetActive(false);
            StartCoroutine(StartUpTransition());
        }
    }

    private IEnumerator TransitionToCutscene()
    {
        if(Title != null)
        {
            Title.SetActive(false);
        }
            

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
            if(Title != null)
            {
                Title.SetActive(true);
            }
        }
    }
}
