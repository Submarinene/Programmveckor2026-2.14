using UnityEngine;

public class MoveProp : MonoBehaviour
{
    bool isMoved = false;
    public void OnMouseDown()
    {
        
        if (!isMoved)
        {
            //move the prop to the right if it's not already moved
            Debug.Log("You moved the prop!");
            transform.position += new Vector3(1, 0, 0);
            isMoved = true;
        } else {
            //move the prop back
            transform.position -= new Vector3(1, 0, 0);
            isMoved = false;
        }
    }

    
    
}
