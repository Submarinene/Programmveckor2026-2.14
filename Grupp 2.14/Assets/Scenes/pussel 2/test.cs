using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class test : MonoBehaviour
{
    //lista med typ rigidbody(istället för img) på alla collumner 
    //lista på outlines
    [SerializeField]
    GameObject[] cA; //column A

    [SerializeField]
    GameObject[] cB; //column B

    [SerializeField]
    GameObject[] cC; //column C

    [SerializeField]
    GameObject[] cD; //column D

    [SerializeField]
    GameObject[] cE; //column E

    [SerializeField]
    GameObject[] cF; //column F

    [SerializeField]
    GameObject[] cG; //column G

    [SerializeField]
    GameObject[] cH; //column H

    [SerializeField]
    GameObject[] cI; //column I

    [SerializeField]
    GameObject[] cJ; //column J

    [SerializeField]
    GameObject[] antalOrd;
  

    void Start()
    {
      /*  for (int i = 0; i < antalOrd.Length; i++)
        {
            antalOrd(i).SetActive(false);
        }*/

    }

    public void OnMouseDown()
    {


        Debug.Log("det funkar att klicka");

  
    }



}
