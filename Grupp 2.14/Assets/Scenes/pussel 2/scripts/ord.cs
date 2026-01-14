using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        if (Input.GetKeyUp(KeyCode.Mouse0)) //när man trycker på musknapp0
        {

            if (CheckCompleteWord(ord0)) //ifall ord är complete
            {
                ord0complete = true; //sätter ordets bool = true
                LockWord(ord0);  //gör alla dessa buttons ej interactable
            }
            if (CheckCompleteWord(ord1))
            {
                ord1complete = true;
                LockWord(ord1);
            }
            if (CheckCompleteWord(ord2))
            {
                ord2complete = true;
                LockWord(ord2);
            }
            if (CheckCompleteWord(ord3))
            {
                ord3complete = true;
                LockWord(ord3);
            }
            if (CheckCompleteWord(ord4))
            {
                ord4complete = true;
                LockWord(ord4);
            }
            if (CheckCompleteWord(ord5))
            {
                ord5complete = true;
                LockWord(ord5);
            }
        }


        if (ord0complete && ord1complete && ord2complete && ord3complete && ord4complete && ord5complete) //när alla ord är färdiga så kör den på metoden puzzleComplete 
        {
            puzzleComplete();
        }
    }
     
    void puzzleComplete() //metod som ska köras när puzzlet är färdigt
    {
        completePuzzle.SetActive(true); // sätter på parenten/objektet som är insat på completePuzzle
    }

    bool CheckCompleteWord(GameObject[] ord) //kollar listan av gameobjects per ord
    {
        bool complete = true;

        foreach(GameObject bokstav in ord) //för varje bokstav i listan 
        {
            if(bokstav.GetComponent<ruta>().selected != true) //kollar ifall boolen från skriptet ruta är != selected
            {
                complete = false;
            }

        }
        return complete;
    }

    void LockWord(GameObject[] ord) //metod för att "låsa" ord
    {
        foreach (GameObject bokstav in ord)
        {
            bokstav.GetComponent<Button>().image.color = Color.lightGreen; // gör ordet grönt
            bokstav.GetComponent<ruta>().button.interactable = false; // gör att man inte kan ändra på färgen

        }
    }



   /* void ord0Completed() 
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

    }*/



}
