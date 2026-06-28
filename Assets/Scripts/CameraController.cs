using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Header("Follow")]
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 playerOffset;
    
    [Header("Focus")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float focusZoom = 5f;
    [SerializeField] private float normalZoom = 8f;
    [SerializeField] private Vector3 focusOffset;

    private Camera cam;
    private Transform focusTarget;

    public bool IsFocused => focusTarget != null;

    private void Awake()
    {
        Instance = this;

        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        UpdatePosition();
        UpdateZoom();
    }

    private void UpdatePosition()
    {
        Vector3 targetPosition;

        if (focusTarget != null)
        {
            targetPosition = focusTarget.position + focusOffset;
        }
        else
        {
            targetPosition = player.position + playerOffset;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void UpdateZoom()
    {
        if (cam == null)
            return;

        float targetZoom = focusTarget != null
            ? focusZoom
            : normalZoom;

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, moveSpeed * Time.deltaTime);
    }

    public void Focus(Transform target)
    {
        focusTarget = target;
    }

    public void Unfocus()
    {
        focusTarget = null;
    }
}