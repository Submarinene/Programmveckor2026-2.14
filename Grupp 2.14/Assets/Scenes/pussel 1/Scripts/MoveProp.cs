using UnityEngine;

public class MoveProp : MonoBehaviour
{
    bool isMoved = false;
    public void OnMouseDown()
    {
        
        if (!isMoved)
        {
            //move the prop to the right
            Debug.Log("You moved the prop!");
            transform.position += new Vector3(1, 0, 0);
            isMoved = true;
        } else {

            transform.position -= new Vector3(1, 0, 0);
            isMoved = false;
        }
    }

    
    
}
