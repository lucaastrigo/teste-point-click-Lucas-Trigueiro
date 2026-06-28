using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<ItemData> Items { get; private set; } = new();

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(ItemData item)
    {
        Items.Add(item);
        InventoryUI.Instance.Refresh();
    }
}