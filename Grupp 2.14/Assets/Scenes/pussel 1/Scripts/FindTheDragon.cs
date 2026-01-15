using UnityEngine;

public class FindTheDragon : MonoBehaviour
{
    [SerializeField] Transform[] props;
    [SerializeField] float maxCooldown;
    float timer;
    bool isFound = false;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!isFound)
        {
            if (timer > maxCooldown)
            {
                TeleportPoints(Random.Range(0, (props.Length + 1)));
                timer = 0;
            }
        } 
    }
    public void OnMouseDown()
    {
        Debug.Log("The Dragon has been found!");
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        transform.position = new Vector3(0.22f, 1.18f, -6.2f);
        isFound = true;
    }

    void TeleportPoints(int propIndex)
    {
        transform.position = props[propIndex].transform.position;
    }
}
