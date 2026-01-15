using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Words : MonoBehaviour
{

    public bool word0 = false;
    public bool word1 = false;
    public bool word2 = false;
    public bool word3 = false;
    public bool word4 = false;
    public bool word5 = false;

    [SerializeField]
    public GameObject w0;
    [SerializeField]
    public GameObject w1;
    [SerializeField]
    public GameObject w2;
    [SerializeField]
    public GameObject w3;
    [SerializeField]
    public GameObject w4;
    [SerializeField]
    public GameObject w5;

    [SerializeField] GameObject completePuzzle3; //ställe att lägga parenten som heter complete puzzle


    void Start()
    {
        completePuzzle3.SetActive(false); // stänger av parenten/objektet som är insat på completePuzzle
    }

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Mouse0)) //när man trycker på musknapp0
        {

            if (CheckWordRightSpot(w0)) //ifall ord är complete
            {
                word0 = true; //sätter ordets bool = true
                //LockWord(w0);  //gör alla dessa buttons ej interactable
            }
            if (CheckWordRightSpot(w1))
            {
                word1 = true;
                //LockWord(ord1);
            }
            if (CheckWordRightSpot(w2))
            {
                word2 = true;
                //LockWord(ord2);
            }
            if (CheckWordRightSpot(w3))
            {
                word3 = true;
                //LockWord(ord3);
            }
            if (CheckWordRightSpot(w4))
            {
                word4 = true;
                //LockWord(ord4);
            }
          
        }


        if (word0 && word1 && word2 && word3 && word4) //när alla ord är färdiga så kör den på metoden puzzleComplete 
        {
            puzzle3Complete();
        }
    }

    void puzzle3Complete() //metod som ska köras när puzzlet är färdigt
    {
        completePuzzle3.SetActive(true); // sätter på parenten/objektet som är insat på completePuzzle
    }

    bool CheckWordRightSpot(GameObject word) //kollar listan av gameobjects per ord
    {
        bool complete = true;

       
            if (word.GetComponent<word>().selected != true) //kollar ifall boolen från skriptet ruta är != selected
            {
                complete = false;
            }

        return complete;
    }

  /*  void LockWord(GameObject[] ord) //metod för att "låsa" ord
    {
        foreach (GameObject bokstav in ord)
        {
            bokstav.GetComponent<Button>().image.color = Color.lightGreen; // gör ordet grönt
            bokstav.GetComponent<ruta>().button.interactable = false; // gör att man inte kan ändra på färgen

        }
    }*/


}