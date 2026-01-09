using UnityEngine;
using UnityEngine.UIElements;

public class DragControl : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject wordBox;

    private Vector3 ogWorldBoxPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ogWorldBoxPos = wordBox.transform.position;
    }

    void OnMouseDrag()
    {
        rb.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    }

    void OnMouseUp()
    {
        rb.position = ogWorldBoxPos;
    }
}
