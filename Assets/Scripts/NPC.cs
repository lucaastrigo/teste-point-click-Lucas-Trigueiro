using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueData dialogue;

    public void Interact()
    {
        VolumeController.Instance.PlaySound("dialogue-start");
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}