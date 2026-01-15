using UnityEngine;
using UnityEngine.SceneManagement;

public class Act3PuzzleManager : MonoBehaviour
{
    [SerializeField] LinearTime timerScript;
    [SerializeField] GameObject swipeControl;
    [SerializeField] GameObject instructions;
    [SerializeField] FindTheDragon dragonScript;
    [SerializeField] GameObject completePuzzle;
    [SerializeField] GameObject flashlight;
    [SerializeField] GameObject spotlight;
    [SerializeField] int loseSceneIndex;
    bool hasFound;
    bool isTimerUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instructions.SetActive(true);
        swipeControl.SetActive(true);
        flashlight.SetActive(true);
        timerScript.gameObject.SetActive(true);
        completePuzzle.SetActive(false);
        spotlight.SetActive(false);
        hasFound = dragonScript.GetComponent<FindTheDragon>().IsDragonFound();
        isTimerUp = timerScript.GetComponent<LinearTime>().Lose();
    }

    // Update is called once per frame
    void Update()
    {
        Win();
        TimersUp();
    }

    void Win()
    {
        hasFound = dragonScript.GetComponent<FindTheDragon>().IsDragonFound();
        if (hasFound && !isTimerUp)
        {
            swipeControl.SetActive(false);
            flashlight.SetActive(false);
            timerScript.gameObject.SetActive(false);
            completePuzzle.SetActive(true);
            spotlight.SetActive(true);
        }
    }

    void TimersUp()
    {
        isTimerUp = timerScript.GetComponent<LinearTime>().Lose();
        if (isTimerUp)
        {
            SceneManager.LoadScene(loseSceneIndex);
        }
    }
}
