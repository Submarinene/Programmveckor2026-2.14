using UnityEngine;

public class instruktioner : MonoBehaviour
{
    [SerializeField] GameObject instruktion; //ställe att lägga parenten som heter instuktion


    void Start()
    {
        instruktion.SetActive(true); // sätter på parenten/objektet som är insat på instuktion
    }

   public void stängAvInstruktioner() //metod som ska köras när man trycker på -->
    {
        instruktion.SetActive(false); // stänger av parenten/objektet som är insat på instuktion
    }
}
