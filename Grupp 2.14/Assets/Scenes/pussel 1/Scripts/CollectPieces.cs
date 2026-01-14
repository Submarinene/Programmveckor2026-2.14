using UnityEngine;
using TMPro;

public class CollectPieces : MonoBehaviour
{
    Vector3 mousePosition;
    RaycastHit rayHit;
    GameObject item;
    bool isHit;
    [SerializeField] TextMeshProUGUI counter;
    [SerializeField] GameObject pieceIcon;
    int count = 0;

    private void Start()
    {
        counter.text = count.ToString();
        counter.gameObject.SetActive(true);
        pieceIcon.SetActive(true);
    }
    void Update()
    {
        counter.text = count.ToString();
        MouseController();
        Win();
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

    public bool Win()
    {
        if (count == 9)
        {
            pieceIcon.SetActive(false);
            counter.gameObject.SetActive(false);
            return true;
        }
        return false;
    }

    void MouseController()
    {
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
}
