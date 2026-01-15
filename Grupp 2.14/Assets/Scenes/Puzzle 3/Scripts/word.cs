using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class word : MonoBehaviour
{
  
    public TextMeshPro texten;
    public bool isTouchingCorrectBlank = false;
    [SerializeField] GameObject correctBlankSpace;

    private void Start()
    {
        texten = GetComponent<TextMeshPro>();
    }
    public void makeOrdSelected()
    {
        if(isTouchingCorrectBlank == true)
        {
            ordGraphics();
        }
           
    }

    void ordGraphics()
    {

        if (isTouchingCorrectBlank)
            texten.color = Color.green; //byter färgen till röd
        else
            texten.color = Color.black; //byter färgen till röd
    }

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
