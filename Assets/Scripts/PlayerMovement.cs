using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask interactableLayer;

    private IInteractable targetInteraction;
    private float interactionDistance = 2;

    [SerializeField] private Animator anim;
    [SerializeField] private GameObject visual;

    private Camera mainCamera;
    private NavMeshAgent agent;


    private void Awake()
    {
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();

        if (agent != null) agent.updateRotation = false;
        if (agent != null) agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (anim != null) anim.SetFloat("Speed", agent.velocity.magnitude);

        Vector3 moveDirection = agent.velocity.normalized;
        float side = Vector3.Dot(moveDirection, mainCamera.transform.right);

        if (Mathf.Abs(side) > 0.1f)
        {
            if (visual != null) visual.transform.localScale = new Vector3(side > 0 ? 1 : -1, 1, 1);
        }

        if (CameraController.Instance.IsFocused) return;
        if (DialogueManager.Instance.IsOpen) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return;
                MoveToTouchPosition(touch.position);
            }
        }

        //#if UNITY_EDITOR
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (EventSystem.current.IsPointerOverGameObject()) return;
        //    MoveToTouchPosition(Input.mousePosition);
        //}
        //#endif

        CheckInteraction();
    }

    private void MoveToTouchPosition(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                targetInteraction = interactable;

                if (agent != null) agent.SetDestination(hit.point);

                return;
            }

            if (((1 << hit.collider.gameObject.layer) & groundLayer) != 0)
            {
                targetInteraction = null;

                if (agent != null) agent.SetDestination(hit.point);
            }
        }
    }

    private void CheckInteraction()
    {
        if (targetInteraction == null)
            return;

        if (agent.pathPending)
            return;

        if (agent.remainingDistance <= interactionDistance)
        {
            targetInteraction.Interact();
            targetInteraction = null;
        }
    }
}