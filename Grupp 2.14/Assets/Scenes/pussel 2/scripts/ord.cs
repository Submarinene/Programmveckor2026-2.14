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

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

            Debug.Log("test");

            if (CheckCompleteWord(ord0))
            {
                ord0complete = true;
                //gör alla dessa buttons ej interactable
                LockWord(ord0);
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

    private void OnMouseUp()
    {
        Debug.Log("test");
    }

    bool CheckCompleteWord(GameObject[] ord)
    {
        bool complete = true;

        foreach(GameObject bokstav in ord)
        {
            if(bokstav.GetComponent<ruta>().selected != true)
            {
                complete = false;
            }

        }
        return complete;
    }

    void LockWord(GameObject[] ord)
    {
        foreach (GameObject bokstav in ord)
        {
            bokstav.GetComponent<Button>().image.color = Color.lightGreen; // gör ordet grönt
            bokstav.GetComponent<ruta>().button.interactable = false; // gör att man inte kan ändra på färgen

        }
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
