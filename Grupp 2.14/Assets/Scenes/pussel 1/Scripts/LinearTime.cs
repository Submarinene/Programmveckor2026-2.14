using UnityEngine;
using UnityEngine.UI;

public class LinearTime : MonoBehaviour
{
    [SerializeField] Image timeBar;
    [SerializeField] float maxTime;
    float timeRemaining;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeRemaining = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    public bool Lose()
    {
        if (timeRemaining <= 0)
        {
            return true;
        }
        return false;
    }

    public void Timer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timeBar.fillAmount = timeRemaining / maxTime;
        }
    }

}
