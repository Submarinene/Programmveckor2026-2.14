using TMPro;
using UnityEngine;

public class ord : MonoBehaviour
{
    public bool ord0complete = false;
    public bool ord1complete = false;
    public bool ord2complete = false;
    public bool ord3complete = false;
    public bool ord4complete = false;
    public bool ord5complete = false;

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




    void ord0Completed()
    {
        ord0complete = true;

    }
    void ord1Completed()
    {
        ord1complete = true;

    }
    void ord2Completed()
    {
        ord2complete = true;

    }
    void ord3Completed()
    {
        ord3complete = true;

    }
    void ord4Completed()
    {
        ord4complete = true;

    }
    void ord5Completed()
    {
        ord5complete = true;

    }



}
