using UnityEngine;

public class Monument : MonoBehaviour, IInteractable
{
    [SerializeField] private UIPause pauseMenu;

    public void Interact()
    {
        VolumeController.Instance.PlaySound("focus");
        CameraController.Instance.Focus(transform);
        pauseMenu.SetUnfocusButtonVisible(true);
    }
}