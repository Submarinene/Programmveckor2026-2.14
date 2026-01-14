using UnityEngine;
using TMPro;
using System.Linq;
using static UnityEditor.Progress;

public class CollectPieces : MonoBehaviour
{
    Vector3 mousePosition;
    RaycastHit rayHit;
    GameObject item;
    bool isHit;
    [SerializeField] TextMeshProUGUI counter;
    int count = 0;

    private void Start()
    {
        counter.text = count.ToString();
    }
    void Update()
    {
        counter.text = count.ToString();
        if (count == 9)
        {
            //go to next puzzle
        }
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            Ray mouseRay = Camera.main.ScreenPointToRay(mousePosition);
            isHit = Physics.Raycast(mouseRay, out rayHit, 100f);
            if (isHit)
            {
                item = rayHit.collider.gameObject;
                CollectItem(item);
                Debug.Log(item.name + "   " + rayHit.point);
            }
            else
            {
                Debug.Log("Empty Space");
            }
        }
    }

    void CollectItem(GameObject item)
    {
        if (item.CompareTag("Piece"))
        {
            Debug.Log("You collected a piece!");
            count += 1;
            Destroy(item);
        }
    }
}
