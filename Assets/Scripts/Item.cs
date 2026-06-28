using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData itemData;

    public void Interact()
    {
        VolumeController.Instance.PlaySound("item");
        InventoryManager.Instance.AddItem(itemData);
        Destroy(gameObject);
    }
}