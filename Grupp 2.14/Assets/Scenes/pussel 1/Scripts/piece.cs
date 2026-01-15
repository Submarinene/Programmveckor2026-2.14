using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class piece : MonoBehaviour
{
    public bool isTouchingCorrectBlank = false;
    [SerializeField] GameObject correctBlankSpace;

   /* public void makeOrdSelected()
    {
        if (isTouchingCorrectBlank == true)
        {
        }

    }*/

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Blank Space"))
        {
            if (collision.gameObject == correctBlankSpace)
            {
                isTouchingCorrectBlank = true;
            }
            else
            {
                isTouchingCorrectBlank = false;
            }
        }
    }
}
