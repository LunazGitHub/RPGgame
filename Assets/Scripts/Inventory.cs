using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    // Simple list (no stacking). Change to a dictionary if you need stacks.
    public List<ItemData> items = new List<ItemData>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Add(ItemData item)
    {
        if (item == null) return;
        items.Add(item);
        Debug.Log("Picked up: " + item.itemName);
    }

    public void Remove(ItemData item)
    {
        if (item == null) return;
        items.Remove(item);
    }
}
