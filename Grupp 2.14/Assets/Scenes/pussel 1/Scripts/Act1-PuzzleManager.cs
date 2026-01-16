using UnityEngine;
using UnityEngine.SceneManagement;

public class Act1PuzzleManager : MonoBehaviour
{
    [SerializeField] CollectPieces collectScript;
    [SerializeField] LinearTime timerScript;
    [SerializeField] GameObject swipeControl;
    [SerializeField] GameObject assemblePuzzle;
    [SerializeField] int loseSceneIndex;
    bool hasCollectedAllPieces;
    bool isTimerUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        assemblePuzzle.SetActive(false);
        swipeControl.SetActive(true);
        hasCollectedAllPieces = collectScript.GetComponent<CollectPieces>().Win();
        isTimerUp = timerScript.GetComponent<LinearTime>().Lose();

    }

    // Update is called once per frame
    void Update()
    {
        CollectAllPiece();
        TimersUp();
    }

    void CollectAllPiece()
    {
        hasCollectedAllPieces = collectScript.GetComponent<CollectPieces>().Win();
        if (hasCollectedAllPieces && !isTimerUp)
        {
            assemblePuzzle.SetActive(true);
            swipeControl.SetActive(false);
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
