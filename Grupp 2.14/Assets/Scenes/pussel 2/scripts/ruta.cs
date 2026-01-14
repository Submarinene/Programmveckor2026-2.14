using UnityEngine;
using UnityEngine.UI;

public class ruta : MonoBehaviour
{
    public bool selected = false;
    public Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void makeSelected()
    {
        selected = !selected; //if bool = true then bool = false and vise versa
        buttonGraphics();
    }

    void buttonGraphics()
    {

        if (selected)
            button.image.color = Color.red; //byter färgen till röd
        else
            button.image.color = Color.lightGray; //byter färgen till ljus grå
    }

    
}
