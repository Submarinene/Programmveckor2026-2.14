using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    bool retryButton = false;

    // Update is called once per frame
    void Update()
    {
        if (retryButton)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void RetryButton()
    {
        Debug.Log("Retry button pressed.");
        retryButton = true;
    }
}
