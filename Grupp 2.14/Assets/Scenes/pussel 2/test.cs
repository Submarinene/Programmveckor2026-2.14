using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class test : MonoBehaviour
{
    //lista med typ rigidbody(istället för img) på alla collumner 
    //lista på outlines
    [SerializeField]
    GameObject[] collumnA;

    void Start()
    {
        for (int i = 0; i < collumnA.Length; i++)
        {
            collumnA(i).SetActive(true);
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
