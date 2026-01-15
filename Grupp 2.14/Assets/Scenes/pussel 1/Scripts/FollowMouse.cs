using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] float distance;
    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
    }
}
