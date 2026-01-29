using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class pieces : MonoBehaviour
{

    public bool bit0 = false;
    public bool bit1 = false;
    public bool bit2 = false;
    public bool bit3 = false;
    public bool bit4 = false;
    public bool bit5 = false;
    public bool bit6 = false;
    public bool bit7 = false;
    public bool bit8 = false;

    [SerializeField]
    public GameObject b0;
    [SerializeField]
    public GameObject b1;
    [SerializeField]
    public GameObject b2;
    [SerializeField]
    public GameObject b3;
    [SerializeField]
    public GameObject b4;
    [SerializeField]
    public GameObject b5;
    [SerializeField]
    public GameObject b6;
    [SerializeField]
    public GameObject b7;
    [SerializeField]
    public GameObject b8;

    [SerializeField] GameObject completePuzzle1; //ställe att lägga parenten som heter complete puzzle
    [SerializeField] GameObject timerScript;


    void Start()
    {
        completePuzzle1.SetActive(false); // stänger av parenten/objektet som är insat på completePuzzle
    }

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Mouse0)) //när man trycker på musknapp0
        {

            if (CheckWordRightSpot(b0)) //ifall ord är complete
            {
                bit0 = true; //sätter ordets bool = true
            }
            if (CheckWordRightSpot(b1))
            {
                bit1 = true;
            }
            if (CheckWordRightSpot(b2))
            {
                bit2 = true;
            }
            if (CheckWordRightSpot(b3))
            {
                bit3 = true;
            }
            if (CheckWordRightSpot(b4))
            {
                bit4 = true;
            }
            if (CheckWordRightSpot(b5))
            {
                bit5 = true;
            }
            if (CheckWordRightSpot(b6))
            {
                bit6 = true;
            }
            if (CheckWordRightSpot(b7))
            {
                bit7 = true;
            }
            if (CheckWordRightSpot(b8))
            {
                bit8 = true;
            }

        }


        if (bit0 && bit1 && bit2 && bit3 && bit4 && bit5 && bit6 && bit7 && bit8) //när alla ord är färdiga så kör den på metoden puzzleComplete 
        {
            puzzle1Complete();
            
        }
    }

    void puzzle1Complete() //metod som ska köras när puzzlet är färdigt
    {
        completePuzzle1.SetActive(true); // sätter på parenten/objektet som är insat på completePuzzle
        timerScript.SetActive(false);
    }

    bool CheckWordRightSpot(GameObject bit) //kollar gameobjects 
    {
        bool complete = true;


        if (bit.GetComponent<piece>().isTouchingCorrectBlank != true) //kollar ifall boolen från skriptet word är != selected
        {
            complete = false;
        }

        return complete;
    }


}