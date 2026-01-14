using UnityEngine;
using UnityEngine.UI;

public class ruta : MonoBehaviour
{
    public bool selected = false;
    public Button button;
    Image image;
    Image image2;
    private void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        //image2 = GetComponent<Image>();
    }

    public void makeSelected()
    {
        selected = !selected; //if bool = true then bool = false and vise versa
       // buttonGraphics();
    }

    void buttonGraphics()
    {
        /*if(selected = true && OnMouseDown = true)
        {
            reset.button;

        }*/


        if (!selected)
            button.image = image;
        else
            button.image = image2;
    }

   /* void metod()
    {
        if (cA[0].GetComponent<columner>) { }

    }*/
    
}
