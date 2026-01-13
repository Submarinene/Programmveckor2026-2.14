using UnityEngine;

public class Click : MonoBehaviour
{
    
    Vector3 mousePosition;
    RaycastHit rayHit;
    GameObject item;
    bool isHit;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);
            isHit = Physics.Raycast(mouseRay, out rayHit, 100f);
            if (isHit)
            {
                item = rayHit.collider.gameObject;
                Debug.Log(item.name + "   " + rayHit.point);
            } else
            {
                Debug.Log("Empty Space");
            }
        }
    }

    
}
