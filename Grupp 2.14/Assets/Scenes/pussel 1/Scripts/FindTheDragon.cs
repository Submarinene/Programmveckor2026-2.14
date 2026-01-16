using UnityEngine;

public class FindTheDragon : MonoBehaviour
{
    [SerializeField] Transform[] props;
    [SerializeField] float maxCooldown;
    float timer;
    bool isFound = false;

    private void Start()
    {
        transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!isFound)
        {
            if (timer > maxCooldown)
            {
                TeleportPoints(Random.Range(0, props.Length));
                timer = 0;
            }
        } 
    }
    public void OnMouseDown()
    {
        //Debug.Log("The Dragon has been found!");
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        transform.position = new Vector3(0.47f, 0.54f, -5.46f);
        isFound = true;
    }

    void TeleportPoints(int propIndex)
    {
        transform.position = props[propIndex].transform.position;
    }

    public bool IsDragonFound()
    {
        if (isFound)
        {
            return true;
        }
        return false;
    }
}
