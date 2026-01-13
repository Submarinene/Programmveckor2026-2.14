using UnityEngine;
using static UnityEngine.Rigidbody2D;

public class SwipeProp : MonoBehaviour
{
    Vector3 mousePosition;
    RaycastHit rayHit;
    GameObject prop;
    bool isHit;
    bool isDragging, swipeLeft, swipeRight;
    Vector2 startPosition, swipeDelta;

    private void Update()
    {
        SwipeControl();
        if (isHit)
        {
            prop = rayHit.collider.gameObject;
            SwipeMovement(prop);
        }
    }

    private void Reset()
    {
        //refresh position
        startPosition = swipeDelta = Vector2.zero;
        isDragging = false;
    }

    void SwipeControl()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            //get initial position of the mouse

            isDragging = true;
            startPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //reset the position of mouse
            isDragging = false;
            Reset();
        }

        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(startPosition);
                isHit = Physics.Raycast(mouseRay, out rayHit);
                //calculate the direction
                swipeDelta = (Vector2)Input.mousePosition - startPosition;
                //Debug.Log(swipeDelta.magnitude);
                
            }
        }

        //when the mouse goes over the minimum reach -> move the prop
        if (swipeDelta.magnitude > 50)
        {
            float x = swipeDelta.x;
            if (x < 0)
            {
                swipeLeft = true;
            }
            else
            {
                swipeRight = true;
            }
            Reset();
        }

    }

    void SwipeMovement(GameObject item)
    {
        if (item.CompareTag("Prop"))
        {
            if (swipeLeft)
            {
                //move to the left
                item.transform.position += Vector3.left;
                swipeLeft = false;
            }
            else if (swipeRight)
            {
                //move to the right
                item.transform.position += Vector3.right;
                swipeRight = false;
            }
        }
        
    }
}
