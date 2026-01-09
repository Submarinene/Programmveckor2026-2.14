using UnityEngine;

public class FindTheDragon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseDown()
    {
        Debug.Log("The Dragon has been found!");
        transform.localScale += new Vector3(2, 2, 2);
    }
}
