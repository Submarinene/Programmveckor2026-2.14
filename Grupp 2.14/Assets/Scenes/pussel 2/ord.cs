using TMPro;
using UnityEngine;

public class ord : MonoBehaviour
{
    [SerializeField]
    public GameObject[] ord0;
    [SerializeField]
    public GameObject[] ord1;
    [SerializeField]
    public GameObject[] ord2;
    [SerializeField]
    public GameObject[] ord3;
    [SerializeField]
    public GameObject[] ord4;
    [SerializeField]
    public GameObject[] ord5;

    [SerializeField] GameObject completePuzzle; //ställe att lägga parenten som heter complete puzzle


    void Start()
    {
        completePuzzle.SetActive(false); // stänger av parenten/objektet som är insat på completePuzzle
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return)) //när man trycker på enter så kör den på metoden puzzleComplete.... det ska bytas ut till när alla ord är färdiga
        {
            puzzleComplete();
        }
    }

    void puzzleComplete() //metod som ska köras när puzzlet är färdigt
    {
        completePuzzle.SetActive(true); // sätter på parenten/objektet som är insat på completePuzzle
    }
}
