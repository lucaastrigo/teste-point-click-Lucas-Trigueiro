using UnityEngine;

public class FootstepSFX : MonoBehaviour
{
    public void Footstep()
    {
        VolumeController.Instance.PlaySound("walk");
    }
}
