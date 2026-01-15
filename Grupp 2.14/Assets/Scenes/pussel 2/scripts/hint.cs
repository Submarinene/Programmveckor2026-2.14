using UnityEngine;

public class hint : MonoBehaviour
{
    [SerializeField] GameObject hints; //ställe att lägga parenten som heter instuktion


    public void SättPåHint()
    {
        Time.timeScale = 0;
        hints.SetActive(true); // sätter på parenten/objektet som är insat på instuktion
    }

    public void StängAvHint() //metod som ska köras när man trycker på -->
    {
        hints.SetActive(false); // stänger av parenten/objektet som är insat på instuktion
        Time.timeScale = 1;
    }
}
