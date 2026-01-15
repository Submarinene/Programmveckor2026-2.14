using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class word : MonoBehaviour
{
    public bool selected = false;
    public TextMeshProUGUI texten;

    private void Start()
    {
        texten = GetComponent<TextMeshProUGUI>();
    }
    public void makeOrdSelected()
    {
        selected = !selected; //if bool = true then bool = false and vise versa
        ordGraphics();
    }

    void ordGraphics()
    {

        if (selected)
            texten.color = Color.green; //byter färgen till röd
        else
            texten.color = Color.black; //byter färgen till röd
    }


}
