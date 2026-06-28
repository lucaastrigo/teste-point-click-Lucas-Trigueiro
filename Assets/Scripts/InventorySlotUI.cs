using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;

    public void Setup(ItemData item)
    {
        icon.sprite = item.icon;
    }
}