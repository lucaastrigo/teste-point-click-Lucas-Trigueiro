using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [SerializeField] private Transform slotsParent;
    [SerializeField] private InventorySlotUI slotPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void Refresh()
    {
        foreach (Transform child in slotsParent) Destroy(child.gameObject);

        foreach (ItemData item in InventoryManager.Instance.Items)
        {
            InventorySlotUI slot = Instantiate(slotPrefab, slotsParent);
            slot.Setup(item);
        }
    }
}