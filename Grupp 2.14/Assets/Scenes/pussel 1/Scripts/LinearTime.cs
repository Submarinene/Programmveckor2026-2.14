using UnityEngine;
using UnityEngine.UI;

public class LinearTime : MonoBehaviour
{
    [SerializeField] Image timeBar;
    [SerializeField] GameObject timesUpText;
    [SerializeField] float maxTime;
    float timeRemaining;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timesUpText.SetActive(false);
        timeRemaining = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timeBar.fillAmount = timeRemaining / maxTime;
        } else
        {
            timesUpText.SetActive(true);
        }
    }
}
