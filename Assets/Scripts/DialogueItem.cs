using UnityEngine;

public class DialogueItem : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData dialogue;
    [SerializeField] private ItemData itemData;

    private bool dialogueFinished;

    public void Interact()
    {
        if (!dialogueFinished)
        {
            dialogueFinished = true;

            VolumeController.Instance.PlaySound("dialogue-start");
            DialogueManager.Instance.StartDialogue(dialogue);

            return;
        }

        VolumeController.Instance.PlaySound("item");
        InventoryManager.Instance.AddItem(itemData);

        Destroy(gameObject);
    }
}