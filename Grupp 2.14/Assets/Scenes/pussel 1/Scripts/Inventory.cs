using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory
{
    public List<GameObject> items;

    public Inventory()
    {
        items = new List<GameObject>();
        Debug.Log("Inventory");
    }

    public void AddItem(GameObject item)
    {
        items.Add(item);
        Debug.Log(items.Count);
    }
}
