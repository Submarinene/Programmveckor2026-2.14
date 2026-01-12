using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class test : MonoBehaviour
{
    //lista med typ rigidbody(istället för img) på alla collumner 
    //lista på outlines
    [SerializeField]
    GameObject[] cA;

    void Start()
    {
        for (int i = 0; i < cA.Length; i++)
        {
            cA(i).SetActive(true);
        }

    }

    public void OnMouseDown()
    {


       

      /* for(int i = 0; i < collumnA.Length; i++)
        {
            collumnA(i).SetActive(false);
        }
        */
    }



}
