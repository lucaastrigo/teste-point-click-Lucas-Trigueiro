using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 direction = mainCamera.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(-direction);
    }
}