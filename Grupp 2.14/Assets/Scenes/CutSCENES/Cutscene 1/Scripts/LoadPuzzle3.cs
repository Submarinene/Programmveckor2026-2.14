using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadPuzzle3 : MonoBehaviour
{
    [SerializeField] private Animator sceneTransitionAnimator;
    [SerializeField] private string endTransitionTrigger = "FadeIn";
    [SerializeField] private float transitionDuration = 1.0f;
    [SerializeField] int scene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piece"))
        {
            StartCoroutine(TransitionToCutscene());
        }
    }

    private IEnumerator TransitionToCutscene()
    {
        // Trigger the end transition (fade to black)
        if (sceneTransitionAnimator != null)
        {
            sceneTransitionAnimator.SetTrigger(endTransitionTrigger);
            yield return new WaitForSeconds(transitionDuration);
        }

        SceneManager.LoadScene(scene);
    }
}
