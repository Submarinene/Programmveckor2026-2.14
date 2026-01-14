using UnityEngine;

public class FindTheDragon : MonoBehaviour
{
    [SerializeField] Transform propA, propB, propC, propD;
    float timer;
    bool isFound = false;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!isFound)
        {
            if (timer > 2)
            {
                TeleportPoints(Random.Range(1, 5));
                Debug.Log("Dragon teleported!");
                timer = 0;
            }
        } 
    }
    public void OnMouseDown()
    {
        Debug.Log("The Dragon has been found!");
        transform.localScale += new Vector3(2, 2, 2);
        isFound = true;
    }

    void TeleportPoints(int teleportProp)
    {
        if (teleportProp == 1)
        {
            transform.position = new Vector3(propA.transform.position.x, propA.transform.position.y, (propA.transform.position.z + 1));
        }
        else if (teleportProp == 2)
        {
            transform.position = new Vector3(propB.transform.position.x, propB.transform.position.y, (propB.transform.position.z + 1));
        }
        else if (teleportProp == 3)
        {
            transform.position = new Vector3(propC.transform.position.x, propC.transform.position.y, (propC.transform.position.z + 1));
        }
        else if (teleportProp == 4)
        {
            transform.position = new Vector3(propD.transform.position.x, propD.transform.position.y, (propD.transform.position.z + 1));
        }
    }
    
}
