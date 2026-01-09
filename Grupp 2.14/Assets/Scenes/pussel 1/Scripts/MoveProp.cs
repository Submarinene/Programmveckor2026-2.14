using UnityEngine;

public class MoveProp : MonoBehaviour
{
    
    bool isMoved = false;

    private void Update()
    {
    }
    public void OnMouseDown()
    {
        
        if (!isMoved)
        {
            //move the prop to the right
            Debug.Log("You moved the object!");
            transform.position += new Vector3(1, 0, 0);
            isMoved = true;
        } else
        {
            transform.position -= new Vector3(1, 0, 0);
            isMoved = false;
        }
    }

    
}
